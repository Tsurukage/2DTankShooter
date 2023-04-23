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
    public UnityEvent OnHealthEmpty;
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
        {
            var speed = transform.GetComponent<Patrolling>();
            if (speed != null)
                speed.Speed = 0;
            OnHealthEmpty?.Invoke();
            StartCoroutine(Dead());
        }
        else
        {
            OnHit?.Invoke();
        }
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        OnDead?.Invoke();
        CameraEffects.ShakeOnce(1f, 10f, Vector3.one);
    }
}
