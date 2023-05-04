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
    [SerializeField] private Transform _uiCanvas;

    void Start()
    {
        var startPanel = Instantiate(_stagePreparePanel, _uiCanvas);
        startPanel.SetActive(true);
        var chancePanel = Instantiate(_stageChancePanel, _uiCanvas);
        chancePanel.SetActive(false);
        var clearPanel = Instantiate(_stageClearPanel, _uiCanvas);
        clearPanel.SetActive(false);
        var failPanel = Instantiate(_stageFailPanel, _uiCanvas);
        failPanel.SetActive(false);
    }
}
