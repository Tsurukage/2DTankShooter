using Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    private Player Player => Game.World.Player;
    [SerializeField] private Text name_player;
    [SerializeField] private Image img_player;
    [SerializeField] private Image img_playerRank;
    [SerializeField] private Text text_diamond;
    [SerializeField] private Text text_badge;

    //Test Reset
    [SerializeField] private Button ResetPlayerPref;
    [SerializeField] private Sprite[] img_rankSprite;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayer();
        //var badge = PlayerPrefs.GetInt("badge");
        //SetPlayerBadge(badge);
        if (ResetPlayerPref != null)
            ResetPlayerPref.onClick.AddListener(ResetPlayerPrefs);
        //var rank = PlayerPrefs.GetInt("rank");
        //SetPlayerRank(rank);
    }
   
    public void SetPlayer()
    {
        SetName(Player.Name);
        SetPlayerBadge(Player.Badge);
        SetPlayerRank((int)Player.Rank);
        SetPlayerDiamond(Player.Diamond);
    }
    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("badge");
        var badge = PlayerPrefs.GetInt("badge");
        SetPlayerBadge(badge);
    }

    public void SetName(string name) => name_player.text = name;
    public void SetPlayerIcon(Sprite icon) => img_player.sprite = icon;
    public void SetPlayerRank(int rank) => img_playerRank.sprite = img_rankSprite[rank];
    public void SetPlayerDiamond(int value) => text_diamond.text = value.ToString();
    public void SetPlayerBadge(int value) => text_badge.text = value.ToString();
}
