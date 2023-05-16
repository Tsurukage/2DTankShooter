using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private MobileJoystick joystick;
    public GameState State;
    private int randomIndex;

    public static event Action<GameState, float> OnStateChange;
    private void Awake() => Instance = this;
    private void Start()
    {
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
        Player player = new Player();
        switch (player.Rank)
        {
            case Rank.Bronze:
                break;
            case Rank.Silver:
                break;
            case Rank.Gold:
                break;
            case Rank.Platinum:
                break;
            case Rank.Diamond:
                break;
            case Rank.Master:
                break;
            case Rank.Grandmaster:
                break;
            case Rank.Legend:
                break;
            case Rank.Mythic:
                break;
        }
        randomIndex = Random.Range(1, 17);
        SceneManager.LoadScene(randomIndex);
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
