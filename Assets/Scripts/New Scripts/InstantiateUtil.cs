using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUtil : MonoBehaviour
{
    public GameObject objectToinstantiate;
    public AudioClip clip;

    public void InstantiateObject()
    {
        if (objectToinstantiate != null)
            Instantiate(objectToinstantiate);
    }
    public void SendSFX()
    {
        SoundEffectManager.Instance.SetSFX(clip);
    }
    public void StopSFX()
    {
        SoundEffectManager.Instance.StopSFX();
    }
}
