using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public AimTurret aimTurret;
    public Turret[] turrets;
    public Laser laser;

    public void Start()
    {
        if (aimTurret == null)
            aimTurret = GetComponentInChildren<AimTurret>();
        if(turrets == null || turrets.Length == 0)
            turrets = GetComponentsInChildren<Turret>();
        if(laser == null)
            laser = GetComponentInChildren<Laser>();
    }
    public void HandleShoot()
    {
        foreach (var turret in turrets)
        {
            turret.Shoot();
        }
        laser.EnableLine(false);
    }
    public void HandleTurretMovement(Vector2 rotateVec)
    {
        aimTurret.Aim(rotateVec);
        laser.EnableLine(true);
    }
}
