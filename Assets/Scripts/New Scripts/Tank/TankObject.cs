using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TankObject : MonoBehaviour
{
    [SerializeField] private EnemyAdvanceSO enemyAdSO;
    public TankState State;
    private bool isAttacking = false;
    [SerializeField] private Sprite[] tankSprite;

    public UnityEvent OnShoot;

    public void SetData(EnemyAdvanceSO data)
    {
        enemyAdSO = data;
        tankSprite[0] = enemyAdSO.tank_patrol;
        tankSprite[1] = enemyAdSO.tank_attack;
    }
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
                PanelWarningFlash.Instance.EndFlash();
                if (tankSprite != null)
                    sprite.sprite = tankSprite[0];
                if (speed != null)
                    speed.Speed = 0.5f;
                break;
            case TankState.SlowDown:
                break;
            case TankState.Attacking:
                PanelWarningFlash.Instance.StartFlash();
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
            for (int i = 0; i < enemyAdSO.shootingCount; i++)
            {
                if (GameManager.Instance.State == GameState.StageFailUI || GameManager.Instance.State == GameState.StageClearUI)
                {
                    break;
                }
                shoot.Shoot();
                OnShoot?.Invoke();
                yield return new WaitForSeconds(2f);
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
