using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;
    public TurretData turretData;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;
    
    public UnityEvent OnShoot, OnCantShoot;
    //public UnityEvent<float> OnReloading;

    public event Action<bool> OnCountDown;
    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }
    private void Start()
    {
        //OnReloading?.Invoke(currentDelay);
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.StageInProgress)
        {
            if (canShoot == false)
            {
                GameManager.Instance.HandleStageClear(false);
                currentDelay -= Time.deltaTime;
                //OnReloading?.Invoke(currentDelay);
                if (currentDelay <= 0)
                {
                    canShoot = true;
                    GameManager.Instance.HandleStageClear(true);
                    //OnCountDown?.Invoke(canShoot);
                }
            }
        }
    }
    public void SetBulletData(BulletData bulletdata)
    {
        turretData.bulletData = bulletdata;
    }
    public void Shoot()
    {
        if (turretData.bulletData != null)
        {
            if (canShoot)
            {
                canShoot = false;
                currentDelay = turretData.reloadDelay;
                OnCountDown?.Invoke(canShoot);
                if (turretData.bulletData.doubleFire)
                {
                    StartCoroutine(ShootDoubleBulletsWithDelay());
                }
                else
                {
                    foreach (var barrel in turretBarrels)
                    {
                        GameObject bullet = Instantiate(turretData.bulletPrefab);
                        bullet.transform.position = barrel.position;
                        bullet.transform.localRotation = barrel.rotation;
                        bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);
                        foreach (var collider in tankColliders)
                        {
                            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                        }
                    }
                }
                OnShoot?.Invoke();
                //OnReloading?.Invoke(currentDelay);
            }
            else
            {
                OnCantShoot?.Invoke();
            }
        }
        else
            print("炮弹库存为0");
    }

    IEnumerator ShootDoubleBulletsWithDelay()
    {
        foreach (var barrel in turretBarrels)
        {
            // Shoot first bullet
            GameObject bullet1 = Instantiate(turretData.bulletPrefab);
            bullet1.transform.position = barrel.position;
            bullet1.transform.localRotation = barrel.rotation;
            bullet1.GetComponent<Bullet>().Initialize(turretData.bulletData);

            foreach (var collider in tankColliders)
            {
                Physics2D.IgnoreCollision(bullet1.GetComponent<Collider2D>(), collider);
            }

            yield return new WaitForSeconds(turretData.bulletData.doubleFireDelay); // Wait for double fire delay

            // Shoot second bullet
            GameObject bullet2 = Instantiate(turretData.bulletPrefab);
            bullet2.transform.position = barrel.position;
            bullet2.transform.localRotation = barrel.rotation;
            bullet2.GetComponent<Bullet>().Initialize(turretData.bulletData);

            foreach (var collider in tankColliders)
            {
                Physics2D.IgnoreCollision(bullet2.GetComponent<Collider2D>(), collider);
            }

            yield return new WaitForSeconds(turretData.bulletData.doubleFireDelay); // Wait for double fire delay
        }

        yield break;
    }
}
