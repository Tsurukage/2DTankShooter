using System;
using Models;
using System.Collections.Generic;

[Serializable]
public class NPCDataList
{
    public List<Player> npcDataList;
    public NPCDataList(List<Player> npcDataList)
    {
        this.npcDataList = npcDataList;
    }
}
