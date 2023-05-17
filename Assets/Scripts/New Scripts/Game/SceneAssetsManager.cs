using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAssetsManager : MonoBehaviour
{
    public static SceneAssetsManager Instance;
    [SerializeField] private List<SceneAsset> tier_bronze = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_silver = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_gold = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_platinum = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_diamond = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_master = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_grandmaster = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_legend = new List<SceneAsset>();
    [SerializeField] private List<SceneAsset> tier_mythic = new List<SceneAsset>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        var list = tier_bronze.Count;
        print(list);
    }
    public void LoadScene(Rank rank)
    {
        List<SceneAsset> possibleScene = new List<SceneAsset>();
        switch (rank)
        {
            case Rank.Bronze:
                foreach (SceneAsset scene in tier_bronze)
                    possibleScene.Add(scene);
                break;
            case Rank.Silver:
                foreach (SceneAsset scene in tier_silver)
                    possibleScene.Add(scene);
                break;
            case Rank.Gold:
                foreach (SceneAsset scene in tier_gold)
                    possibleScene.Add(scene);
                break;
            case Rank.Platinum:
                foreach (SceneAsset scene in tier_platinum)
                    possibleScene.Add(scene);
                break;
            case Rank.Diamond:
                foreach (SceneAsset scene in tier_diamond)
                    possibleScene.Add(scene);
                break;
            case Rank.Master:
                foreach (SceneAsset scene in tier_master)
                    possibleScene.Add(scene);
                break;
            case Rank.Grandmaster:
                foreach (SceneAsset scene in tier_grandmaster)
                    possibleScene.Add(scene);
                break;
            case Rank.Legend:
                foreach (SceneAsset scene in tier_legend)
                    possibleScene.Add(scene);
                break;
            case Rank.Mythic:
                foreach (SceneAsset scene in tier_mythic)
                    possibleScene.Add(scene);
                break;
        }
        SceneAsset selectecScene = possibleScene[Random.Range(0, possibleScene.Count)];
        SceneManager.LoadScene(selectecScene.name);
    }
}
