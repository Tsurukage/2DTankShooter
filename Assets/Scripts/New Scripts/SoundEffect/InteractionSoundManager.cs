using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSoundManager : MonoBehaviour
{
    public static InteractionSoundManager Instance;
    public AudioSource _interaction;

    void Awake()
    {
        Instance = this;
        _interaction = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void OnClickSound()
    {
        _interaction.Play();
    }
}
