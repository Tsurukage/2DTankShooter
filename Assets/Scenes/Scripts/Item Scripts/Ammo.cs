using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Ammo : MonoBehaviour
{
    public Ammo_Selector selectedAmmo;
    public float impactField;
    public float force;
    public AudioEffect audioEffect;
    MovementJoystick joystick;

    public UnityEvent OnDestroy;
    public UnityEvent OnHit;
    // Update is called once per frame
    void Awake()
    {
        selectedAmmo = FindObjectOfType<Ammo_Selector>();
        audioEffect = FindObjectOfType<AudioEffect>();
        audioEffect.LaunchClip();
        joystick = FindObjectOfType<MovementJoystick>();
        joystick.MovementBool(false);
    }
    void Update()
    {
        selectedAmmo.lastVelocity = selectedAmmo.rigidbody2D.velocity;
        impactField = selectedAmmo.impactField;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = selectedAmmo.lastVelocity.magnitude;
        var direction = Vector3.Reflect(selectedAmmo.lastVelocity.normalized, collision.GetContact(0).point);
        selectedAmmo.lastVelocity = direction * Mathf.Max(speed, 0f);
        if(collision.transform.tag == "Enemy" || collision.transform.tag == "Obstacle")
        {
            audioEffect.ImpactClip();
            var bullet = transform.GetChild(0);
            Destroy(bullet.gameObject);
            joystick.MovementBool(true);
            OnDestroy?.Invoke();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        OnDestroy?.Invoke();
        joystick.MovementBool(true);
    }

    void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField);
        foreach(Collider2D obj in objects)
        {
            if(obj.transform.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * selectedAmmo.ammoDamage);
                var damagable = obj.GetComponent<Damagable>();
                if(damagable != null)
                    damagable.Hit((int)selectedAmmo.ammoDamage % 3);
            }
        }
        Destroy(gameObject, 0.1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }
}
