using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBadgeDrop : MonoBehaviour
{
    public EnemyScriptableObject _enemySO;

    public void SetTankData(EnemyScriptableObject data)
    {
        _enemySO = data;
    }
    public void SetBadgeDrop()
    {
        var manager = FindObjectOfType<SimpleGame>();
        manager.UpdateBadgeCount(_enemySO.badgeDrop);
    }
}
