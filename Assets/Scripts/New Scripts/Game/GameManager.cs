using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnStateChange;
    private void Awake() => Instance = this;
    private void Start()
    {
        UpdateGameState(GameState.StagePrepareUI);
    }
    public void UpdateGameState(GameState gameState)
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
        OnStateChange?.Invoke(gameState);
    }
    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
