using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Counter", menuName = "Enemies/Enemy_Counter", order = 2)]
public class EnemyAdvanceSO : ScriptableObject
{
    public string tank_name;
    public float tank_speed;
    public int tank_maxHealth = 100;
    public int tank_badge = 1;
    public Sprite tank_patrol;
    public Sprite tank_attack;
    public int shootingCount = 1;
    public float attackDelay;
    public float attackInterval;
}
