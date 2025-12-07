using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayIdle() 
    {

    }

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void PlayHit()
    {
        _animator.SetTrigger("Hit");
    }

    public void PlayDie()
    {
        _animator.SetTrigger("Die");
    }
}
