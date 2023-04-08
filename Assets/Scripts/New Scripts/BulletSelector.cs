using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSelector : MonoBehaviour
{
    Turret bulletData;
    [SerializeField] BulletData data;
    private void Start()
    {
        bulletData = FindObjectOfType<Turret>();
    }
    public void SetCurrentBullet()
    {
        bulletData.SetBulletData(data);
    }
}
