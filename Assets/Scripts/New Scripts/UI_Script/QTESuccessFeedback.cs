using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESuccessFeedback : MonoBehaviour
{
    public static QTESuccessFeedback instance;
    public GameObject[] feedbacks;
    public AudioClip[] clips;

    private void Start()
    {
        instance = this;
    }
    public void InstantiateQTEFeedback(int index)
    {
        if (feedbacks[index] != null)
            Instantiate(feedbacks[index], transform);
    }
    public void SendSecondSFX(int index)
    {
        if (clips[index] != null)
            SoundEffectManager.Instance.SetForthSFX(clips[index]);
    }
    public void StopSecondSFX()
    {
        SoundEffectManager.Instance.StopLoopForthSFX();
    }
}
