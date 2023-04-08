using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabs;

    private void Start()
    {
    }
    public void Spawn(int index, Vector2 pos)
    {
        var obj = Instantiate(_prefabs[index]);
        obj.transform.localPosition = pos;
    }
}
