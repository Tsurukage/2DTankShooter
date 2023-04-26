using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBadgeReduce : MonoBehaviour
{
    [SerializeField] private int _reducedValue;

    public void DecreaseBadge()
    {
        var manager = FindObjectOfType<SimpleGame>();
        manager.UpdateBadgeCount(_reducedValue);
    }
}
