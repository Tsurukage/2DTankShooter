using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    private TankObject tankObj;
    [SerializeField] private GameObject go_tankBullet;
    [SerializeField] private GameObject target;
    [SerializeField] private TankAim turret;
    [SerializeField] private Transform barrel;

    // Start is called before the first frame update
    void Start()
    {
        tankObj = GetComponent<TankObject>();
        target = GameObject.FindWithTag("Player");
    }

    public void Shoot()
    {
        var bullet = Instantiate(go_tankBullet);
        bullet.transform.position = barrel.position;
        bullet.transform.localRotation = barrel.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (tankObj.State == TankState.Attacking)
        {
            if (target != null)
                turret.Aim(target.transform);
        }
        else
            turret.ResetAim();
    }
}
