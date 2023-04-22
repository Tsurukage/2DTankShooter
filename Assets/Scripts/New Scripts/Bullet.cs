using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    private GameObject _trailLineObj;
    private float splashRange;
    private SpriteRenderer bulletSprite;
    private Vector2 startPosition;
    private float conquaredDistance = 0;
    private Rigidbody2D rb2d;
    bool bulletOutOfbound = false;

    public UnityEvent OnHit = new UnityEvent();
    public UnityEvent OnDestroy = new UnityEvent();
    public UnityEvent OnExplosionRange = new UnityEvent();

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

        splashRange = bulletData.splashRange;
        var explosion = GetComponentInChildren<InstantiateExplosionSplash>();
        if(explosion != null )
        {
            explosion.Radius = splashRange;
        }
        _trailLineObj = bulletData.bulletTrail;
        Instantiate(_trailLineObj, transform);
    }
    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if(conquaredDistance >= bulletData.maxDistance || bulletOutOfbound)
        {
            DisableObject();
            OnHit?.Invoke();
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
    void OnTriggerEnter2D(Collider2D collider)
    {
        OnHit?.Invoke();
        var bulletType = bulletData.bulletType;
        var damage = bulletData.damage;
        switch (bulletType)
        {
            case BulletType.SingleHit:
                var damagable = collider.GetComponent<Damagable>();
                if (damagable != null)
                    damagable.Hit(damage);
                bulletOutOfbound = true;
                break;
            case BulletType.Explosion:
                OnExplosionRange?.Invoke();
                if (bulletData.splashRange > 0)
                {
                    var hitColliders = Physics2D.OverlapCircleAll(transform.position, bulletData.splashRange);
                    foreach (var hitCollider in hitColliders)
                    {
                        var childDamagable = hitCollider.GetComponent<Damagable>();
                        if (childDamagable != null)
                        {
                            var closestPoint = hitCollider.ClosestPoint(transform.position);
                            var disance = Vector2.Distance(closestPoint, transform.position);
                            var damagePercentage = Mathf.InverseLerp(bulletData.splashRange, 0, disance);
                            childDamagable.Hit((int)(damage * damagePercentage));
                        }
                    }
                    DisableObject();
                }
                break;
            case BulletType.Penetrate:
                var penDamagable = collider.GetComponent<Damagable>();
                if (penDamagable != null)
                    penDamagable.Hit(damage);
                break;
            case BulletType.ReflectBullet:
                var trigger = GetComponent<Collider2D>();
                if (collider.tag != "Obstacle")
                {
                    trigger.isTrigger = true;
                    var reflDamagable = collider.GetComponent<Damagable>();
                    if(reflDamagable != null)
                    {
                        reflDamagable.Hit(damage);
                    }
                    DisableObject();
                }
                else
                    trigger.isTrigger = false;
                break;
            default:
                break;
        }
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
