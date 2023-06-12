using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMoveSpeed : MonoBehaviour
{
    private float speed = 0;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private SpriteRenderer sprite_tank;
    public float moveRange;
    private int randValue;
    public bool moveRight;
    void Start()
    {
        sprite_tank = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
