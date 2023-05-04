using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankRankBadge : MonoBehaviour
{
    [SerializeField] private GameObject _redStar;
    [SerializeField] private GameObject _yellowStar;

    
    public void SetRedStar()
    {
        _redStar.SetActive(true);
    }
    public void SetYellowStar()
    {
        _yellowStar.SetActive(true);
    }
}
