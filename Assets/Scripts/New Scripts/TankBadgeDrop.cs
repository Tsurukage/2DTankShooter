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
    int blueConvertValue = 4;
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
        var blueStar = arg / blueConvertValue;
        var yelloStar = arg % blueConvertValue;
        for(var i = 0; i < blueStar; i++ )
        {
            CreateBlueBadge();
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

    public void CreateBlueBadge()
    {
        GameObject newBadge = Instantiate(rankIcon, _starHolder);
        var blueStar = newBadge.GetComponent<TankRankBadge>();
        blueStar.SetBlueStar();
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
