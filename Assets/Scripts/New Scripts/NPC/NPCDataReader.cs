using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NPCDataReader : MonoBehaviour
{
    public TextAsset npcDataFile;
    public Dictionary<string, Player> npcPlayerDictionary = new Dictionary<string, Player>();
    [SerializeField] private int MaxNpc = 10;
    string jsonFilePath;
    private void Awake()
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "npcDave.bytes");
    }
    void Start()
    {
        Clear();
        ReadNPCDataFromCSV(MaxNpc);
        SaveNPCJson(npcPlayerDictionary);
        LoadNPCData();

    }
    private void ReadNPCDataFromCSV(int count)
    {
        Dictionary<string, Player> dataDict = new Dictionary<string, Player>();
        string[] lines = npcDataFile.text.Split("\n");
        List<string> npcLines = new List<string>(lines);
        npcLines.RemoveAt(0);
        System.Random random = new System.Random();

        for(int i = 0; i < count; i++)
        {
            int randomIndex = random.Next(npcLines.Count);
            string line = npcLines[randomIndex].Trim();
            npcLines.RemoveAt(randomIndex);

            if(string.IsNullOrEmpty(line)) continue;

            string[] data = line.Split(",");

            if(data.Length >= 6)
            {
                string npc_id = data[0];
                string npc_name = data[1];
                int npc_gender;
                if (int.TryParse(data[2], out npc_gender))
                {
                    string npc_nationality = data[3];
                    int npc_avatar;
                    if (int.TryParse(data[4], out npc_avatar))
                    {
                        int npc_rank;
                        if (int.TryParse(data[5], out npc_rank))
                        {
                            int npc_badge;
                            if (int.TryParse(data[6], out npc_badge))
                            {
                                Player npcData = new Player(npc_id, npc_name, npc_nationality, npc_badge, (Rank)npc_rank, (Gender)npc_gender, npc_avatar);
                                dataDict.Add(npc_id, npcData);
                            }
                        }
                    }
                }
            }
        }
        npcPlayerDictionary = dataDict;
    }
    private void SaveNPCJson(Dictionary<string, Player> npcDataDict)
    {
        List<Player> npcDataList = new List<Player>(npcDataDict.Values);
        string jsonData = JsonConvert.SerializeObject(new NPCDataList(npcDataList), Formatting.Indented);

        File.WriteAllText(jsonFilePath, jsonData);
    }

    private Dictionary<string, Player> LoadNPCData()
    {
        Dictionary<string, Player> dataDict = new Dictionary<string, Player>();
        try
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                NPCDataList dataList = JsonConvert.DeserializeObject<NPCDataList>(json);
                foreach (Player npcData in dataList.npcDataList)
                {
                    dataDict.Add(npcData.Uid, npcData);
                }
            }
            return dataDict;
        }
        catch (Exception)
        {
            return null;
        }
    }
    private void Clear()
    {
        File.Delete(jsonFilePath);
    }
}

