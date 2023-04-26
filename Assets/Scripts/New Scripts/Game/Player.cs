using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Badge { get; set; }
    public Tier tier { get; set; }
}
public enum Tier
{
    Recruit,
    Private,
    Gefreiter,
    Coporal,
    Master_Coporal,
    Sergeant,
    Staff_Sergeant,
    Master_Sergeant,
    First_Sergeant,
    Sergeant_Major
}