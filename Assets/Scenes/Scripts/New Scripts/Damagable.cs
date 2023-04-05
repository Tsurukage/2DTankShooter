using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] private int health = 0;
    public int Health
    {
        get { return health; }
        set 
        { 
            health = value;
            OnHealthChange?.Invoke((float)Health/MaxHealth);
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit;

    private void Start()
    {
        var enemy = GetComponent<Enemy>();
        if(health == 0)
            Health = MaxHealth;
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
