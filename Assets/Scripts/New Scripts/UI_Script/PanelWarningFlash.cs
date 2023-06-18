using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelWarningFlash : MonoBehaviour
{
    public static PanelWarningFlash Instance;

    public UnityEvent OnStartFlash;
    public UnityEvent OnEndFlash;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OnEndFlash?.Invoke();
    }
    public void StartFlash()
    {
        OnStartFlash?.Invoke();
    }
    public void EndFlash()
    {
        OnEndFlash?.Invoke();
    }
}
