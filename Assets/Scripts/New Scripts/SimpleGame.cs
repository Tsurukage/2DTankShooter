using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleGame : MonoBehaviour
{
    [SerializeField] List<EnemyComp> _enemyComp;
    [SerializeField] private int shootingCount = 3;
    [SerializeField] private int tankCount;
    [SerializeField] private GameObject _tankPrefab;
    public Transform _canvas, _stageClearPanel, _gameOverPanel;

    public UnityEvent<string> OnTankUpdate = new UnityEvent<string>();
    public UnityEvent<string> OnShootingUpdate = new UnityEvent<string>();
    // Start is called before the first frame update
    void Awake()
    {
        tankCount = _enemyComp.Count;
        OnTankUpdate?.Invoke(tankCount.ToString());
        OnShootingUpdate?.Invoke(shootingCount.ToString());
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
        }
    }
    public void UpdateTankCount()
    {
        tankCount--;
        OnTankUpdate?.Invoke(tankCount.ToString());
        CheckGame();
    }
    public void UpdateShootingCount()
    {
        shootingCount--;
        OnShootingUpdate?.Invoke(shootingCount.ToString());
        CheckGame();
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
}
[Serializable]
public class EnemyComp
{
    public string _name;
    public EnemyScriptableObject _enemySO;
    public Vector2 position;
}
