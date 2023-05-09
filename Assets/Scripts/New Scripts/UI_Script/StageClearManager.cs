using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearManager : MonoBehaviour
{
    public static StageClearManager instance;
    [SerializeField] private Button _nextStage;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private GameObject _filledStars_I;
    [SerializeField] private GameObject _filledStars_II;
    [SerializeField] private GameObject _filledStars_III;
    [SerializeField] private Text _badgeValue;
    void Awake()
    {
        instance = this;
        GameManager.OnStateChange += OnSetActive;
        SimpleGame.StarOne += ActiveStarOne;
        SimpleGame.StarTwo += ActiveStarTwo;
        SimpleGame.StarThree += ActiveStarThree;
        SimpleGame.Top_UI += SetBadgeValue;
    }

    private void SetBadgeValue(int tank, int shoot, int badge, int animal)
    {
        if (_badgeValue != null)
            _badgeValue.text = badge.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
        SimpleGame.StarOne -= ActiveStarOne;
        SimpleGame.StarTwo -= ActiveStarTwo;
        SimpleGame.StarThree -= ActiveStarThree;
    }
    public void OnSetActive(GameState state, float delay = 0)
    {
        gameObject.SetActive(state == GameState.StageClearUI);
        transform.localScale = Vector3.zero;
        if (state == GameState.StageClearUI)
            StartCoroutine(SetDealy(delay));
    }

    void Start()
    {
        if (_nextStage != null)
            _nextStage.onClick.AddListener(GetNextLevel);
        if(_mainMenu !=null)
            _mainMenu.onClick.AddListener(BackToMainMenu);
    }
    private void BackToMainMenu()
    {
        if(SoundEffectManager.Instance != null)
            SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.HomeScene();
    }
    private void GetNextLevel()
    {
        if (SoundEffectManager.Instance != null)
            SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.NextStage();
    }
    public void ActiveStarOne(bool fullied) => _filledStars_I.SetActive(fullied);
    public void ActiveStarTwo(bool fullied) => _filledStars_II.SetActive(fullied);
    public void ActiveStarThree(bool fullied) => _filledStars_III.SetActive(fullied);

    IEnumerator SetDealy(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.localScale = Vector3.one;
        if (SoundEffectManager.Instance != null)
            SoundEffectManager.Instance.OnVictorySound();
    }
}
