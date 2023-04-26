using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public Sprite enemySprite;
    public string enemyName;
    public float enemySpeed = 0;
    public int maxHealth = 100;
    public int badgeDrop = 1;

    public EnemyScriptableObject(string enemyName, float enemySpeed, int maxHealth)
    {
        this.enemyName = enemyName;
        this.enemySpeed = enemySpeed;
        this.maxHealth = maxHealth;
    }
}
