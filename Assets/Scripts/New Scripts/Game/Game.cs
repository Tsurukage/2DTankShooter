using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public static GameWorld World { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        World = new GameWorld();
        World.SetPlayer(new Player("uid3991", "äﬂâ∆1çÜ", "Malaysia", 0, 60, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
