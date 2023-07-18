using Models;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField]private SaveManager saveMgr;
    [SerializeField]private AdAgent adAgent;
    public static GameWorld World { get; private set; }
    public static SaveManager SaveManager { get; private set; }
    public static AdAgent AdAgent { get; private set; }
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
        AdAgent = adAgent;
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
            player = new Player("T201", "Player", "China", 0, 0, 0, 0);
            World.SetPlayer(player);
            Save();
        }
        World.SetPlayer(player);
    }
    public void SetNewPlayer(string uid, string name, string nation, int badge, Rank rank, Gender gender, int avatar)
    {
        var player = new Player(uid, name, nation, badge, rank, gender, avatar);
        World.SetPlayer(player);
        Save();
    }
}
