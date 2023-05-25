using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
    public interface IPlayer
    {
        Rank Rank { get; }
        int Badge { get; }
    }
    [Serializable]
    public class Player : IPlayer
    {
        public string Uid{ get; set; }
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
            Rank = (Rank)rank;
        }
        public Player(PlayerSave p)
        {
            Uid = p.Uid;
            Name = p.Name;
            Nationality = p.Nationality;
            Badge = p.Badge;
            Diamond = p.Diamond;
            Rank = p.Rank;
        }
        internal void AddBadge(int badge)
        {
            if (badge == 0) return;
            //Badge = PlayerPrefs.GetInt("badge");
            var last = Badge;
            Badge += badge;
            Debug.Log($"Badge [{badge}] added to [{last}], total: {Badge}");
            //PlayerPrefs.SetInt("badge", Badge);
        }
        public void AddDiamond(int diamond)
        {
            var last = Diamond;
            Diamond += diamond;
            Debug.Log($"Badge [{diamond}] added to [{last}], total: {Diamond}");
        }
        public void SetRank(int rank)
        {
            var last = (int)Rank;
            Rank += rank;
            if (Rank < Rank.Bronze) Rank = Rank.Bronze;
            if (Rank > Rank.Mythic) Rank = Rank.Mythic;
            Debug.Log($"Player up level from [{(Rank)last}] to [{Rank}]");
        }
        public PlayerSave ToSave()
        {
            return new PlayerSave(this);
        }
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