using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _stagePreparePanel;
    [SerializeField] private GameObject _stageChancePanel;
    [SerializeField] private GameObject _stageClearPanel;
    [SerializeField] private GameObject _stageFailPanel;
    private void Awake()
    {
        GameManager.OnStateChange += GMOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnStateChange -= GMOnGameStateChanged;
    }
    private void GMOnGameStateChanged(GameState state)
    {
        _stagePreparePanel.SetActive(state == GameState.StagePrepareUI);
        _stageChancePanel.SetActive(state == GameState.StageChancesUI);
        _stageClearPanel.SetActive(state == GameState.StageClearUI);
        _stageFailPanel.SetActive(state == GameState.StageFailUI);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
