using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionPlayerRank : MonoBehaviour
{
    public static SectionPlayerRank SPRankInstance;
    private Player Player => Game.World.Player;
    [SerializeField] private Text text_rankPos;
    [SerializeField] private Text text_name;
    [SerializeField] private Image img_icon;
    [SerializeField] private Image img_gender;
    [SerializeField] private Text text_nationality;
    [SerializeField] private Image img_rank;
    [SerializeField] private Text text_rankName;
    [SerializeField] private Text text_badge;

    private Sprite[] sprites_avatar;
    private Sprite[] sprites_gender;
    [SerializeField] private Sprite[] sprite_rank;
    [SerializeField] private string[] rank_name;
    void Awake()
    {
        SPRankInstance = this;
        sprites_avatar = Resources.LoadAll<Sprite>("profile_pictures");
        sprites_gender = Resources.LoadAll<Sprite>("gender");
    }
    public void SetPlayerRankComp(int rankPos, string name, int avatar, int gender, string nationality, int rank, int badge)
    {
        SetRankPos(rankPos);
        SetName(name);
        SetAvatar(avatar);
        SetGender(gender);
        SetNationality(nationality);
        SetRank(rank);
        SetBadge(badge);
    }
    void SetRankPos(int rankPos) => text_rankPos.text = rankPos.ToString();
    void SetName(string name) => text_name.text = name;
    void SetAvatar(int avatar) => img_icon.sprite = sprites_avatar[avatar];
    void SetGender(int gender)
    {
        if (gender == 0)
            img_gender.sprite = null;
        else
            img_gender.sprite = sprites_gender[gender - 1];
    }
    void SetNationality(string nationality) => text_nationality.text = nationality;
    void SetRank(int rank)
    {
        int spritePick = 0;
        if (rank < 3)
            spritePick = 0;
        else if (rank > 2 && rank < 6)
            spritePick = 1;
        else if (rank > 5 && rank < 9)
            spritePick = 2;
        else if (rank > 8 && rank < 12)
            spritePick = 3;
        else if (rank > 11 && rank < 15)
            spritePick = 4;
        else if (rank > 14 && rank < 18)
            spritePick = 5;
        else if (rank == 18)
            spritePick = 6;
        else if (rank == 19)
            spritePick = 7;
        img_rank.sprite = sprite_rank[spritePick];
        text_rankName.text = rank_name[rank];
    }
    void SetBadge(int badge) => text_badge.text = badge.ToString();
}
