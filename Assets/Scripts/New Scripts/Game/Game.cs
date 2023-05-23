using Models;
using System;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public static GameWorld World { get; private set; }
    [SerializeField] TextAsset playerDataTA;

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
        string[] data = playerDataTA.text.Split(",");
        Player player = new Player();
        player.Uid = data[0];
        player.Name = data[1];
        player.Nationality = data[2];
        player.Badge = Convert.ToInt32(data[3]);
        player.Rank = (Rank)Convert.ToInt32(data[4]);

        World = new GameWorld();
        World.SetPlayer(player);
        if (playerDataTA != null)
        {
        }
        print(player.Rank);
    }
}
