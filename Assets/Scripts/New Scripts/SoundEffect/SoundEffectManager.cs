using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    public AudioSource _soundeffect;
    public AudioSource _soundeffect2;
    public AudioClip _click_se;
    public AudioClip _victory_se;
    public AudioClip _fail_se;
    void Awake()
    {
        _soundeffect = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnClickSound()
    {
        _soundeffect.PlayOneShot(_click_se, 1f);
    }
    public void OnVictorySound()
    {
        _soundeffect.PlayOneShot(_victory_se, 0.5f);
    }
    public void OnFailSound()
    {
        _soundeffect.PlayOneShot(_fail_se, 0.5f);
    }

    internal void SetSFX(AudioClip clip)
    {
        _soundeffect.clip = clip;
        _soundeffect.Play();
    }

    internal void StopSFX()
    {
        _soundeffect.volume = 0f;
    }

    internal void SetSecondSFX(AudioClip clip)
    {
        _soundeffect2.clip = clip;
        _soundeffect2.Play();
    }
}
