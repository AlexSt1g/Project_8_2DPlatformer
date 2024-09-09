using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Died += Die;
    }

    private void OnDisable()
    {
        _player.Died -= Die;
    }

    public void UpdateMove(bool isRunning, bool isGround)
    {        
        _animator.SetBool("IsRunning", isRunning);
        _animator.SetBool("IsGround", isGround);
    }    

    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    public void Revive()
    {
        _animator.SetTrigger("Revive");
    }

    private void Die()
    {
        _animator.SetTrigger("Die");
    }
}
