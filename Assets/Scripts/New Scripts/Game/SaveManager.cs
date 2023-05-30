using System.IO;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _savePath;

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "playerSave.bytes");
    }
    public void SaveToFile(PlayerSave p)
    {
        string json = JsonConvert.SerializeObject(p);
        File.WriteAllText(_savePath, json);
        print(json);
    }
    public PlayerSave LoadPlayer()
    {
        try
        {
            string json = File.ReadAllText(_savePath);
            return JsonConvert.DeserializeObject<PlayerSave>(json);
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    public void Clear()
    {
        string json = string.Empty;
        File.WriteAllText(_savePath, json);
        //File.Delete(_savePath);
        
    }
}
