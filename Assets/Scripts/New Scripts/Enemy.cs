using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] List<EnemyScriptableObject> _enemyList = new List<EnemyScriptableObject>();
    void Start()
    {
        foreach(EnemyScriptableObject enemy in _enemyList)
        {
            GameObject enemyGO = Instantiate(_enemyPrefab);
            enemyGO.transform.rotation = Quaternion.identity;
            enemyGO.GetComponentInChildren<SpriteRenderer>().sprite = enemy.enemySprite;
            enemyGO.GetComponentInChildren<Patrolling>().Speed = enemy.enemySpeed;
            enemyGO.GetComponentInChildren<Damagable>().Health = enemy.maxHealth;
        }
    }
}
