using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankRankBadge : MonoBehaviour
{
    [SerializeField] private GameObject _blueStar;
    [SerializeField] private GameObject _yellowStar;

    
    public void SetBlueStar()
    {
        _blueStar.SetActive(true);
    }
    public void SetYellowStar()
    {
        _yellowStar.SetActive(true);
    }
}
