using UnityEngine;
using UnityEngine.UI;

public class Prefab_NPC : MonoBehaviour
{
    [SerializeField] private Text text_rankPos;
    [SerializeField] private Text text_name;
    [SerializeField] private Image img_icon;
    [SerializeField] private Image img_gender;
    [SerializeField] private Text text_nationality;
    [SerializeField] private Image img_rank;
    [SerializeField] private Text text_level;
    [SerializeField] private Text text_badge;

    private Sprite[] sprite_avatars;
    private Sprite[] sprite_gender;
    [SerializeField] private Sprite[] sprite_rank;
    [SerializeField] private string[] rank_name;
    void Awake()
    {
        sprite_avatars = Resources.LoadAll<Sprite>("profile_pictures");
        sprite_gender = Resources.LoadAll<Sprite>("gender");
    }
    public void SetRankPos(int rankPos) => text_rankPos.text = rankPos.ToString();
    public void SetName(string name) => text_name.text = name;
    public void SetIcon(int avatar) => img_icon.sprite = sprite_avatars[avatar];
    public void SetGender(int gender)
    {
        if (gender == 0)
            img_gender.sprite = null;
        else
            img_gender.sprite = sprite_gender[gender-1];
    }
    public void SetNation(string nation) => text_nationality.text = nation;
    public void SetRank(int rank)
    {
        int spritePick = 0;
        if (rank < 3)
            spritePick = 0;
        else if (rank > 2 && rank < 6)
            spritePick = 1;
        else if (rank > 5 && rank < 9)
            spritePick = 2;
        else if(rank > 8 && rank < 12)
            spritePick = 3;
        else if(rank > 11 && rank < 15)
            spritePick = 4;
        else if(rank > 14 && rank < 18)
            spritePick = 5;
        else if(rank == 18)
            spritePick = 6;
        else if(rank == 19)
            spritePick = 7;
        img_rank.sprite = sprite_rank[spritePick];
        text_level.text = rank_name[rank];
    }
    public void SetBadge(int badge) => text_badge.text = badge.ToString();
}
