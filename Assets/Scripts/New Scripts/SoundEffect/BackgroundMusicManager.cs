using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;
    private AudioSource bgmAudioSource;

    void Awake()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        if(Instance ==  null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {

    }
    public void SetBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }
}
