using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmAudioSource;
    private AudioSource[] sfxAudioSource;

    private float bgmVolume = 0.25f;
    private float sfxVolume = 0.5f;

    public bool isBgmEnabled = true;
    public bool isSfxEnabled = true;

    private const string BGMPrefsKey = "BgmEnabled";
    private const string SFXPrefsKey = "SfxEnabled";
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        GameObject[] bgmObjs = GameObject.FindGameObjectsWithTag("BGM");
        foreach(GameObject bgmobj in bgmObjs)
        {
            bgmAudioSource = bgmobj.GetComponent<AudioSource>();
        }
        GameObject[] sfxObjs = GameObject.FindGameObjectsWithTag("SFX");
        foreach(GameObject sfxmobj in sfxObjs)
        {
            sfxAudioSource = sfxmobj.GetComponents<AudioSource>();
        }
        isBgmEnabled = PlayerPrefs.GetInt(BGMPrefsKey, 1) == 1;
        isSfxEnabled = PlayerPrefs.GetInt(SFXPrefsKey, 1) == 1;
        ApplyAudioSettings();
    }

    private void ApplyAudioSettings()
    {
        bgmAudioSource.volume = bgmVolume * (isBgmEnabled ? 1 : 0);
        for(int i =0; i < sfxAudioSource.Length; i++)
        {
            sfxAudioSource[i].volume = sfxVolume * (isSfxEnabled ? 1 : 0);
        }
    }

    public void ToggleBGM()
    {
        isBgmEnabled = !isBgmEnabled;
        ApplyAudioSettings();
    }
    public void ToggleSFX()
    {
        isSfxEnabled = !isSfxEnabled;
        ApplyAudioSettings();
    }
}
