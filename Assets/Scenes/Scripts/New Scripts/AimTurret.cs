using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    public float turretRotationSpeed = 150;

    public void Aim(Vector2 inputPointerPosition)
    {
        var desiredAngle = Mathf.Atan2(inputPointerPosition.y, inputPointerPosition.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turretRotationSpeed);
    }
}
