using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private GameObject go_tankBullet;
    [SerializeField] private Transform target;
    [SerializeField] private TankAim turret;
    [SerializeField] private Transform barrel;

    // Start is called before the first frame update
    void Start()
    {

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
        turret.Aim(target);
    }
}
