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
    public void InstantiateObjectRT()
    {
        if (objectToinstantiate != null)
            Instantiate(objectToinstantiate, transform);
    }
    public void SendSFX()
    {
        SoundEffectManager.Instance.SetSFX(clip);
    }
    public void StopSFX()
    {
        SoundEffectManager.Instance.StopSFX();
    }
    public void SendSecondSFX()
    {
        SoundEffectManager.Instance.SetSecondSFX(clip);
    }
    public void LoopCasting()
    {
        SoundEffectManager.Instance.LoopCasting(clip);
    }
    public void StopLoop()
    {
        SoundEffectManager.Instance.StopLoop();
    }
}
