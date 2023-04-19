using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLootAddToList : MonoBehaviour
{
    [SerializeField] BulletData data;
    private SpawnButtonManager spawnButtonManager;
    private void Awake()
    {
        spawnButtonManager = FindObjectOfType<SpawnButtonManager>();
    }
    public void SetLootInfo(BulletData bullet)
    {
        data = bullet;
    }
    public void AddToList()
    {
        print("Add to Button List");
        StartCoroutine(SpawnBullet());
        Destroy(gameObject);
    }
    IEnumerator SpawnBullet()
    {
        spawnButtonManager.AddToList(data);
        yield return new WaitForSeconds(2f);
    }
}
