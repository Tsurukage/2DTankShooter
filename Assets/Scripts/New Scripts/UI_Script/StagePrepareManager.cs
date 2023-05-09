using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePrepareManager : MonoBehaviour
{
    [SerializeField] private Text _stageName;
    [SerializeField] private Text _timerText;
    [SerializeField] private Button _adsButton;
    private float _time = 9;
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
        _timerText = GameObject.Find("text_cd").GetComponent<Text>();
        _stageName.text = SimpleGame.Instance.Stage_Name;
        _time = SimpleGame.Instance.CountDown;
        _timerText.text = _time.ToString();
        _adsButton.onClick.AddListener(OnClickAction);
    }

    private void OnClickAction()
    {
        var loot = GetComponent<LootBag>();
        loot.InstantiateLoot();
        SoundEffectManager.Instance.OnClickSound();
        _adsButton.interactable = false;
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
