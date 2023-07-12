using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSFX : MonoBehaviour
{
    [SerializeField] private InstantiateUtil instantiate;
    // Start is called before the first frame update
    void Start()
    {
        instantiate = GetComponent<InstantiateUtil>();
        instantiate.SendThirdSFX();
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(instantiate.clip.length);
        Destroy(gameObject);
    }
}
