using Models;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    private Player Player => Game.World.Player;
    [SerializeField] private Text name_player;
    [SerializeField] private Image img_player;
    [SerializeField] private Image img_playerRank;
    [SerializeField] private Text text_rank;
    [SerializeField] private Text text_diamond;
    [SerializeField] private Text text_badge;

    public static PlayerUIManager Instance;
    //Test Reset
    [SerializeField] private Button ResetPlayerPref;
    [SerializeField] private Sprite[] img_rankSprite;
    [SerializeField] private string[] text_rankText;
    private Sprite[] sprite_avatars;
    void Awake()
    {
        WindowPlayerInfo.OnUpdate += SetPlayer;
        Window_AvatarLoader.OnAvatarUpdate += SetPlayer;
    }
    void OnDestroy()
    {
        WindowPlayerInfo.OnUpdate -= SetPlayer;
        Window_AvatarLoader.OnAvatarUpdate -= SetPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if (ResetPlayerPref != null)
            ResetPlayerPref.onClick.AddListener(ResetPlayerPrefs);
        sprite_avatars = Resources.LoadAll<Sprite>("profile_pictures");
        SetPlayer();
    }
   
    public void SetPlayer()
    {
        var name = (Player.Name == string.Empty)? Player.Uid : Player.Name;
        SetName(name);
        SetPlayerBadge(Player.Badge);
        var language = PlayerPrefs.GetInt("language");
        SetLanguage((Language)language);
        SetAvatar(sprite_avatars[Player.Avatar]);
        //SetPlayerDiamond(Player.Diamond);
    }
    private void ResetPlayerPrefs()
    {
        var clear = FindObjectOfType<SaveManager>();
        clear.Clear();
    }

    public void SetName(string name) => name_player.text = name;
    public void SetAvatar(Sprite icon) => img_player.sprite = icon;
    public void SetPlayerRank(int rank)
    {
        img_playerRank.sprite = img_rankSprite[rank];
        text_rank.text = text_rankText[rank];
    }
    public void SetPlayerDiamond(int value) => text_diamond.text = value.ToString();
    public void SetPlayerBadge(int value) => text_badge.text = value.ToString();

    public void SetLanguage(Language language)
    {
        if (language == Language.English)
            text_rankText = LoadTextFile("Localization/English/rank_names");
        else if(language == Language.Chinese)
            text_rankText = LoadTextFile("Localization/Chinese/rank_names");
        SetPlayerRank((int)Player.Rank);
    }

    private string[] LoadTextFile(string path)
    {
        TextAsset textAsset= Resources.Load<TextAsset>(path);
        return textAsset.text.Split('\n');
    }
}
