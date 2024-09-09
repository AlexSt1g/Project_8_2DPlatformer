using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinsParent;

    private Coin[] _coins;

    private void Awake()
    {
        _coins = new Coin[_coinsParent.childCount];

        for (int i = 0; i < _coins.Length; i++)        
            _coins[i] = _coinsParent.GetChild(i).GetComponent<Coin>();    
    }

    private void OnEnable()
    {        
        foreach (var coin in _coins)
            coin.Collected += RespawnCoin;
    }

    private void OnDisable()
    {        
        foreach (var coin in _coins)
            coin.Collected -= RespawnCoin;
    }

    private void RespawnCoin(Coin coin, float respawnTime)
    {
        coin.gameObject.SetActive(false);

        StartCoroutine(SpawnWithDelay(coin, respawnTime));
    }

    private IEnumerator SpawnWithDelay(Coin coin, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        coin.gameObject.SetActive(true);
    }
}
