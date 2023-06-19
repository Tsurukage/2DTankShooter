using System;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    public AudioSource _soundeffect;
    public AudioSource _soundeffect2;
    public AudioSource _soundeffect3;
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
    internal void SetThirdSFX(AudioClip clip)
    {
        _soundeffect3.clip = clip;
        _soundeffect3.Play();
    }

    internal void LoopCasting(AudioClip clip)
    {
        _soundeffect.clip = clip;
        _soundeffect.Play();
        _soundeffect.loop = true;
    }
    internal void LoopSecondSFX(AudioClip clip)
    {
        _soundeffect2.clip = clip;
        _soundeffect2.Play();
        _soundeffect2.loop = true;
    }
    internal void LoopThirdSFX(AudioClip clip)
    {
        _soundeffect3.clip = clip;
        _soundeffect3.Play();
        _soundeffect3.loop = true;
    }
    internal void StopLoop()
    {
        _soundeffect.loop = false;
    }
    internal void StopLoopSecondSFX()
    {
        _soundeffect2.loop = false;
    }
    internal void StopLoopThirdSFX()
    {
        _soundeffect3.loop = false;
    }
    internal void StopThirdSFX() => _soundeffect3.Stop(); 
}
