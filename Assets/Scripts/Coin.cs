using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float _respawnTime = 20f;

    public event Action<Coin, float> Collected;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            Collected?.Invoke(this, _respawnTime);
    }
}
