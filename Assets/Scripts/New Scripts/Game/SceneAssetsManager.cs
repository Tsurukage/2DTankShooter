using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAssetsManager : MonoBehaviour
{
    public static SceneAssetsManager Instance;
    [SerializeField] private List<string> tier_bronze = new List<string>();
    [SerializeField] private List<string> tier_silver = new List<string>();
    [SerializeField] private List<string> tier_gold = new List<string>();
    [SerializeField] private List<string> tier_platinum = new List<string>();
    [SerializeField] private List<string> tier_diamond = new List<string>();
    [SerializeField] private List<string> tier_master = new List<string>();
    [SerializeField] private List<string> tier_grandmaster = new List<string>();
    [SerializeField] private List<string> tier_legend = new List<string>();
    [SerializeField] private List<string> tier_mythic = new List<string>();
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
        List<string> possibleScene = new List<string>();
        switch (rank)
        {
            case Rank.Bronze:
                foreach (string scene in tier_bronze)
                    possibleScene.Add(scene);
                break;
            case Rank.Silver:
                foreach (string scene in tier_silver)
                    possibleScene.Add(scene);
                break;
            case Rank.Gold:
                foreach (string scene in tier_gold)
                    possibleScene.Add(scene);
                break;
            case Rank.Platinum:
                foreach (string scene in tier_platinum)
                    possibleScene.Add(scene);
                break;
            case Rank.Diamond:
                foreach (string scene in tier_diamond)
                    possibleScene.Add(scene);
                break;
            case Rank.Master:
                foreach (string scene in tier_master)
                    possibleScene.Add(scene);
                break;
            case Rank.Grandmaster:
                foreach (string scene in tier_grandmaster)
                    possibleScene.Add(scene);
                break;
            case Rank.Legend:
                foreach (string scene in tier_legend)
                    possibleScene.Add(scene);
                break;
            case Rank.Mythic:
                foreach (string scene in tier_mythic)
                    possibleScene.Add(scene);
                break;
        }
        string selectecScene = possibleScene[Random.Range(0, possibleScene.Count)];
        SceneManager.LoadScene(selectecScene);
    }
}
