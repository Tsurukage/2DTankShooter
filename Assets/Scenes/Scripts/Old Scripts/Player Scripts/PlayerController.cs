using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100, bulletSpeed = 100;
    private Transform handPos;
    private Transform firePos1;
    public GameObject bullet;

    public int reflections;
    public float maxLength;
    private LineRenderer rayLine;
    private Ray2D ray;
    private RaycastHit2D raycastHit;
    void Awake()
    {
        handPos = GameObject.Find("turretHead").transform;
        firePos1 = GameObject.Find("FirePos1").transform;
        rayLine = GameObject.Find("turretHead").GetComponent<LineRenderer>();
        rayLine.enabled = false;
    }
    void Update()
    {
        ray = new Ray2D(firePos1.position, firePos1.up);
        rayLine.positionCount = 1;
        rayLine.SetPosition(0, firePos1.position);

        raycastHit = Physics2D.Raycast(firePos1.position, firePos1.up);
        //print(raycastHit.collider);

        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            rayLine.positionCount += 1;
            if (Physics2D.Raycast(ray.origin, ray.direction, remainingLength))
            {
                rayLine.SetPosition(rayLine.positionCount - 1, raycastHit.point);
                remainingLength -= Vector2.Distance(ray.origin, raycastHit.point);
                ray = new Ray2D(raycastHit.point, Vector2.Reflect(raycastHit.point, raycastHit.normal));
                if (raycastHit.collider.tag != "Wall")
                    break;
            }
            else
            {
                rayLine.positionCount += 1;
                rayLine.SetPosition(rayLine.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
    public void GetAmmo(GameObject ammo, float ammoSpeed)
    {
        bullet = ammo;
        bulletSpeed = ammoSpeed;
    }

    public void Aim(float x, float y)
    {
        float angle = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        handPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
        RayLine(true);
    }
    public void Shoot()
    {
        RayLine(false);
        GameObject b = Instantiate(bullet, firePos1.position, firePos1.transform.rotation);
        if (transform.localScale.x > 0)
            b.GetComponent<Rigidbody2D>().AddForce(firePos1.up * bulletSpeed, ForceMode2D.Impulse);
        else
            b.GetComponent<Rigidbody2D>().AddForce(-firePos1.up * bulletSpeed, ForceMode2D.Impulse);

        //handPos.rotation = Quaternion.identity;
    }
    public void RayLine(bool rendered)
    {
        rayLine.enabled = rendered;
        rayLine.SetPosition(0, firePos1.position);
        //rayLine.SetPosition(1, firePos2.position);
    }
    
}
