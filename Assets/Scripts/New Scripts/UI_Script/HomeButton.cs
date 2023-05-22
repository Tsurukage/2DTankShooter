using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    [SerializeField]private Button btn_start;
    [SerializeField]private Button btn_setting;

    void Start()
    {
        if (btn_start != null)
            btn_start.onClick.AddListener(StartGame);
        if(btn_setting != null)
            btn_setting.onClick.AddListener(OpenSetting);
    }

    private void OpenSetting()
    {
        SoundEffectManager.Instance.OnClickSound();
    }

    private void StartGame()
    {
        SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.NextStage();
    }
}
