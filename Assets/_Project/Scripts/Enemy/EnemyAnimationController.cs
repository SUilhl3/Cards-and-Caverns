using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void PlayHit()
    {
        _animator.SetTrigger("Hit");
    }
}
