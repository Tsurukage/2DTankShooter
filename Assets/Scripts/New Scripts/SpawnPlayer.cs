using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = new Vector2(0, -5.5f);
        SpawnPlayerPrefab(pos);
    }

    void SpawnPlayerPrefab(Vector2 position)
    {
        var obj = Instantiate(_playerPrefab);
        obj.transform.position = position;
    }
}
