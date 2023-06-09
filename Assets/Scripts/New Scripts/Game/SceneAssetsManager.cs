using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAssetsManager : MonoBehaviour
{
    public static SceneAssetsManager Instance;
    [SerializeField] private List<string> tier_bronzeLow = new List<string>();
    [SerializeField] private List<string> tier_bronzeMid = new List<string>();
    [SerializeField] private List<string> tier_bronzeHigh = new List<string>();
    [SerializeField] private List<string> tier_silverLow = new List<string>();
    [SerializeField] private List<string> tier_silverMid = new List<string>();
    [SerializeField] private List<string> tier_silverHigh = new List<string>();
    [SerializeField] private List<string> tier_goldLow = new List<string>();
    [SerializeField] private List<string> tier_goldMid = new List<string>();
    [SerializeField] private List<string> tier_goldHigh = new List<string>();
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void LoadScene(Rank rank)
    {
        List<string> possibleScene = new List<string>();
        switch (rank)
        {
            case Rank.BronzeLow:
                foreach (string scene in tier_bronzeLow)
                    possibleScene.Add(scene);
                break;
            case Rank.BronzeMid:
                foreach (string scene in tier_bronzeMid)
                    possibleScene.Add(scene);
                break;
            case Rank.BronzeHigh:
                foreach (string scene in tier_bronzeHigh)
                    possibleScene.Add(scene);
                break;
            case Rank.SilverLow:
                foreach (string scene in tier_silverLow)
                    possibleScene.Add(scene);
                break;
            case Rank.SilverMid:
                foreach (string scene in tier_silverMid)
                    possibleScene.Add(scene);
                break;
            case Rank.SilverHigh:
                foreach (string scene in tier_silverHigh)
                    possibleScene.Add(scene);
                break;
            case Rank.GoldLow:
                foreach (string scene in tier_goldLow)
                    possibleScene.Add(scene);
                break;
            case Rank.GoldMid:
                foreach (string scene in tier_goldMid)
                    possibleScene.Add(scene);
                break;
            case Rank.GoldHigh:
                foreach (string scene in tier_goldHigh)
                    possibleScene.Add(scene);
                break;
        }
        string selectecScene = possibleScene[Random.Range(0, possibleScene.Count)];
        SceneManager.LoadScene(selectecScene);
    }
}
