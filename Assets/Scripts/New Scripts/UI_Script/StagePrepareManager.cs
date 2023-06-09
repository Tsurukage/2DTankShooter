using UnityEngine;
using UnityEngine.UI;
using Models;
using System;

public class StagePrepareManager : MonoBehaviour
{
    private Player Player => Game.World.Player;
    [SerializeField] private Image img_rank;
    [SerializeField] private Text text_rank;
    [SerializeField] private Text _stageName;
    [SerializeField] private Text _timerText;
    [SerializeField] private Button _adsButton;
    [SerializeField] private Button _diamondButton;

    [SerializeField] private Sprite[] sprite_rank;
    [SerializeField] private string[] string_rank;
    private float _time = 3;        //Previously set to 9
    void Awake()
    {
        GameManager.OnStateChange += OnSetActive;
    }
    private void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
    }
    public void OnSetActive(GameState state, float delay = 0)
    {
        gameObject.SetActive(state == GameState.StagePrepareUI);
    }

    private void Start()
    {
        var game = FindObjectOfType<Game>();
        if(game != null)
        {
            var rank = (int)Player.Rank;
            img_rank.sprite = sprite_rank[rank];
            text_rank.text = string_rank[rank];
        }
        _timerText = GameObject.Find("text_cd").GetComponent<Text>();
        _stageName.text = SimpleGame.Instance.Stage_Name;
        _time = SimpleGame.Instance.CountDown;
        _timerText.text = _time.ToString();
        _adsButton.onClick.AddListener(OnAdsClickAction);
        //_diamondButton.onClick.AddListener(OnDiamondClickAction);
    }

    //private void OnDiamondClickAction()
    //{
        //var player = Game.World.Player;
        //var diamond = player.Diamond;
        //if (diamond > 0)
        //{
            //player.AddDiamond(-1);
            //var loot = GetComponent<LootBag>();
            //loot.InstantiateLoot();
            //SoundEffectManager.Instance.OnClickSound();
            //SetInteraction(false);
        //}
        //else
            //print("Player has no diamond");
    //}

    private void OnAdsClickAction()
    {
        SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.UpdateGameState(GameState.StageWatchAds);
        AdsSimulation.SimAds(isSuccess =>
        {
            if (isSuccess)
            {
                GameManager.Instance.UpdateGameState(GameState.StageInProgress);
                var loot = GetComponent<LootBag>();
                loot.InstantiateLoot();
                SetInteraction(false);
                return;
            }
            {
                Debug.Log("�v�����ŁI");
                GameManager.Instance.UpdateGameState(GameState.StageInProgress);
            }
        });
    }
    private void SetInteraction(bool interactable)
    {
        _adsButton.interactable = interactable;
        _diamondButton.interactable = interactable;
    }

    void Update()
    {
        if(_time > 0)
        {
            _time -= Time.deltaTime;
        }
        if(_time < 0)
        {
            GameManager.Instance.UpdateGameState(GameState.StageInProgress);
        }
        _timerText.text = _time.ToString("00");
    }
}
