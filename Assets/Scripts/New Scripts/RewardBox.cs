using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardBox : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();

    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(SpawnLoot());
    }
    IEnumerator SpawnLoot()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        OnHit?.Invoke();
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
