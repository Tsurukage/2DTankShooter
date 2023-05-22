using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBGM : MonoBehaviour
{
    [SerializeField] private AudioClip homeClip;

    private void Start()
    {
        BackgroundMusicManager.Instance.SetBgm(homeClip);
    }
}
