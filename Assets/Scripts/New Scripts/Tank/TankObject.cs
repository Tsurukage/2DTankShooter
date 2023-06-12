using System;
using System.Collections;
using UnityEngine;

public class TankObject : MonoBehaviour
{
    public TankState State;
    private bool isAttacking = false;
    public void UpdateTankState(TankState state)
    {
        State = state;
        var speed = GetComponent<Patrolling>();
        switch (state)
        {
            case TankState.Idle:
                if (speed != null)
                    speed.Speed = 0f;
                break;
            case TankState.Patrolling:
                if (speed != null)
                    speed.Speed = 0.5f;
                break;
            case TankState.SlowDown:
                break;
            case TankState.Attacking:
                    StartCoroutine(FireRoutine());
                break;
            case TankState.Reloading:
                break;
            case TankState.Destroyed:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    IEnumerator FireRoutine()
    {
        for(int i = 0; i < 3; i++)
        {
            var shoot = GetComponent<TankAttack>();
            if (shoot != null)
            {
                shoot.Shoot();
                yield return new WaitForSeconds(3f);
            }
        }
        UpdateTankState(TankState.Idle);
        CheckHealth();
    }
    void CheckHealth()
    {
        var health = GetComponent<Damagable>();
        if (health != null)
        {
            var hp = health.Health;
            if (hp > 0)
                UpdateTankState(TankState.Patrolling);
            if (hp <= 0)
                UpdateTankState(TankState.Destroyed);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(StartAttackAfterDelay());
    }
    IEnumerator StartAttackAfterDelay()
    {
        UpdateTankState(TankState.Idle);
        yield return new WaitForSeconds(5f);
        UpdateTankState(TankState.Attacking);
    }
}
public enum TankState
{
    Idle,
    Patrolling,
    SlowDown,
    Attacking,
    Reloading,
    Destroyed
}
