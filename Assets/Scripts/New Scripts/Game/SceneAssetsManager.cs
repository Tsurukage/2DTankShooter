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
    [SerializeField] private List<string> tier_platinumLow = new List<string>();
    [SerializeField] private List<string> tier_platinumMid = new List<string>();
    [SerializeField] private List<string> tier_platinumHigh = new List<string>();
    [SerializeField] private List<string> tier_diamondLow = new List<string>();
    [SerializeField] private List<string> tier_diamondMid = new List<string>();
    [SerializeField] private List<string> tier_diamondHigh = new List<string>();
    [SerializeField] private List<string> tier_masterLow = new List<string>();
    [SerializeField] private List<string> tier_masterMid = new List<string>();
    [SerializeField] private List<string> tier_masterHigh = new List<string>();
    [SerializeField] private List<string> tier_challenger = new List<string>();
    [SerializeField] private List<string> tier_grandmaster = new List<string>();
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
            case Rank.PlatinumLow:
                foreach (string scene in tier_platinumLow)
                    possibleScene.Add(scene);
                break;
            case Rank.PlatinumMid:
                foreach (string scene in tier_platinumMid)
                    possibleScene.Add(scene);
                break;
            case Rank.PlatinumHigh:
                foreach (string scene in tier_platinumHigh)
                    possibleScene.Add(scene);
                break;
            case Rank.DiamondLow:
                foreach (string scene in tier_diamondLow)
                    possibleScene.Add(scene);
                break;
            case Rank.DiamondMid:
                foreach (string scene in tier_diamondMid)
                    possibleScene.Add(scene);
                break;
            case Rank.DiamondHigh:
                foreach (string scene in tier_diamondHigh)
                    possibleScene.Add(scene);
                break;
            case Rank.MasterLow:
                foreach (string scene in tier_masterLow)
                    possibleScene.Add(scene);
                break;
            case Rank.MasterMid:
                foreach (string scene in tier_masterMid)
                    possibleScene.Add(scene);
                break;
            case Rank.MasterHigh:
                foreach (string scene in tier_masterHigh)
                    possibleScene.Add(scene);
                break;
            case Rank.Challenger:
                foreach (string scene in tier_challenger)
                    possibleScene.Add(scene);
                break;
            case Rank.GrandMaster:
                foreach (string scene in tier_grandmaster)
                    possibleScene.Add(scene);
                break;
        }
        string selectecScene = possibleScene[Random.Range(0, possibleScene.Count)];
        SceneManager.LoadScene(selectecScene);
    }
}
