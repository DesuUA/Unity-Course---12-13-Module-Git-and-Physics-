using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private float _gameTime = 60f;
    [SerializeField] private KeyCode _restartGameKey = KeyCode.R;

    public bool GameProcess { get; private set; } = true; //Game currently processed?
    public bool IsGameWin { get; private set; } = false; //false - gameLoose
    
    private float _timer;

    private void Start()
    {
        _timer = _gameTime;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(_restartGameKey))
            RestartGame();

        if (GameProcess == false)
            return;
        
        TimeUpdate();
        int timerInt = (int)_timer;
        Debug.Log($"Time remain: {timerInt} \nCoins remain: {_coinManager.CoinsOnScene}");
        
        if (_timer <= 0 && _coinManager.CoinsOnScene > 0)
            GameLoose();
        
        if ((_timer <= 0 && _coinManager.CoinsOnScene <= 0) || _coinManager.CoinsOnScene <= 0)
            GameWin();
    }

    private void GameWin()
    {
        _player.SetActive(false);
        
        IsGameWin = true;
        GameProcess = false;
        
        Debug.Log("Game won");
    }

    private void GameLoose()
    {
        _player.SetActive(false);
        
        IsGameWin = false;
        GameProcess = false;
        
        Debug.Log("Game Over");
    }

    private void RestartGame()
    {
        _player.SetActive(true);
        
        _coinManager.RestartAllCoins();
        
        _timer = _gameTime;
        IsGameWin = false;
        GameProcess = true;
    }

    private void TimeUpdate()
    {
        _timer -= Time.deltaTime;
    }
}
