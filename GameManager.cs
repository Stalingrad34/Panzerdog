using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{  
    [SerializeField] private int _totalEnemys = 30;
    [SerializeField] private int _numberOfWaves = 3;
    private EnemySpawner _enemySpawner = null;
    private int currentNumberEnemys = 0;
    public int TotalEnemys => _totalEnemys;  
    public int CurrentWave { get; private set; } = 1;


    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        Messenger.AddListener(GameEvent.ENEMY_KILL, ChangeEnemyCounter);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, GameOver);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        Messenger.Broadcast(GameEvent.NEW_WAVE);
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3.0f);
        currentNumberEnemys = _totalEnemys;
        StartCoroutine(_enemySpawner.SpawnEnemy(_totalEnemys));
    }   

    public void ChangeEnemyCounter()
    {
        currentNumberEnemys--;       
        if (currentNumberEnemys == 0 && CurrentWave < _numberOfWaves)
        {
            Invoke("NextWave", 3.0f);
        }

        if (currentNumberEnemys == 0 && CurrentWave == _numberOfWaves)
        {
            Messenger.Broadcast(GameEvent.WIN);
            Time.timeScale = 0.0f;
        }
    }

    public void NextWave()
    {
        _enemySpawner.Restart();
        CurrentWave++;
        Messenger.Broadcast(GameEvent.NEW_WAVE);
        Messenger.Broadcast(GameEvent.WAVE_COMPLETE);
        StartCoroutine(StartGame());

    }

    private void GameOver()
    {
        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_KILL, ChangeEnemyCounter);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, GameOver);
    }
}
