using Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameWorld World { get; private set; }
    private MobileJoystick joystick;
    public GameState State;

    public static event Action<GameState, float> OnStateChange;
    private void Awake() => Instance = this;
    private void Start()
    {
        World = new GameWorld();
        World.SetPlayer(new Player("uid3991", "îLêÂ", "Malaysia", 0, 60, 8));
        UpdateGameState(GameState.StagePrepareUI);
        joystick = FindObjectOfType<MobileJoystick>();
    }
    public void UpdateGameState(GameState gameState, float delay = 0)
    {
        State = gameState;

        switch (gameState)
        {
            case GameState.StagePrepareUI:
                break;
            case GameState.StageInProgress:
                break;
            case GameState.StageChancesUI:
                break;
            case GameState.StageClearUI:
                break;
            case GameState.StageFailUI:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
        OnStateChange?.Invoke(gameState, delay);
    }

    public void HandleStageClear(bool active)
    {
        if (joystick != null)
            joystick.SetInteraction(active);
    }

    public void NextStage()
    {
        var rank = PlayerPrefs.GetInt("rank");
        SceneAssetsManager.Instance.LoadScene((Rank)rank);
    }
    public void HomeScene()
    {
        SceneManager.LoadScene(0);
    }
    
}
public enum GameState
{
    StagePrepareUI,
    StageInProgress,
    StageChancesUI,
    StageClearUI,
    StageFailUI
}
