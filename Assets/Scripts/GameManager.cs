using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameMode { Wait, Move, Finish };
public enum gameStatus { Normal, Gravity };

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public static Action onGameStateChanged;
    public static Action onLevelFinished;

    [Header("GameModes")]
    public GameMode gameMode;
    public gameStatus inGameStatus;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    private void Start()
    {
        onGameStateChanged += OnGameStateChanged;
        inGameStatus = gameStatus.Normal;

        SetGameMode(GameMode.Wait);
    }
    private void OnDestroy()
    {
        onGameStateChanged -= OnGameStateChanged;

    }

    public gameStatus ChangeGameStatus(gameStatus gameStatus)
    {
        return inGameStatus = gameStatus;
    }
    public GameMode SetGameMode(GameMode GameMode)
    {
        gameMode = GameMode;
        onGameStateChanged?.Invoke();
        return gameMode;
    }
    void OnGameStateChanged()
    {
        if(gameMode == GameMode.Finish)
        {
            onLevelFinished?.Invoke();
        }
    }
}
