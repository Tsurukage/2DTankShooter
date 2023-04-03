using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public AudioClip clipLaunch, clipHit;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void LaunchClip()
    {
        source.PlayOneShot(clipLaunch);
    }
    public void ImpactClip()
    {
        source.PlayOneShot(clipHit);
    }
}
