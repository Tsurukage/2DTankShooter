using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int health = 0;
    public int Health
    {
        get { return health; }
        set 
        { 
            health = value;
            var enemy = GetComponent<Enemy>();
            OnHealthChange?.Invoke((float)Health/enemy.enemyScriptable.maxHealth);
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit;

    private void Start()
    {
        var enemy = GetComponent<Enemy>();
        if(health == 0)
            Health = enemy.enemyScriptable.maxHealth;
    }

    public void Hit(int damageppoint)
    {
        Health -= damageppoint;
        if (health <= 0)
            OnDead?.Invoke();
        else
            OnHit?.Invoke();
    }
}
