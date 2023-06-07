using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    public float acceleration;
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
        if (_trailLineObj != null)
            Instantiate(_trailLineObj, transform);
    }
    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if(conquaredDistance >= bulletData.maxDistance || bulletOutOfbound)
        {
            DisableObject();
            ToogleDarkness.Instance.ToggleToLight();
            OnHit?.Invoke();
        }
    }
    private void FixedUpdate()
    {
        rb2d.AddForce(transform.up * acceleration, ForceMode2D.Force);
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
        CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.1f, 0.1f));//击中目眮E
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
                        var reward = hitCollider.GetComponent<RewardBox>();
                        if(reward != null)
                        {
                            reward.Looting();
                        }
                    }
                    DisableObject();
                }
                break;
            case BulletType.Penetrate:
                var penDamagable = collider.GetComponent<Damagable>();
                if (penDamagable != null)
                {
                    CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.1f, 0.1f));//穿透时的击中
                    penDamagable.Hit(damage);
                }
                break;
            case BulletType.ReflectBullet:
                var trigger = GetComponent<Collider2D>();
                if (collider.tag != "Obstacle")
                {
                    trigger.isTrigger = true;
                    var reflDamagable = collider.GetComponent<Damagable>();
                    if (reflDamagable != null)
                    {
                        reflDamagable.Hit(damage);
                    }
                    CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.1f, 0.1f));//反弹时的击中
                    DisableObject();
                }
                else
                    trigger.isTrigger = false;
                break;
            case BulletType.StopTank:
                if (collider.tag == "Enemy")
                {
                    var speed = collider.GetComponent<Patrolling>();
                    if (speed != null)
                    {
                        speed.Speed = 0;
                    }
                    DisableObject();
                }
                else
                {
                    var stbullet = collider.GetComponent<Damagable>();
                    if (stbullet != null)
                        stbullet.Hit(damage);
                    CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.1f, 0.1f));//只击中有效目眮E
                    bulletOutOfbound = true;
                }
                break;
            case BulletType.SlowTank:
                if (collider.tag == "Enemy")
                {
                    var slow = collider.GetComponent<Patrolling>();
                    if (slow != null)
                    {
                        slow.Speed = (slow.Speed / 2);
                    }
                    DisableObject();
                }
                else
                {
                    var sltbullet = collider.GetComponent<Damagable>();
                    if (sltbullet != null)
                        sltbullet.Hit(damage);
                    CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.1f, 0.1f));//只击中有效目眮E
                    bulletOutOfbound = true;
                }
                break;
            case BulletType.TankOnly:
                if (collider.tag == "Enemy")
                {
                    var slow = collider.GetComponent<Patrolling>();
                    if (slow != null)
                    {
                        slow.Speed = (slow.Speed / 2);
                    }
                    DisableObject();
                }
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
