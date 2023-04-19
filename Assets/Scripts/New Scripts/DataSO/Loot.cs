using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "奖励", menuName = "奖励/炮弹奖励")]
public class Loot : ScriptableObject
{
    public string lootName;
    public int dropChance;
    public BulletData data;

    public Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
