using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public event EventHandler OnPauseChanged;
    public event EventHandler OnGameOverEnter;
    public event EventHandler OnCountDownChanged;
    public static GameHandler Instance { get; private set; }
    private enum GameState
    {
        waitingToStart,
        countDownToPlay,
        Playing,
        gameOver
    }
    private GameState state;

    private float waitTime = 1f;
    private float countdownTime = 3f;
    private float playingTime = 100f;
    private float playingTimeMax = 100f;
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        state = GameState.waitingToStart;
    }
    private void Start()
    {
        InputScript.instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ChangePause();
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.waitingToStart:
                waitTime -= Time.deltaTime;
                if (waitTime < 0f)
                {
                    state = GameState.countDownToPlay;
                }
                break;
            case GameState.countDownToPlay:
                countdownTime -= Time.deltaTime;
                if (countdownTime < 0f)
                {
                    state = GameState.Playing;
                    OnCountDownChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.Playing:
                playingTime -= Time.deltaTime;
                if (playingTime < 0f)
                {
                    OnGameOverEnter?.Invoke(this, EventArgs.Empty);
                    state = GameState.gameOver;
                }
                break;
            case GameState.gameOver:
                break;
        }
    }


    public bool IsGamePlaying()
    {
        return state == GameState.Playing;
    }
    public bool IsCountDown()
    {
        return state == GameState.countDownToPlay;
    }
    public float ReturnCountdownTime()
    {
        return countdownTime;
    }
    public float ReturnPlayTime()
    {
        return 1 - (playingTime / playingTimeMax);
    }
    public bool ReturnPause()
    {
        return isPaused;
    }
    public void ChangePause() 
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f ;
        OnPauseChanged?.Invoke(this, EventArgs.Empty);
    }
}
