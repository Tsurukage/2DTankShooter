using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SectionLeaderboard : MonoBehaviour
{
    [SerializeField] private NPCDataReader npcsData;
    [SerializeField] private GameObject npcLeaderboard;
    [SerializeField] private Transform leaderboard;
    [SerializeField] private int maxNPCs;
    [SerializeField ]private float interval = 1800;
    private List<Player> npcPlayerList;

    void Start()
    {
        Load();
        GenerateLeaderBoard();
        int timelapse = PlayerPrefs.GetInt("Count");
        for(int i = 0; i < timelapse; i++)
        {
            UpdateNpcPlayerData();
        }
        UpdateLeaderboard();
    }

    private void Load()
    {
        var npc = npcsData.LoadNPCData();
        npcPlayerList = npc;
        if(npcPlayerList == null)
        {
            npcsData.ReadNPCDataFromCSV(maxNPCs);
            npcPlayerList = npcsData.npcPlayerDictionary.Values.ToList();
            UpdateData();
        }
    }
    
    public void GenerateLeaderBoard()
    {
        foreach(Transform child in leaderboard)
        {
            Destroy(child.gameObject);
        }
        List<Player> allList = new List<Player>();
        var player = Game.World.Player;
        allList.Add(player);
        allList.AddRange(npcPlayerList);

        allList = allList.OrderByDescending(npc => npc.Rank).ThenByDescending(npc => npc.Badge).ToList();
        for (int i = 0; i < allList.Count; i++)
        {
            Player npc = allList[i];
            var npcObj = Instantiate(npcLeaderboard, leaderboard);
            npcObj.name = $"{npc.Name}, {npc.Badge}";
            var objPrefab = npcObj.GetComponent<Prefab_NPC>();
            objPrefab.SetName(npc.Name);
            objPrefab.SetNation(npc.Nationality);
            objPrefab.SetRank((int)npc.Rank);
            objPrefab.SetIcon(npc.Avatar);
            objPrefab.SetBadge(npc.Badge);
            objPrefab.SetGender((int)npc.Gender);
            objPrefab.SetRankPos(i + 1);
            if(npc.Name == player.Name)
            {
                var bg = npcObj.GetComponent<Image>();
                bg.color = Color.cyan;
            }
        }
    }
    private void UpdateLeaderboard()
    {
        InvokeRepeating("UpdateNpcPlayerData", interval, interval);
    }

    private void UpdateNpcPlayerData()
    {
        foreach(var npcPlayer in npcPlayerList)
        {
            npcPlayer.Rank = UpdateRank(npcPlayer.Rank);
            npcPlayer.Badge = UpdateBadge(npcPlayer.Badge);
        }
        UpdateData();
        GenerateLeaderBoard();
    }

    private int UpdateBadge(int badge)
    {
        bool isBadgeIncrease = UnityEngine.Random.Range(0f, 1f) < 0.5f;
        if(isBadgeIncrease)
        {
            int incValue = UnityEngine.Random.Range(1, 4);
            badge += incValue;
        }
        else
        {
            int decValue = UnityEngine.Random.Range(1, 4);
            badge -= decValue;
        }
        return badge;
    }

    private Rank UpdateRank(Rank rank)
    {
        bool isRankUp = UnityEngine.Random.Range(0f, 1f) < 0.5f;
        if(isRankUp)
        {
            int incRank = UnityEngine.Random.Range(0, 2);
            rank += incRank;
        }
        else
        {
            int decRank = UnityEngine.Random.Range(0, 2);
            rank -= decRank;
        }
        rank = (rank < 0) ? 0 : rank;
        rank = ((int)rank > 8) ? (Rank)8 : rank;
        return rank;
    }
    void UpdateData()
    {
        npcsData.SaveNPCJson(npcPlayerList);
    }
   
}
