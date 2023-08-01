using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdAgent : MonoBehaviour
{
    public enum AdType
    {
        Unity,
        Pangle,
    }
#if UNITY_EDITOR
    [SerializeField]private Text _message;
    private static Text Message { get; set; }
    [SerializeField]private Button _requestButton;
#endif
    [SerializeField]private bool _unityAdTestMode;
    [SerializeField]private AdType _adType = AdType.Unity;
    //private PangleAdController _pangleAdController;
    //private PangleAdController PangleAdController => _pangleAdController ??= new PangleAdController();
    private UnityAdController _unityAdController;
    private UnityAdController UnityAdController => _unityAdController ??= new UnityAdController();

    private void Start()
    {
#if UNITY_EDITOR
        Message = _message;
#endif
        Print("初始化中...");
        switch (_adType)
        {
            case AdType.Unity:
                InitUnityAd();
                break;
            case AdType.Pangle:
                InitPangleAd();
                break;
        }
    }

    static void Print(string message)
    {
#if UNITY_EDITOR
        if (Message != null) Message.text = message;
#else
        print(message);
#endif
    }

    public void SwitchAdType(AdType adType)
    {
        _adType = adType;
        switch (_adType)
        {
            case AdType.Unity:
                InitUnityAd();
                break;
            case AdType.Pangle:
                InitPangleAd();
                break;
        }
    }

    public void RequestRewardedAd(Action<bool> successAction)
    {
        switch (_adType)
        {
            case AdType.Unity:
                StartCoroutine(ShowUnityAd(successAction));
                break;
            case AdType.Pangle:
#if UNITY_EDITOR
                successAction(true);
#else
                //PangleAdController.RequestDirectRewardedAd((success, message) => successAction(success));
#endif
                break;
        }
    }

    private bool isShowingUnityAd;
    IEnumerator ShowUnityAd(Action<bool> successAction)
    {
        if(isShowingUnityAd) yield break;
        isShowingUnityAd = true;
        var waiting = 2f;
        var loopSecs = 0f;
        if (!UnityAdController.IsRewardedAdLoaded)
        {
            yield return new WaitForSeconds(0.5f);
            loopSecs += 0.5f;
            if (loopSecs >= waiting)
            {
                successAction(false);
                isShowingUnityAd = false;
                yield break;
            }
        }
        UnityAdController.ShowRewardedAd(success =>
        {
            isShowingUnityAd = false;
            successAction(success);
            UnityAdController.LoadRewardedAd(null);
        });
    }

    private void InitUnityAd()
    {
        if(UnityAdController.IsInit) return;
        UnityAdController.Init(_unityAdTestMode);
        UnityAdController.LoadRewardedAd(null);
#if UNITY_EDITOR
        if (_requestButton != null)
        {
            _requestButton.onClick.AddListener(() =>
            {
                Print("加载中...");
                UnityAdController.ShowRewardedAd((success) => { Print(success ? "看完广告" : "未看完广告"); });
            });
        }
#endif
    }

    private void InitPangleAd()
    {
//        if(PangleAdController.IsInit) return;

//        PangleAdController.Init((success, message) =>
//        {
//            Print(message);
//        });
//#if UNITY_EDITOR
//        if(_requestButton != null)
//        {
//            _requestButton.onClick.AddListener(() =>
//            {
//                Print("加载中...");
//                PangleAdController.RequestDirectRewardedAd((success, message) =>
//                {
//                    Print(message);
//                    if (success) return;
//                    Print(message);
//                });
//            });
//        }
//#endif
    }
}