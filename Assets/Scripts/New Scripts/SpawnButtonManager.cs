﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonManager : MonoBehaviour
{
    [SerializeField] private Button _prefabBtn;
    [SerializeField] private TurretData _turretData;
    [SerializeField] List<BulletData> _bulletData = new List<BulletData>();
    [SerializeField] private Transform _prefabParent;

    [SerializeField] private ScrollRect _scrollRect;
    private int SelectedIndex { get; set; }
    private void Start()
    {
        Spawn();
        if (_bulletData.Count > 0)
            ChangeBulletData(SelectedIndex);
    }
    //为了显示当前选择的炮弹
    private void Update()
    {
        if (SelectedIndex < _bulletData.Count)
            ChangeBulletData(SelectedIndex);
        else if (SelectedIndex >= _bulletData.Count) ChangeBulletData(SelectedIndex - 1);
    }
    public void Spawn()
    {
        var bullets = new List<(Sprite icon, Sprite gradeBase ,int index, string name)>();
        for (int i = 0; i < _bulletData.Count; i++)
        {
            var blt = _bulletData[i];
            bullets.Add((blt.bulletIcon, blt.bulletGradeBase, blt.damage, blt.bulletName));
        }
        ClearChild();
        for (int i = 0; i < bullets.Count; i++)
        {
            var bullet = bullets[i];
            var index = i;
            var obj = Instantiate(_prefabBtn, _prefabParent);
            obj.GetComponent<Image>().sprite = bullet.gradeBase;
            var text = obj.GetComponentInChildren<Text>();
            text.text = bullet.name;
            var childObj = obj.transform.Find("img_bullet");
            childObj.GetComponent<Image>().sprite = bullet.icon;
            obj.onClick.AddListener(() => ChangeBulletData(index));
            obj.onClick.AddListener(PlaySoundEffect);
        }
    }

    private void PlaySoundEffect()
    {
        InteractionSoundManager.Instance.OnClickSound();
    }

    private void ChangeBulletData(int buttonIndex)
    {
        _turretData.bulletData = _bulletData[buttonIndex];
        for (int i = 0; i < _prefabParent.childCount; i++)
        {
            var btn = _prefabParent.GetChild(i);
            var isSelected = i == buttonIndex;
            btn.Find("img_selected").gameObject.SetActive(isSelected);
            SelectedIndex = buttonIndex;
        }
    }

    public void Remove()
    {
        if (_bulletData.Count > 0)
        {
            if (SelectedIndex > 0)
            {
                _bulletData.RemoveAt(SelectedIndex);
                var childObject = _prefabParent.GetChild(SelectedIndex).gameObject;
                Destroy(childObject);
            }
        }
        Spawn();
    }
    void ClearChild()
    {
        if (_prefabParent.childCount > 0)
        {
            for (int i = 0; i < _prefabParent.childCount; i++)
            {
                var childObj = _prefabParent.GetChild(i).gameObject;
                Destroy(childObj);
            }
        }
    }
    public void AddToList(BulletData data)
    {
        _bulletData.Add(data);
        Spawn();
    }
}
