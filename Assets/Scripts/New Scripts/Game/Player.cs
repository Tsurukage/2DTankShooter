using System;
using System.Linq;
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
        public Rank Rank { get; set; }
        public  Gender Gender { get; set; }
        public int Avatar { get;set; }
        //public int Diamond { get; set; }

        internal Player(string uid, string name, string nationality, int badge, Rank rank, Gender gender, int avatar)
        {
            Uid = uid;
            Name = name;
            Nationality = nationality;
            Badge = badge;
            Rank = (Rank)rank;
            Gender = gender;
            Avatar = avatar;
            //Diamond = diamond;
        }
        public Player(PlayerSave p)
        {
            Uid = p.Uid;
            Name = p.Name;
            Nationality = p.Nationality;
            Badge = p.Badge;
            //Diamond = p.Diamond;
            Rank = p.Rank;
            Avatar = p.Avatar;
            Gender = p.Gender;
        }
        internal void AddBadge(int badge)
        {
            if (badge == 0) return;
            var last = Badge;
            Badge += badge;
            Debug.Log($"Badge [{badge}] added to [{last}], total: {Badge}");
        }
        //public void AddDiamond(int diamond)
        //{
        //    var last = Diamond;
        //    Diamond += diamond;
        //    Debug.Log($"Badge [{diamond}] added to [{last}], total: {Diamond}");
        //}
        public void SetRank(int rank)
        {
            var last = (int)Rank;
            Rank += rank;
            int lowestRankValue = Enum.GetValues(typeof(Rank)).Cast<int>().Min();
            int highestRankValue = Enum.GetValues(typeof(Rank)).Cast<int>().Max();

            if (Rank < (Rank)lowestRankValue) Rank = (Rank)lowestRankValue;
            if (Rank > (Rank)highestRankValue) Rank = (Rank)highestRankValue;
            if(last > (int)Rank)
            {
                Debug.Log($"Player down level from [{(Rank)last}] to [{Rank}]");
            }
            if(last < (int)Rank)
            {
                Debug.Log($"Player level up from [{(Rank)last}] to [{Rank}]");
            }
        }
        public PlayerSave ToSave()
        {
            return new PlayerSave(this);
        }
    }
}
public enum Rank
{
    BronzeLow = 0,
    BronzeMid = 1,
    BronzeHigh = 2,
    SilverLow = 3,
    SilverMid = 4,
    SilverHigh = 5,
    GoldLow = 6,
    GoldMid = 7,
    GoldHigh = 8,
    PlatinumLow = 9,
    PlatinumMid = 10,
    PlatinumHigh = 11,
    DiamondLow = 12,
    DiamondMid = 13,
    DiamondHigh = 14,
    MasterLow = 15,
    MasterMid = 16,
    MasterHigh = 17,
    Challenger = 18,
    GrandMaster = 19
}
public enum Gender
{
    Unknown = 0,
    Female = 1,
    Male = 2
}