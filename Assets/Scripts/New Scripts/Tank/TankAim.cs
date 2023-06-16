using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour
{
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }
    public void Aim(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        float desiredAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
        transform.rotation = targetRotation;
    }
    public void ResetAim()
    {
        transform.rotation = originalRotation;
    }
}
