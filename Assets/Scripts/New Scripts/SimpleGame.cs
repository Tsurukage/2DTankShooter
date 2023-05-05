using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleGame : MonoBehaviour
{
    public static SimpleGame Instance;

    [SerializeField] List<EnemyComp> _enemyComp;
    [SerializeField] private GameObject _tankPrefab;
    [SerializeField] private string _stageName;
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
    public void UpdateTankCount()
    {
        tankCount--;
        UpdateCount();
        CheckGame();
    }
    public void UpdateShootingCount()
    {
        shootingCount--;
        UpdateCount();
        CheckGame();
    }
    public void UpdateBadgeCount(int amount)
    {
        badgeCount += amount;
        UpdateCount();
    }
    public void UpdateAnimalChanceCount()
    {
        animalCount--;
        UpdateCount();
        CheckGame();
    }
    public void UpdateCount()
    {
        Top_UI?.Invoke(tankCount, shootingCount, badgeCount, animalCount);
    }
    public void CheckGame()
    {
        if (tankCount == 0)
        {
            Debug.Log("Stage Complete");
            GameManager.Instance.UpdateGameState(GameState.StageClearUI);
        }
        else if (tankCount > 0 && shootingCount == 0)
        {
            Debug.Log("Game Over!");
            GameManager.Instance.UpdateGameState(GameState.StageFailUI);
        }
        else if(animalCount == 0)
        {
            Debug.Log("不要滥杀动物！");
            GameManager.Instance.UpdateGameState(GameState.StageFailUI);
        }
        StarOne?.Invoke(tankCount == 0);
        StarTwo?.Invoke(shootingCount > 0);
        StarThree?.Invoke(animalCount == InitAnimalCount);
    }
}
[Serializable]
public class EnemyComp
{
    public string _name;
    public EnemyScriptableObject _enemySO;
    public Vector2 position;
}
