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

    private void Start()
    {
        Spawn();
        if (_bulletData.Count > 0)
            ChangeBulletData(0);
    }
    public void Spawn()
    {
        var bullet = new List<(Sprite icon, int index)>();
        for (int i = 0; i < _bulletData.Count; i++)
        {
            var blt = _bulletData[i];
            bullet.Add((blt.bulletIcon, blt.damage));

            //int buttonIndex = i;
            //var obj = Instantiate(_prefabBtn, _prefabParent);
            //var childObj = obj.transform.GetChild(0);
            //childObj.GetComponent<Image>().sprite = _bulletData[i].bulletIcon;
            //obj.onClick.AddListener(() => ChangeBulletData(buttonIndex));
        }
        for (int i = 0; i < bullet.Count; i++)
        {
            var bul = bullet[i];
            var index = i;
            var obj = Instantiate(_prefabBtn, _prefabParent);
            var childObj = obj.transform.GetChild(0);
            childObj.GetComponent<Image>().sprite = bul.icon;
            obj.onClick.AddListener(() => ChangeBulletData(index));
        }
    }

    private void ChangeBulletData(int buttonIndex)
    {
        _turretData.bulletData = _bulletData[buttonIndex];
    }

    public void SetButtonList()
    {
        Remove();
        Spawn();
    }
    public void Remove()
    {
        for (int i = 0; i < _prefabParent.childCount; i++)
        {
            var childObj = _prefabParent.GetChild(i);
            Destroy(childObj.gameObject);
        }
    }

}
