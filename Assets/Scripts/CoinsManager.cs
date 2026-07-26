using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int CoinsOnScene { get; private set; }
    public int CoinsCollected { get; private set; }

    private List<Coin> _allCoins = new List<Coin>();
    private List<Coin> _activeCoins = new List<Coin>();

    private void Start()
    {
        _allCoins.AddRange(GetComponentsInChildren<Coin>(true));

        RestartAllCoins();
    }

    private void Update()
    {
        if (CoinsOnScene <= 0)
            return;
        
        for (int i = _activeCoins.Count - 1; i >= 0; i--)
        {
            if (_activeCoins[i].gameObject.activeSelf == false)
            {
                _activeCoins.RemoveAt(i);
                
                CoinsOnScene--;
                CoinsCollected++;
            }
        }
    }
    
    public void RestartAllCoins()
    {
        _activeCoins.Clear();
        
        foreach (Coin coin in _allCoins)
        {
            coin.gameObject.SetActive(true);
            _activeCoins.Add(coin);
        }
        
        CoinsOnScene = _allCoins.Count;
        CoinsCollected = 0;
    }
}