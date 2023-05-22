using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowUISetting : MonoBehaviour
{
    [SerializeField] private Toggle toggle_bgm;
    [SerializeField] private Toggle toggle_sfx;
    [SerializeField] private Button btn_exitApp;
    [SerializeField] private Button btn_exitLevel;
    [SerializeField] private Button btn_closeWin;

    void Awake()
    {
        Top_UI_Manager.OnClick += SetObjectActive;
    }
    void OnDestroy()
    {
        Top_UI_Manager.OnClick -= SetObjectActive;
    }
    public void SetObjectActive()
    {
        gameObject.SetActive(true);
        SetActive(GameManager.Instance.State);
    }

    void Start()
    {
        if (toggle_bgm != null)
        {
            toggle_bgm.isOn = PlayerPrefs.GetInt("BgmEnabled", 1) == 1;
            toggle_bgm.onValueChanged.AddListener(ToggleBgm);
        }
        if (toggle_sfx != null)
        {
            toggle_sfx.isOn = PlayerPrefs.GetInt("SfxEnabled", 1) == 1;
            toggle_sfx.onValueChanged.AddListener(ToggleSfx);
        }
        if (btn_closeWin != null)
            btn_closeWin.onClick.AddListener(CloseWind);
        if (btn_exitApp != null)
            btn_exitApp.onClick.AddListener(ExitApp);
        if (btn_exitLevel != null)
            btn_exitLevel.onClick.AddListener(ExitLevel);

    }

    private void ExitLevel()
    {
        SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.HomeScene();
    }

    private void ExitApp()
    {
        SoundEffectManager.Instance.OnClickSound();
        GameManager.Instance.ExitApplication();
    }

    private void CloseWind()
    {
        gameObject.SetActive(false);
        SoundEffectManager.Instance.OnClickSound();
    }

    private void ToggleSfx(bool arg0)
    {
        PlayerPrefs.SetInt("SfxEnabled", arg0 ? 1 : 0);
        SoundEffectManager.Instance.OnClickSound();
        AudioManager.Instance.ToggleSFX();
    }

    private void ToggleBgm(bool arg0)
    {
        PlayerPrefs.SetInt("BgmEnabled", arg0 ? 1 : 0);
        SoundEffectManager.Instance.OnClickSound();
        AudioManager.Instance.ToggleBGM();
    }
    public void SetActive(GameState state, float delay = 0)
    {
        btn_exitApp.gameObject.SetActive(state == GameState.InMainMenu);
        btn_exitLevel.gameObject.SetActive(state != GameState.InMainMenu);
    }
}
