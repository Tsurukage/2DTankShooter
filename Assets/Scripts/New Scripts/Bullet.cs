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
    public UnityEvent OnDestroy = new UnityEvent();

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
        OnDestroy?.Invoke();
    }
    void OnBecameInvisible()
    {
        bulletOutOfbound = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit?.Invoke();
        if (bulletData.splashRange > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, bulletData.splashRange);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<Damagable>();
                if (enemy != null)
                {
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var disance = Vector2.Distance(closestPoint, transform.position);
                    var damagePercentage = Mathf.InverseLerp(bulletData.splashRange, 0, disance);
                    enemy.Hit((int)(bulletData.damage * damagePercentage));
                }
            }

        }
        else
        {
            var damagable = collision.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.Hit(bulletData.damage);
            }
        }
        DisableObject();
    }
    private void OnDrawGizmos()
    {
        if(bulletData != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, bulletData.splashRange);
        }
    }
}
