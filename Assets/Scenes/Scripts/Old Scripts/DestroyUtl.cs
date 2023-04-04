using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUtl : MonoBehaviour
{
    private SimpleGame game;
    private void Start()
    {
        game = FindObjectOfType<SimpleGame>();
    }
    public void DestroyHelper()
    {
        print("TankDestroy");
        Destroy(gameObject);
        game.ShootingCD();
    }
}
