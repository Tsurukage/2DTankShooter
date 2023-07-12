using System;
using UnityEngine;
using UnityEngine.UI;
using ToastNotify.Model;

public class AdsSimulation : MonoBehaviour
{
    public Button btn_success;
    public Button btn_failure;

    private static Button Btn_success { get; set; }
    private static Button Btn_failure { get; set; }
    private static AdsSimulation instance;

    private PangleAdController _pangleAdController;
    private PangleAdController PangleAdController => _pangleAdController ??= new PangleAdController();
    void Display(bool display)
    {
        gameObject.SetActive(display);
    }
    private void Start()
    {
        instance = this;
        Btn_success = btn_success;
        Btn_failure = btn_failure;
        Display(false);
        PangleAdController.Init((success, massage) =>
        {
        });
    }
    public static void SimAds(Action<bool> callbackAction)
    {
        instance.Display(true);
        Btn_success.onClick.RemoveAllListeners();
        Btn_failure.onClick.RemoveAllListeners();
        Btn_success.onClick.AddListener(() =>
        {
            SoundEffectManager.Instance.OnClickSound();
            callbackAction(true);
            instance.Display(false);
        });
        Btn_failure.onClick.AddListener(() =>
        {
            SoundEffectManager.Instance.OnClickSound();
            callbackAction(false);
            instance.Display(false);
        });
    }
    public static void InitAds(Action<bool> callbackAction)
    {
        instance.Display(true);
        instance.PangleAdController.RequestDirectRewardedAd((success, message) =>
        {
            if (success)
            {
                callbackAction(true);
                instance.Display(false);
            }
            else
            {
                callbackAction(false);
                instance.Display(false);
                Toast.Show("奖励失败！");
            }
        });
    }
}
