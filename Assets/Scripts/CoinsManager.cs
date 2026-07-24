using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int СoinsOnScene { get; private set; }
    public int СoinsCollected { get; private set; }

    private List<Coin> _coins = new List<Coin>();

    private void Start()
    {
        Coin[] foundCoins = GetComponentsInChildren<Coin>(true);

        _coins.AddRange(foundCoins);

        СoinsOnScene = _coins.Count;
    }

    private void Update()
    {
        for (int i = _coins.Count - 1; i >= 0; i--)
        {
            if (_coins[i] && _coins[i].gameObject.activeSelf == false)
            {
                _coins.RemoveAt(i);
                
                СoinsOnScene = _coins.Count;
                
                СoinsCollected++;
            }
        }
    }
}