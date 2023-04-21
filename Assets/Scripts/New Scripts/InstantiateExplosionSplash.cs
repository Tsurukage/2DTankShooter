using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateExplosionSplash : MonoBehaviour
{
    [SerializeField] GameObject _explosionSplashPrefab;
    private float radius;
    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    public void InstanteSplash()
    {
        var splash = Instantiate(_explosionSplashPrefab);
        splash.transform.position = transform.parent.transform.position;
        splash.transform.localScale = new Vector3(Radius, Radius, Radius);
    }
}
