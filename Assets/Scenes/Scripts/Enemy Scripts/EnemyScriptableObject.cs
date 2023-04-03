using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyType enemyType;
    public Color enemyColor;
    public GameObject enemyPrefab;
    [Range (0f, 0.5f)]
    public float enemySpeed = 0;
    public int maxHealth;
    public Condition con;
}

public enum EnemyType
{
    Simple,
    Armored
}
public enum Condition
{
    Normal,
    SlowDown,
    Freeze
}