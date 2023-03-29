using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAYING = 0,
    PAUSED  = 1,
    OVER    = 2
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State { get; internal set; } = GameState.PLAYING;

    void Awake() => Instance = this;

    public void GameOver()
    {
        State = GameState.OVER;

        FloorRiser.Instance.ToggleRising();
        UIManager .Instance.GameOver();
    }
}

