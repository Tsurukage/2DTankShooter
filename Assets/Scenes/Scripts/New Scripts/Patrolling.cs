using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    public float speed = 0;
    private SpriteRenderer tankSprite;
    public float boundary;
    private int boolValue;
    public bool moveRight;
    // Start is called before the first frame update
    void Start()
    {
        tankSprite = GetComponent<SpriteRenderer>();
        speed = Random.Range(0.3f, 0.5f);
        boolValue = Random.Range(1, 4);
        if (boolValue < 3) moveRight = false;
        if (boolValue > 2) moveRight = true;
        boundary = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRight)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            tankSprite.flipX = moveRight;
            if (transform.position.x > boundary)
                moveRight = false;
        }
        else
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            tankSprite.flipX = moveRight;
            if (transform.position.x < -boundary)
                moveRight = true;
        }
    }
}
