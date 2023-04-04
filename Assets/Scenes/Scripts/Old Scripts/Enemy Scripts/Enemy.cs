using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyScriptable;
    public float speed;
    public float waitTime = 1;
    public Transform[] moveSpots;
    private int randomSpot;
    private Animator animator;

    //public List<Transform> movePoints = new List<Transform>();

    public bool moveRight;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = enemyScriptable.enemyColor;
        //StartCoroutine(Move());
        animator = GetComponent<Animator>();
        speed = enemyScriptable.enemySpeed;
    }
    void Update()
    {
        if(moveRight)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if(transform.localPosition.x > 3.25)
            {
                moveRight = false;
                transform.localScale = new Vector3(1, 1);
            }
        }
        else
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            if(transform.localPosition.x < -3.25)
            {
                moveRight = true;
                transform.localScale = new Vector3(-1, 1);
            }
        }
    }
    IEnumerator Move()
    {
        while (true)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
            speed = enemyScriptable.enemySpeed;
            waitTime = Random.Range(0.1f, waitTime);
            while (Vector3.Distance(transform.position, moveSpots[randomSpot].position) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name.Contains("LWall"))
        {
            moveRight = true;
        }
        if (collision.transform.name.Contains("RWall"))
        {
            moveRight = false;
        }
    }
}
