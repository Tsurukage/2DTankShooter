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
    public int Diamond { get; set; }
    public Rank Rank { get; set; }

    public PlayerSave(Player p)
    {
        Uid = p.Uid;
        Name = p.Name;
        Nationality = p.Nationality;
        Badge = p.Badge;
        Diamond = p.Diamond;
        Rank = p.Rank;
    }
    public PlayerSave()
    {

    }
    public Player ToModel()
    {
        return new Player(this);
    }
}
