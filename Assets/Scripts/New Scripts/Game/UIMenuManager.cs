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
    //[SerializeField] private GameObject _windowSetting;
    [SerializeField] private Transform _uiCanvas;

    void Start()
    {
        var startPanel = Instantiate(_stagePreparePanel, _uiCanvas);
        var panelStart = startPanel.GetComponent<StagePrepareManager>();
        panelStart.OnSetActive(GameManager.Instance.State);

        var chancePanel = Instantiate(_stageChancePanel, _uiCanvas);
        var panelChance = chancePanel.GetComponent<StageChanceManager>();
        panelChance.OnSetActive(GameManager.Instance.State);

        var clearPanel = Instantiate(_stageClearPanel, _uiCanvas);
        var panelClear = clearPanel.GetComponent<StageClearManager>();
        panelClear.OnSetActive(GameManager.Instance.State);

        var failPanel = Instantiate(_stageFailPanel, _uiCanvas);
        var panelFail = failPanel.GetComponent<StageFailManager>();
        panelFail.OnSetActive(GameManager.Instance.State);

        //var windowSetting = Instantiate(_windowSetting, _uiCanvas);
        //windowSetting.SetActive(false);
    }
}
