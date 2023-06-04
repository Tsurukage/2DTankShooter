using Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionLeaderboard : MonoBehaviour
{
    [SerializeField] private NPCDataReader npcsData;
    [SerializeField] private GameObject npcLeaderboard;
    [SerializeField] private Transform leaderboard;

    private float interval = 5f;
    private List<Player> npcPlayerList;
    void Start()
    {
        npcPlayerList = npcsData.npcPlayerDictionary.Values.ToList();

        GenerateLeaderBoard();
        UpdateLeaderboard();
    }

    public void GenerateLeaderBoard()
    {
        foreach(Transform child in leaderboard)
        {
            Destroy(child.gameObject);
        }
        List<Player> allList = new List<Player>(npcPlayerList);
        var player = Game.World.Player;
        allList.Add(player);

        allList = allList.OrderByDescending(npc => npc.Rank).ThenByDescending(npc => npc.Badge).ToList();
        for(int i = 0; i < allList.Count; i++)
        {
            if(i < allList.Count)
            {
                Player npc = allList[i];
                var npcObj = Instantiate(npcLeaderboard, leaderboard);
                var objPrefab = npcObj.GetComponent<Prefab_NPC>();
                objPrefab.SetName(npc.Name);
                objPrefab.SetNation(npc.Nationality);
                objPrefab.SetRank((int)npc.Rank);
                objPrefab.SetIcon(npc.Avatar);
                objPrefab.SetBadge(npc.Badge);
                objPrefab.SetGender((int)npc.Gender);
                objPrefab.SetRankPos(i+1);
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
        GenerateLeaderBoard();
    }

    private int UpdateBadge(int badge)
    {
        bool isBadgeIncrease = Random.Range(0f, 1f) < 0.5f;
        if(isBadgeIncrease)
        {
            int incValue = Random.Range(1, 4);
            badge += incValue;
        }
        else
        {
            int decValue = Random.Range(1, 4);
            badge -= decValue;
        }
        return badge;
    }

    private Rank UpdateRank(Rank rank)
    {
        bool isRankUp = Random.Range(0f, 1f) < 0.5f;
        if(isRankUp)
        {
            int incRank = Random.Range(0, 2);
            rank += incRank;
        }
        else
        {
            int decRank = Random.Range(0, 2);
            rank -= decRank;
        }
        rank = (rank < 0) ? 0 : rank;
        rank = ((int)rank > 8) ? (Rank)8 : rank;
        return rank;
    }
}
