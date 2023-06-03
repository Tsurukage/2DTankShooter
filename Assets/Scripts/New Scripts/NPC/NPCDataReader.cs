using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDataReader : MonoBehaviour
{
    public TextAsset npcDataFile;
    public Dictionary<string, Player> npcPlayerDictionary = new Dictionary<string, Player>();
    void Start()
    {
        ReadNPCDataFromCSV();
    }

    private void ReadNPCDataFromCSV()
    {
        string[] lines = npcDataFile.text.Split("\n");
        for(int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if(string.IsNullOrEmpty(line)) continue;

            string[] data = line.Split(",");

            string npcID = data[0];
            string npcName = data[1];
            int npcGender = int.Parse(data[2]);
            string npcNationality = data[3];
            int npcAvatar = int.Parse(data[4]);
            int npcRank = int.Parse(data[5]);
            int npcBadge = int.Parse(data[6]);

            Player npcPlayer = new Player(npcID, npcName, npcNationality, npcBadge, (Rank)npcRank, (Gender)npcGender, npcAvatar);
            npcPlayerDictionary.Add(npcID, npcPlayer);
        }
        print(npcPlayerDictionary.Count);
        int random = UnityEngine.Random.Range(1, 11);
        Player selectedPlayer = GetNPC(random.ToString());
        string name = selectedPlayer.Name;
        print(name);
    }
    public Player GetNPC(string uid)
    {
        if(npcPlayerDictionary.TryGetValue(uid, out var npc)) return npc;
        Debug.LogError($"NPC{uid} is not found");
        return null;
    }
}
