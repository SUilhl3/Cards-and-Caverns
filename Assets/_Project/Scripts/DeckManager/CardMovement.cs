using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;


public class CardMovement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    #region Fields and Properties

    private Canvas _cardCanvas;
    private RectTransform _rectTransform;
    private Card _card;
    private Vector2 _originalAnchoredPosition;
    [SerializeField] private RectTransform discardTarget;
    private Vector2 _velocity;

    private readonly string CANVAS_TAG = "CardCanvas";

    #endregion

    #region Methods

    private void Start()
    {
        _cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _card = GetComponent<Card>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_rectTransform != null) _originalAnchoredPosition = _rectTransform.anchoredPosition;
        // Mark this card as the currently-dragged card so Deck.UpdateHandLayout will skip it.
        if (Deck.Instance != null) Deck.Instance.CurrentlyDraggingCard = _card;
        // Bring dragged card to front
        if (_rectTransform != null) _rectTransform.SetAsLastSibling();
    }

    #endregion
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += (eventData.delta / _cardCanvas.scaleFactor);

        // Live reorder: determine desired index based on current anchored X and update deck order
        if (Deck.Instance != null && Deck.Instance.HandCards != null)
        {
            var hand = Deck.Instance.HandCards;
            int currentIndex = hand.IndexOf(_card);
            if (currentIndex >= 0)
            {
                float x = _rectTransform.anchoredPosition.x;
                int desiredIndex = 0;
                for (int i = 0; i < hand.Count; i++)
                {
                    var other = hand[i];
                    if (other == _card) continue;
                    var ort = other.GetComponent<RectTransform>();
                    if (ort == null) continue;
                    if (x > ort.anchoredPosition.x) desiredIndex++;
                }

                if (desiredIndex != currentIndex)
                {
                    hand.RemoveAt(currentIndex);
                    if (desiredIndex > hand.Count) desiredIndex = hand.Count;
                    hand.Insert(desiredIndex, _card);
                    // Reflow other cards immediately (skip animating the dragged card)
                    Deck.Instance.UpdateHandLayout(true);
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Clear dragging marker so layout will include this card again
        if (Deck.Instance != null) Deck.Instance.CurrentlyDraggingCard = null;

        // If released over an enemy UI/world object, play the card on that enemy.
        if (_card == null || _card.CardData == null)
        {
            //Deck.Instance.DiscardCard(_card);
            StartCoroutine(MoveToAnchoredPosition(_originalAnchoredPosition, false));
            return;
        }

        // Try to resolve a Combatant from the UI raycast first
        Combatant target = null;
        var go = eventData.pointerCurrentRaycast.gameObject;
        if (go != null)
        {
            target = go.GetComponentInParent<Combatant>();
        }

        // If we didn't get a target from the UI raycast, try a physics raycast (world objects)
        if (target == null)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                var ray = cam.ScreenPointToRay(eventData.position);
                if (Physics.Raycast(ray, out var hit, 100f))
                {
                    target = hit.collider.GetComponentInParent<Combatant>();
                }
                else
                {
                    // 2D fallback
                    var worldPoint = cam.ScreenToWorldPoint(eventData.position);
                    var hit2d = Physics2D.Raycast(worldPoint, Vector2.zero);
                    if (hit2d.collider != null) target = hit2d.collider.GetComponentInParent<Combatant>();
                }
            }
        }

        // If we found a Combatant, prefer the canonical instance from EnemyManager if present
        if (target != null)
        {
            var em = UnityEngine.Object.FindAnyObjectByType<EnemyManager>();
            if (em != null && em.Enemies != null)
            {
                var canonical = em.Enemies.FirstOrDefault(e => e != null && e == target);
                if (canonical != null) target = canonical;
            }

            // Use CombatManager's centralized API to play the UI card on the target so TryPlaySelected is used
            var cm = UnityEngine.Object.FindAnyObjectByType<CombatManager>();
            if (cm != null)
            {
                var played = cm.PlayCard(_card, target);
                if (played)
                {
                    // animate success then discard (Deck handles UI removal)
                    StartCoroutine(PlaySuccessAndDiscard(_card));
                    // ensure remaining cards animate into place
                    if (Deck.Instance != null) Deck.Instance.UpdateHandLayout(false);
                    return;
                }
                else
                {
                    // animate failure (snap back)
                    StartCoroutine(PlayFailureAndReturn());
                    if (Deck.Instance != null) Deck.Instance.UpdateHandLayout(false);
                    return;
                }
            }
        }

        // Default behaviour (no target found): snap back to original anchored position
        StartCoroutine(PlayFailureAndReturn());
        if (Deck.Instance != null) Deck.Instance.UpdateHandLayout(false);
    }

    private System.Collections.IEnumerator PlaySuccessAndDiscard(Card card)
    {
        // If a discard target is assigned, move toward it with a physics-like spring; otherwise shrink out where it is.
        if (discardTarget != null && _rectTransform != null)
        {
            // compute target anchored position in the same rect transform parent space
            Vector2 targetAnchored = discardTarget.anchoredPosition;
            // If discard target is in different parent, convert via canvas. Try to handle common case where both are under same canvas.
            if (discardTarget.transform.root != _rectTransform.transform.root)
            {
                // convert world position to anchored position in this rect's parent
                var worldPos = discardTarget.TransformPoint(discardTarget.rect.center);
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_rectTransform.parent, Camera.main.WorldToScreenPoint(worldPos), Camera.main, out localPoint);
                targetAnchored = localPoint;
            }

            yield return StartCoroutine(MoveToAnchoredPosition(targetAnchored, true));
            yield break;
        }

        // fallback: pop & shrink animation
        var startScale = _rectTransform.localScale;
        var popScale = startScale * 1.12f;
        float t = 0f;
        float dur = 0.10f;
        while (t < dur)
        {
            t += Time.deltaTime;
            _rectTransform.localScale = Vector3.Lerp(startScale, popScale, t / dur);
            yield return null;
        }
        t = 0f; dur = 0.12f;
        while (t < dur)
        {
            t += Time.deltaTime;
            _rectTransform.localScale = Vector3.Lerp(popScale, Vector3.zero, t / dur);
            yield return null;
        }

        if (Deck.Instance != null) Deck.Instance.DiscardCard(card);
        _rectTransform.localScale = startScale;
    }

    private System.Collections.IEnumerator PlayFailureAndReturn()
    {
        // move back to original anchored position using spring-like force
        yield return StartCoroutine(MoveToAnchoredPosition(_originalAnchoredPosition, false));
    }

    // Moves the rectTransform's anchoredPosition toward target using a simple critically-damped spring simulation.
    // If deactivateOnComplete is true, Deck.Instance.DiscardCard(card) will be called at the end (card should be provided via closure).
    // This coroutine is public so external components (Deck) can animate cards into the hand.
    public System.Collections.IEnumerator MoveToAnchoredPosition(Vector2 targetAnchored, bool deactivateOnComplete)
    {
        if (_rectTransform == null) yield break;
        _velocity = Vector2.zero;
        float elapsed = 0f;
        float maxTime = 0.8f; // safety
        // spring parameters
        float stiffness = 1000f; // spring constant
        float damping = 2f * Mathf.Sqrt(stiffness); // critical damping

        while (elapsed < maxTime)
        {
            elapsed += Time.unscaledDeltaTime;
            Vector2 pos = _rectTransform.anchoredPosition;
            Vector2 delta = targetAnchored - pos;
            // F = kx - c v
            Vector2 accel = (stiffness * delta) - (damping * _velocity);
            _velocity += accel * Time.unscaledDeltaTime;
            Vector2 newPos = pos + _velocity * Time.unscaledDeltaTime;
            _rectTransform.anchoredPosition = newPos;

            // stop condition
            if (_velocity.sqrMagnitude < 0.01f && delta.sqrMagnitude < 1f) break;
            yield return null;
        }

        _rectTransform.anchoredPosition = targetAnchored;

        // Record this as the canonical original anchored position for this card when it's used as part of the hand layout
        _originalAnchoredPosition = targetAnchored;

        if (deactivateOnComplete && Deck.Instance != null) Deck.Instance.DiscardCard(_card);
    }

    // Helper to animate into hand after an optional delay (used when drawing multiple cards so they appear one-at-a-time).
    public System.Collections.IEnumerator AnimateIntoHand(Vector2 targetAnchored, float delaySeconds)
    {
        if (delaySeconds > 0f) yield return new WaitForSecondsRealtime(delaySeconds);
        yield return StartCoroutine(MoveToAnchoredPosition(targetAnchored, false));
    }
}
