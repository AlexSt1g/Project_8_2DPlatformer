using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerSpawner))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private InputReader _inputReader;
    private GroundDetector _groundDetector;
    private PlayerAnimationController _animator;
    private int _coinCount;
    private bool _isDead;

    public event Action<int> CoinCountChanged;
    public event Action Died;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponent<GroundDetector>();
        _animator = GetComponent<PlayerAnimationController>();
    }

    private void FixedUpdate()
    {
        if (_isDead == false)
        {
            if (_inputReader.Direction != 0)
            {
                _mover.Move(_inputReader.Direction);
            }

            if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            {
                _mover.Jump();
                _animator.Jump();
            }
        }
    }

    private void Update()
    {
        if (_isDead == false)
            _animator.UpdateMove(_inputReader.Direction != 0, _groundDetector.IsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            AddCoin();
            CoinCountChanged?.Invoke(_coinCount);
        }

        if (collision.TryGetComponent<DeathZone>(out _))        
            Die();
    }

    public void Revive()
    {
        _isDead = false;
        _animator.Revive();
    }

    private void Die()
    {
        _isDead = true;
        Died?.Invoke();
    }

    private void AddCoin()
    {
        _coinCount++;
    }
}
