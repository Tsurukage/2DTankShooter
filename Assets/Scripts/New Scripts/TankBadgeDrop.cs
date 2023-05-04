using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBadgeDrop : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject _enemySO;
    [SerializeField] private GameObject rankIcon;
    [SerializeField] private Transform _starHolder;
    List<TankRankBadge> _badges = new List<TankRankBadge>();
    int redConvertValue = 4;
    private void Start()
    {
        DrawRankIcon(_enemySO.badgeDrop);
    }
    public void SetTankData(EnemyScriptableObject data)
    {
        _enemySO = data;
    }
    public void SetBadgeDrop()
    {
        var manager = FindObjectOfType<SimpleGame>();
        manager.UpdateBadgeCount(_enemySO.badgeDrop);
    }

    public void DrawRankIcon(int arg)
    {
        ClearHeart();
        var redStar = arg / redConvertValue;
        var yelloStar = arg % redConvertValue;
        for(var i = 0; i < redStar; i++ )
        {
            CreateredBadge();
        }
        for(var i = 0; i < yelloStar; i++ )
        {
            CreateYellowBadge();
        }
    }

    private void CreateYellowBadge()
    {
        GameObject newBadge = Instantiate(rankIcon, _starHolder);
        var yellowStar = newBadge.GetComponent<TankRankBadge>();
        yellowStar.SetYellowStar();
    }

    public void CreateredBadge()
    {
        GameObject newBadge = Instantiate(rankIcon, _starHolder);
        var redStar = newBadge.GetComponent<TankRankBadge>();
        redStar.SetRedStar();
        TankRankBadge rankBadge = newBadge.GetComponent<TankRankBadge>();
        _badges.Add(rankBadge);
    }
    public void ClearHeart()
    {
        foreach(Transform transform in _starHolder)
        {
            Destroy(transform.gameObject);
        }
        _badges = new List<TankRankBadge>();
    }
}
