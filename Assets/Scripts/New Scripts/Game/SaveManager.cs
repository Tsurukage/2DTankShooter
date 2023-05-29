using System.IO;
using Models;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private TextAsset saveFile;

    public void SaveToFile(PlayerSave p)
    {
        string json = JsonConvert.SerializeObject(p);
        File.WriteAllText(AssetDatabase.GetAssetPath(saveFile), json);
        print(json);
        AssetDatabase.Refresh();
    }
    public PlayerSave LoadPlayer()
    {
        try
        {
            return JsonConvert.DeserializeObject<PlayerSave>(saveFile.text);
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    public void Clear()
    {
        string json = string.Empty;
        File.WriteAllText(AssetDatabase.GetAssetPath(saveFile), json);
        AssetDatabase.Refresh();
    }
}
