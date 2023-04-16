using System;
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
    public UnityEvent<float> OnReloading;

    public event Action<bool> OnCountDown;
    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }
    private void Start()
    {
        OnReloading?.Invoke(currentDelay);
    }
    private void Update()
    {
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay);
            if(currentDelay <= 0)
            {
                canShoot = true;
                OnCountDown?.Invoke(canShoot);
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
                OnShoot?.Invoke();
                OnReloading?.Invoke(currentDelay);
            }
            else
            {
                OnCantShoot?.Invoke();
            }
        }
        else
            print("炮弹库存为0");
    }
}
