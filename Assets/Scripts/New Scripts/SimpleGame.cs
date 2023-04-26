using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleGame : MonoBehaviour
{
    [SerializeField] List<EnemyComp> _enemyComp;
    [SerializeField] private GameObject _tankPrefab;
    public Transform _stageClearPanel, _gameOverPanel;
    //--For UI
    private Top_UI_Manager _manager;
    
    [SerializeField] private int shootingCount = 3;
    public int badgeCount = 0;

    [SerializeField] private int tankCount;

    // Start is called before the first frame update
    void Awake()
    {
        _manager = FindObjectOfType<Top_UI_Manager>();
        tankCount = _enemyComp.Count;
        _manager.SetTankCount(tankCount);
        _manager.SetShootCount(shootingCount);
        _manager.SetBadgeCount(badgeCount);
        
        SpawnCurrentLevel();
        _gameOverPanel.gameObject.SetActive(false);
        _stageClearPanel.gameObject.SetActive(false);
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
            enemyGO.GetComponentInChildren<TankBadgeDrop>()._enemySO = enemyComp._enemySO;
        }
    }
    public void UpdateTankCount()
    {
        tankCount--;
        _manager.SetTankCount(tankCount);
        CheckGame();
    }
    public void UpdateShootingCount()
    {
        shootingCount--;
        _manager.SetShootCount(shootingCount);
        CheckGame();
    }
    public void UpdateBadgeCount(int amount)
    {
        badgeCount += amount;
        _manager.SetBadgeCount(badgeCount);
    }
    public void CheckGame()
    {
        if (tankCount == 0)
        {
            Debug.Log("Stage Complete");
            _stageClearPanel.gameObject.SetActive(true);
        }
        else if (tankCount > 0 && shootingCount == 0)
        {
            Debug.Log("Game Over!");
            _gameOverPanel.gameObject.SetActive(true);
        }
    }
    public void OnAnimaHit()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
}
[Serializable]
public class EnemyComp
{
    public string _name;
    public EnemyScriptableObject _enemySO;
    public Vector2 position;
}
