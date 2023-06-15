using System;
using System.Collections;
using UnityEngine;

public class TankObject : MonoBehaviour
{
    public TankState State;
    private bool isAttacking = false;
    [SerializeField] private Sprite[] tankSprite;
    public void UpdateTankState(TankState state)
    {
        State = state;
        var speed = GetComponent<Patrolling>();
        var sprite = GetComponent<SpriteRenderer>();
        switch (state)
        {
            case TankState.Idle:
                if (speed != null)
                    speed.Speed = 0f;
                break;
            case TankState.Patrolling:
                if (tankSprite != null)
                    sprite.sprite = tankSprite[0];
                if (speed != null)
                    speed.Speed = 0.5f;
                break;
            case TankState.SlowDown:
                break;
            case TankState.Attacking:
                if (tankSprite != null)
                    sprite.sprite = tankSprite[1];
                if (!isAttacking)
                {
                    StartCoroutine(FireRoutine());
                }
                break;
            case TankState.Reloading:
                break;
            case TankState.Destroyed:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(StartAttackAfterDelay());
    }
    IEnumerator StartAttackAfterDelay()
    {
        UpdateTankState(TankState.Idle);
        yield return new WaitForSeconds(1f);
        UpdateTankState(TankState.Attacking);
    }
    IEnumerator FireRoutine()
    {
        isAttacking = true;
        var shoot = GetComponent<TankAttack>();
        yield return new WaitForSeconds(5f);
        if (shoot != null)
        {
            for (int i = 0; i < 3; i++)
            {
                {
                    shoot.Shoot();
                    yield return new WaitForSeconds(2f);
                }
            }
        }
        yield return new WaitForSeconds(2f);
        isAttacking = false;
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
