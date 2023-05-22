using Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private MobileJoystick joystick;
    public GameState State;

    public static event Action<GameState, float> OnStateChange;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        UpdateGameState(State);
        joystick = FindObjectOfType<MobileJoystick>();
    }
    public void UpdateGameState(GameState gameState, float delay = 0)
    {
        State = gameState;

        switch (gameState)
        {
            case GameState.InMainMenu:
                break;
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
        UpdateGameState(GameState.StagePrepareUI);
        var player = Game.World.Player;
        SceneAssetsManager.Instance.LoadScene(player.Rank);
    }
    public void HomeScene()
    {
        SceneManager.LoadScene(0);
        UpdateGameState(GameState.InMainMenu);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
public enum GameState
{
    InMainMenu,
    StagePrepareUI,
    StageInProgress,
    StageChancesUI,
    StageClearUI,
    StageFailUI
}
