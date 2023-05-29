using Models;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField]private SaveManager saveMgr;
    public static GameWorld World { get; private set; }
    public static SaveManager SaveManager { get; private set; }
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
        SaveManager = saveMgr;
        Load();
    }
    public static void Save()
    {
        SaveManager.SaveToFile(World.Player.ToSave());
    }
    void Load()
    {
        var player = SaveManager.LoadPlayer()?.ToModel();
        if (player == null)
        {
            player = new Player("uid3991", "Leo", "Malaysia", 0, 0, 0);
            World.SetPlayer(player);
            Save();
        }
        World.SetPlayer(player);
    }
}
