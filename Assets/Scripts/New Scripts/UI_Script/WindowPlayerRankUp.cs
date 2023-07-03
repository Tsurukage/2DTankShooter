using UnityEngine;
using UnityEngine.UI;

public class WindowPlayerRankUp : MonoBehaviour
{
    public static WindowPlayerRankUp instance;
    [SerializeField] private Image img_beforeRank;
    [SerializeField] private Image img_afterRank;
    [SerializeField] private Text text_beforeRank;
    [SerializeField] private Text text_afterRank;
    private static Image Img_beforeRank { get; set; }
    private static Image Img_afterRank { get; set; }
    private static Text Text_beforeRank { get; set; }
    private static Text Text_afterRank { get; set; }
    [SerializeField] private Sprite[] rankSprite;
    private static Sprite[] RankSprite { get; set; }
    [SerializeField] private string[] rankString;
    private static string[] RankString { get; set; }
    public void Display(bool display)
    {
        gameObject.SetActive(display);
    }
    void Start()
    {
        instance = this;
        Display(false);
        Img_beforeRank = img_beforeRank;
        Img_afterRank = img_afterRank;
        Text_beforeRank = text_beforeRank;
        Text_afterRank = text_afterRank;
        RankSprite = rankSprite;
        RankString = rankString;
    }
    public static void RankChange(int previousRank, int afterRank)
    {
        instance.Display(true);
        Img_beforeRank.sprite = RankSprite[previousRank];
        Text_beforeRank.text = RankString[previousRank];
        Img_afterRank.sprite = RankSprite[afterRank];
        Text_afterRank.text = RankString[afterRank];
    }
    
}
