using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int health = 0;
    private int damageValue;

    public int DamageValue
    {
        get { return damageValue; }
        set { damageValue = value; }
    }
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
        MaxHealth = Health;
        OnHealthChange?.Invoke((float)Health / MaxHealth);
        if(health == 0)
            Health = MaxHealth;
    }

    public void Hit(int damageppoint)
    {
        DamageValue = damageppoint;
        Health -= damageppoint;
        if (health <= 0)
            //OnDead?.Invoke();
            StartCoroutine(Dead());
        else
        {
            OnHit?.Invoke();
        }
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
        OnDead?.Invoke();
        var tankCount = FindObjectOfType<GameController>();
        tankCount.TankCount();
    }
}
