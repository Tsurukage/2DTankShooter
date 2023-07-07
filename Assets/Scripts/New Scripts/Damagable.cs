using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int health = 0;
    private int damageValue;
    private TankObject tankObj;
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
        tankObj = GetComponent<TankObject>();
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
            var col2D = GetComponent<Collider2D>();
            col2D.enabled = false;
            OnHealthEmpty?.Invoke();
            if (tankObj != null)
                tankObj.UpdateTankState(TankState.HealthEmpty);
            var speed = transform.GetComponent<Patrolling>();
            if (speed != null)
                speed.Speed = 0;
            SoundEffectManager.Instance.StopThirdSFX();
            StartCoroutine(Dead());
        }
        else
        {
            OnHit?.Invoke();
        }
        ToogleDarkness.Instance.ToggleToLight();
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        OnDead?.Invoke();
        SoundEffectManager.Instance.StopLoopThirdSFX();
        if (tankObj != null)
        {
            tankObj.UpdateTankState(TankState.Destroyed);
        }
    }
    public void CameraShake()
    {
        CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.3f, 0.3f));
    }
}
