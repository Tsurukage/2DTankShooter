using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageChanceManager : MonoBehaviour
{
    private float value_time = 0;
    [SerializeField] private Text text_timer;
    [SerializeField] private Button btn_ads;
    bool clicked = false;
    bool starTimer = false;
    void Awake()
    {
        GameManager.OnStateChange += OnSetActive;
    }
    void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
    }
    public void OnSetActive(GameState state)
    {
        gameObject.SetActive(state == GameState.StageChancesUI);
        transform.localScale = Vector3.zero;
        if (state == GameState.StageChancesUI)
            StartCoroutine(SetDelay());
    }
    IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(2f);
        starTimer = true;
        transform.localScale = Vector3.one;
    }
    void Start()
    {
        value_time = SimpleGame.Instance.CountDown;
        text_timer.text = value_time.ToString("00");
        btn_ads.onClick.AddListener(OnClickAction);
        starTimer = false;
    }

    private void OnClickAction()
    {
        SimpleGame.Instance.SetBool();
        btn_ads.interactable = false;
        clicked = true;
        SimpleGame.Instance.IncreaseShootingCount(2);
    }

    void Update()
    {
        if (starTimer)
        {
            if (value_time > 0)
            {
                value_time -= Time.deltaTime;
            }
            if (value_time < 0)
            {
                if (clicked)
                    GameManager.Instance.UpdateGameState(GameState.StageInProgress);
                else
                    GameManager.Instance.UpdateGameState(GameState.StageFailUI);
            }
            text_timer.text = value_time.ToString("00");
        }
    }
}
