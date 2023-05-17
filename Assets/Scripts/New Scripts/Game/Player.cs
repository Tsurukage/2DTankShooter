using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayer
{
    Rank Rank { get; }
    int Badge { get; }
}
public class Player : IPlayer
{
    public string Uid { get; set; }
    public string Name { get; set; }
    public string Nationality { get; set; }
    public int Badge { get; set; }
    public int Diamond { get; set; }
    public Rank Rank { get; set; }

    internal Player(string uid, string name, string nationality, int badge, int diamond, Rank rank)
    {
        Uid = uid;
        Name = name;
        Nationality = nationality;
        Badge = badge;
        Diamond = diamond;
        Rank = rank;
    }

    public void AddBadge(int badge)
    {
        Badge = PlayerPrefs.GetInt("badge");
        Badge += badge;
        PlayerPrefs.SetInt("badge", Badge);
    }
    public void SetDiamond(int diamond)
    {
        Diamond = diamond;
    }
    public void AddDiamond(int diamond)
    {
        Diamond += diamond;
    }
    public void SetRank(int rank)
    {
        Rank = (Rank)PlayerPrefs.GetInt("rank");
        Rank += rank;
        if (Rank < Rank.Bronze) Rank = Rank.Bronze;
        if (Rank > Rank.Mythic) Rank = Rank.Mythic;
        PlayerPrefs.SetInt("rank", (int)Rank);
    }
}
public enum Rank
{
    Bronze = 0,
    Silver = 1,
    Gold = 2,
    Platinum = 3,
    Diamond = 4,
    Master = 5,
    Grandmaster = 6,
    Legend = 7,
    Mythic = 8
}
public enum GameLevel
{
    LevelOne,
    LevelTwo,
    LevelThree,
    LevelFour,
    LevelFive,
    LevelSix,
    LevelSeven,
    LevelEight,
    LevelNight
}