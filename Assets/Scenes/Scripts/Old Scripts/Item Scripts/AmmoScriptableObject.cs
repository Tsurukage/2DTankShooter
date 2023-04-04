using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb", menuName = "Bombs/Bomb", order = 0)]
public class AmmoScriptableObject : ScriptableObject
{
    public AmmoType type;
    public string ammon_name;
    public GameObject modelPrefab;
    public float ammoSpeed;
    public float ammoDamage;
    public float impactField;
    public Color ammoColor;

    public Rigidbody2D rb;
    public Vector2 lastVelocity;

}
