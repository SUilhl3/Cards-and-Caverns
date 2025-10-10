using UnityEngine;

public class EnemyTestInput : MonoBehaviour
{
    private EnemyAnimationController _enemy;

    void Start()
    {
        _enemy = GetComponent<EnemyAnimationController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _enemy.PlayAttack();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _enemy.PlayHit();
        }
    }
}