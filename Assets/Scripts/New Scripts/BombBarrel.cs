using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombBarrel : MonoBehaviour
{
    [SerializeField] float _splashRange;
    [SerializeField] float _bombBarrelDamage;
    
    public UnityEvent OnExplode = new UnityEvent();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_splashRange > 0)
        {
            var hitCollider = Physics2D.OverlapCircleAll(transform.position, _splashRange);
            foreach(var hit in hitCollider)
            {
                var enemy = hit.GetComponent<Damagable>();
                if(enemy != null)
                {
                    var closestPoint = hit.ClosestPoint(transform.position);
                    var disance = Vector2.Distance(closestPoint, transform.position);
                    var damagePercentage = Mathf.InverseLerp(_splashRange, 0, disance);
                    enemy.Hit((int)(_bombBarrelDamage * damagePercentage));
                }
            }
        }
        OnExplode?.Invoke();
    }
    private void OnDrawGizmos()
    {
        if(_bombBarrelDamage > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _splashRange);
        }
    }
}
