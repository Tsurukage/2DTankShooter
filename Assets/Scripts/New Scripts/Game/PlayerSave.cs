using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class PlayerSave
{
    public string Uid { get; set; }
    public string Name { get; set; }
    public string Nationality { get; set; }
    public int Badge { get; set; }
    public Rank Rank { get; set; }
    public Gender Gender { get; set; }
    public int Avatar { get; set; }

    //public int Diamond { get; set; }

    public PlayerSave(Player p)
    {
        Uid = p.Uid;
        if(p.Name == string.Empty)
            Name = p.Uid;
        else
            Name = p.Name;
        Nationality = p.Nationality;
        Badge = p.Badge;
        Rank = p.Rank;
        Gender = p.Gender;
        Avatar = p.Avatar;
        //Diamond = p.Diamond;
    }
    public PlayerSave()
    {

    }
    public Player ToModel()
    {
        return new Player(this);
    }
}
