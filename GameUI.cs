using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text uiPlayerHP = null;
    [SerializeField] private Text uiNumberWave = null;
    [SerializeField] private Text uiCounterEnemys = null;
    [SerializeField] private Text uiCountdownText = null;
    [SerializeField] private Image winMenu = null;
    [SerializeField] private Image gameOverMenu = null;
    private GameManager _gameManager = null;
    private int currentNumberEnemy = 0;

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
        Messenger.AddListener(GameEvent.NEW_WAVE, NewWave);
        Messenger.AddListener(GameEvent.ENEMY_KILL, ChangeEnemyCounterUI);      
        Messenger.AddListener(GameEvent.WAVE_COMPLETE, ChangeWaveUI);
        Messenger.AddListener(GameEvent.WIN, WinUI);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, GameOverUI);
    }

    private void NewWave()
    {
        gameOverMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        currentNumberEnemy = _gameManager.TotalEnemys;
        uiNumberWave.text = "Wave " + _gameManager.CurrentWave;
        uiCounterEnemys.text = _gameManager.TotalEnemys + "/" + _gameManager.TotalEnemys;
        uiCountdownText.gameObject.SetActive(true);

        int countdown = 3;
        while (countdown > 0)
        {
            uiCountdownText.text = countdown.ToString();
            countdown--;
            yield return new WaitForSeconds(1.0f);
        }

        uiCountdownText.gameObject.SetActive(false);
    }  

    private void ChangeEnemyCounterUI()
    {
        currentNumberEnemy--;
        string remained = currentNumberEnemy.ToString();
        string total = _gameManager.TotalEnemys.ToString();
        uiCounterEnemys.text = remained + "/" + total;
    }

    private void ChangeWaveUI()
    {
        string wave = _gameManager.CurrentWave.ToString();
        uiNumberWave.text = "Wave " + wave;
    }

    private void WinUI()
    {
        winMenu.gameObject.SetActive(true);
    }

    private void GameOverUI()
    {
        gameOverMenu.gameObject.SetActive(true);
    }

    public void ChangeHPText(float HP)
    {
        uiPlayerHP.text = HP.ToString();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_KILL, ChangeEnemyCounterUI);
        Messenger.RemoveListener(GameEvent.NEW_WAVE, NewWave);
        Messenger.RemoveListener(GameEvent.WAVE_COMPLETE, ChangeWaveUI);
        Messenger.RemoveListener(GameEvent.WIN, WinUI);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, GameOverUI);
    }


}
