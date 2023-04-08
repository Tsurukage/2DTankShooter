using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonManager : MonoBehaviour
{
    [SerializeField] private Button _prefabBtn;
    [SerializeField] private TurretData _turretData;
    [SerializeField] private BulletData[] _bulletData;
    [SerializeField] private Transform _prefabParent;

    private void Start()
    {
        Spawn(3);
    }
    public void Spawn(int bulletNo)
    {
        for(int i =  0; i < bulletNo; i++)
        {
            int buttonIndex = i;
            var obj = Instantiate(_prefabBtn, _prefabParent);
            obj.GetComponent<Image>().sprite = _bulletData[i].bulletSprite;
            obj.onClick.AddListener(() => ChangeBulletData(buttonIndex));
        }
    }

    private void ChangeBulletData(int buttonIndex)
    {
        _turretData.bulletData = _bulletData[buttonIndex];
    }

    public void SetButtonList()
    {

    }

}
