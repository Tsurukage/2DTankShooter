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
    [SerializeField] private GameObject _filledStars_I;
    [SerializeField] private GameObject _filledStars_II;
    [SerializeField] private GameObject _filledStars_III;

    void Awake()
    {
        instance = this;
        GameManager.OnStateChange += OnSetActive;
    }
    void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
    }
    private void OnSetActive(GameState state)
    {
        gameObject.SetActive(state == GameState.StageClearUI);
    }

    void Start()
    {
        if (_nextStage != null)
            _nextStage.onClick.AddListener(GetNextLevel);
    }

    private void GetNextLevel() => GameManager.Instance.NextStage();
    public void ActiveStarOne(bool fullied) => _filledStars_I.SetActive(fullied);
    public void ActiveStarTwo(bool fullied) => _filledStars_II.SetActive(fullied);
    public void ActiveStarThree(bool fullied) => _filledStars_III.SetActive(fullied);
}
