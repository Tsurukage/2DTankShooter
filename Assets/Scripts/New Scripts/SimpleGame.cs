using Models;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGame : MonoBehaviour
{
    public static SimpleGame Instance;
    private Player Player => Game.World.Player;
    [SerializeField] List<EnemyComp> _enemyComp;
    [SerializeField] List<EnemyAdvanceComp> _enemyCounterComp;
    [SerializeField] private GameObject _tankPrefab;
    [SerializeField] private GameObject _tankCounterPrefab;
    [SerializeField] private string _stageName;
    [SerializeField] private AudioClip _audioClip;
    public string Stage_Name
    {
        get { return _stageName; }
        set { _stageName = value; }
    }
    [SerializeField] private int countDown = 0;
    public int CountDown
    {
        get { return countDown; }
        set { countDown = value; }
    }
    //--For game condition
    [SerializeField] private int tankCount;
    [SerializeField] private int shootingCount = 3;
    [SerializeField] private int badgeCount = 0;
    [SerializeField] private int animalCount = 3;
    
    private int InitAnimalCount;
    private bool chanceUsed = false;
    private static int winStreak = 0;
    private static int loseStreak = 0;

    public static event Action<int, int, int, int> Top_UI;
    public static event Action<bool> StarOne;
    public static event Action<bool> StarTwo;
    public static event Action<bool> StarThree;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        tankCount = _enemyComp.Count;
        Top_UI?.Invoke(tankCount, shootingCount, badgeCount, animalCount);
        InitAnimalCount = animalCount;
        SpawnCurrentLevel();
        SpawnCounterTank();
        chanceUsed = false;
        BackgroundMusicManager.Instance.SetBgm(_audioClip);
    }
    private void SpawnCurrentLevel()
    {
        foreach(EnemyComp enemyComp in _enemyComp)
        {
            GameObject enemyGO = Instantiate(_tankPrefab);
            enemyGO.transform.position = enemyComp.position;
            enemyGO.GetComponentInChildren<SpriteRenderer>().sprite = enemyComp._enemySO.enemySprite;
            enemyGO.GetComponentInChildren<Patrolling>().Speed = enemyComp._enemySO.enemySpeed;
            enemyGO.GetComponentInChildren<Damagable>().Health = enemyComp._enemySO.maxHealth;
            enemyGO.GetComponentInChildren<TankBadgeDrop>().SetTankData(enemyComp._enemySO);
        }
    }
    private void SpawnCounterTank()
    {
        foreach(EnemyAdvanceComp enemyCounterComp in _enemyCounterComp)
        {
            GameObject enemyCGO = Instantiate(_tankCounterPrefab);
            enemyCGO.transform.position = enemyCounterComp.position;
            enemyCGO.GetComponentInChildren<SpriteRenderer>().sprite = enemyCounterComp._enemyAdSO.tank_patrol;
            enemyCGO.GetComponentInChildren<Patrolling>().Speed = enemyCounterComp._enemyAdSO.tank_speed;
            enemyCGO.GetComponentInChildren<Damagable>().Health = enemyCounterComp._enemyAdSO.tank_maxHealth;
            enemyCGO.GetComponentInChildren<TankBadgeDrop>().SetAdTankData(enemyCounterComp._enemyAdSO);
            enemyCGO.GetComponentInChildren<TankObject>().SetData(enemyCounterComp._enemyAdSO);
        }
    }
    public void UpdateTankCount()
    {
        tankCount--;
        UpdateCount();
    }
    public void UpdateShootingCount()
    {
        shootingCount--;
        UpdateCount();
        CheckGame();
    }
    public void UpdateAnimalChanceCount()
    {
        animalCount--;
        UpdateCount();
    }
    public void UpdateBadgeCount(int amount)
    {
        badgeCount += amount;
        UpdateCount();
    }
    public void UpdateCount()
    {
        Top_UI?.Invoke(tankCount, shootingCount, badgeCount, animalCount);
    }
    public void CheckGame()
    {
        if (tankCount == 0 || tankCount < 0)
        {
            GameManager.Instance.HandleStageClear(false);
            Debug.Log("Stage Complete");
            GameManager.Instance.UpdateGameState(GameState.StageClearUI, 4);
            FinalBadge(badgeCount);
            print(badgeCount);
        }
        if (shootingCount == 0)
        {
            if (tankCount > 0)
            {
                if (!chanceUsed)
                {
                    GameManager.Instance.UpdateGameState(GameState.StageChancesUI, 1);
                }
                else
                {
                    GameManager.Instance.HandleStageClear(false);
                    FinalBadge(badgeCount);
                    GameManager.Instance.UpdateGameState(GameState.StageFailUI, 4);
                    Debug.Log("Game Over!");
                }
            }
        }
        if (animalCount == 0)
        {
            GameManager.Instance.HandleStageClear(false);
            print(badgeCount);
            FinalBadge(badgeCount);
            Debug.Log("不要滥杀动物！");
            GameManager.Instance.UpdateGameState(GameState.StageFailUI, 4);
        }
        StarOne?.Invoke(tankCount == 0);
        StarTwo?.Invoke(shootingCount > 0);
        StarThree?.Invoke(animalCount == InitAnimalCount);
    }
    public void SetBool()
    {
        chanceUsed = true;
    }
    public void CheckStreak(int win = 0, int lose = 0)
    {
        winStreak += win;
        loseStreak += lose;
        print($"{winStreak}, {loseStreak}");
        if (winStreak == 3 && loseStreak == 0)
        {
            Player.SetRank(1);
            print(Player.Rank);
            winStreak = 0;
        }
        if(winStreak > 0 && loseStreak > 0)
        {
            winStreak = 0;
            loseStreak = 0;
        }
        if(loseStreak == 1 && winStreak == 0)
        {
            Player.SetRank(-1);
            loseStreak = 0;
        }
    }
    public void IncreaseShootingCount(int count)
    {
        shootingCount += count;
        Top_UI?.Invoke(tankCount, shootingCount, badgeCount, animalCount);
    }
    public void FinalBadge(int value)
    {
        Player.AddBadge(value);
    }
}
[Serializable]
public class EnemyComp
{
    public string _name;
    public EnemyScriptableObject _enemySO;
    public Vector2 position;
}
[Serializable]
public class EnemyAdvanceComp
{
    public string _name;
    public EnemyAdvanceSO _enemyAdSO;
    public Vector2 position;
}
