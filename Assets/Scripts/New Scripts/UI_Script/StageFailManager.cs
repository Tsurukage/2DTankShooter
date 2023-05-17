using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFailManager : MonoBehaviour
{
    [SerializeField] private Button _nextStage;
    [SerializeField] private Button _mainMenu;
    void Awake()
    {
        GameManager.OnStateChange += OnSetActive;
    }
    void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
    }

    public void OnSetActive(GameState state, float delay = 0)
    {
        gameObject.SetActive(state == GameState.StageFailUI);
        transform.localScale = Vector3.zero;
        if (state == GameState.StageFailUI)
        {
            SimpleGame.Instance.CheckStreak(lose: 1);
            StartCoroutine(SetDelay(delay));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_nextStage != null)
            _nextStage.onClick.AddListener(GetNextLevel);
        if (_mainMenu != null)
            _mainMenu.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        if (SoundEffectManager.Instance != null)
            SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.HomeScene();
    }
    private void GetNextLevel()
    {
        if (SoundEffectManager.Instance != null)
            SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.NextStage();
    }
    IEnumerator SetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.localScale = Vector3.one;
        if (SoundEffectManager.Instance != null)
            SoundEffectManager.Instance.OnFailSound();
    }
}
