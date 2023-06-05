using System;
using Models;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class NPCDataList
{
    public List<PlayerSave> NPCList { get; set; }

    public NPCDataList(List<Player> npcList)
    {
        NPCList = npcList.Select(npc => new PlayerSave(npc)).ToList();
    }

    public List<Player> ToModelList()
    {
        return NPCList.Select(npcSave => npcSave.ToModel()).ToList();
    }
}
