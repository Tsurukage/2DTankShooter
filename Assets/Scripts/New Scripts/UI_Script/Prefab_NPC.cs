using UnityEngine;
using UnityEngine.UI;

public class Prefab_NPC : MonoBehaviour
{
    [SerializeField] private Text text_rankPos;
    [SerializeField] private Text text_name;
    [SerializeField] private Image img_icon;
    [SerializeField] private Text text_nationality;
    [SerializeField] private Image img_rank;
    [SerializeField] private Text text_level;
    [SerializeField] private Text text_badge;

    private Sprite[] sprite_avatars;
    private Sprite[] sprite_rank;
    void Awake()
    {
        sprite_avatars = Resources.LoadAll<Sprite>("profile_pictures");
    }
    public void SetRankPos(int rankPos) => text_rankPos.text = rankPos.ToString();
    public void SetName(string name) => text_name.text = name;
    public void SetIcon(int avatar) => img_icon.sprite = sprite_avatars[avatar];
    public void SetNation(string nation) => text_nationality.text = nation;
    public void SetRank(int rank)
    {
        //img_rank.sprite = sprite_rank[rank];
        text_level.text = rank.ToString();
    }
    public void SetBadge(int badge) => text_badge.text = badge.ToString();
}
