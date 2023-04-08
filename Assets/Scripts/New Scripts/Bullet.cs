using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private SpriteRenderer bulletSprite;
    private Vector2 startPosition;
    private float conquaredDistance = 0;
    private Rigidbody2D rb2d;
    bool bulletOutOfbound = false;

    public UnityEvent OnHit = new UnityEvent();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bulletSprite = GetComponent<SpriteRenderer>();
    }
    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        startPosition = transform.position;
        rb2d.velocity = transform.up * this.bulletData.speed;
        bulletSprite.sprite = this.bulletData.bulletSprite;
    }
    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if(conquaredDistance >= bulletData.maxDistance || bulletOutOfbound)
        {
            DisableObject();
        }
    }
    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        bulletOutOfbound = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print($"Collide {collision.name}");
        OnHit?.Invoke();
        var damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            damagable.Hit(bulletData.damage);
        }
        DisableObject();
    }
}
