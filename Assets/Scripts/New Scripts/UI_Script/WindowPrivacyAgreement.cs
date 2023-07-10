using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowPrivacyAgreement : MonoBehaviour
{
    private static WindowPrivacyAgreement instance;
    [SerializeField] private Button btn_close;

    private void Start()
    {
        instance = this;
        Display(false);
        btn_close.onClick.AddListener(CloseDisplay);
    }
    private void Display(bool display)
    {
        gameObject.SetActive(display);
    }
    public static void OpenDisplay()
    {
        instance.Display(true);
    }
    private void CloseDisplay()
    {
        SoundEffectManager.Instance.OnClickSound();
        Display(false);
    }
}
