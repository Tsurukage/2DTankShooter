using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAdController : IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public const string GAME_ID = "5347991"; //replace with your gameID from dashboard. note: will be different for each platform.

    private const string BANNER_PLACEMENT = "Banner_Android";
    private const string VIDEO_PLACEMENT = "Interstitial_Android";
    private const string REWARDED_VIDEO_PLACEMENT = "Rewarded_Android";

    [SerializeField] private BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    private Action<bool> LoadCallback { get; set; }
    private Action<bool> ShowCallback { get; set; }
    public bool IsInit { get; private set; }
    public bool IsRewardedAdLoaded { get; private set; }
    public void Init(bool testMode = true, BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER)
    {
        _bannerPosition = bannerPosition;
        if (Advertisement.isSupported) Debug.Log(Application.platform + " supported by Advertisement");
        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    public void ToggleBanner(bool show)
    {
        if (!show)
        {
            Advertisement.Banner.Hide();
            return;
        }

        Advertisement.Banner.SetPosition(_bannerPosition);
        Advertisement.Banner.Show(BANNER_PLACEMENT);
    }

    public void LoadRewardedAd(Action<bool> callbackAction)
    {
        LoadCallback = callbackAction;
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void ShowRewardedAd(Action<bool> callbackAction)
    {
        ShowCallback = callbackAction;
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void LoadNonRewardedAd(Action<bool> callbackAction)
    {
        LoadCallback = callbackAction;
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }

    public void ShowNonRewardedAd(Action<bool> callbackAction)
    {
        ShowCallback = callbackAction;
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        IsInit = true;
        Debug.Log("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        IsInit = false;
        Debug.LogWarning($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        IsRewardedAdLoaded = true;
        LoadCallback?.Invoke(true);
        Debug.Log($"Load Success: {placementId}");
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        IsRewardedAdLoaded = false;
        LoadCallback?.Invoke(false);
        Debug.LogWarning($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        ShowCallback?.Invoke(false);
        Debug.Log($"OnUnityAdsShowFailure: [{error}]: {message}");
    }
    public void OnUnityAdsShowStart(string placementId)
    {
        IsRewardedAdLoaded = false;
        Debug.Log($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId) => Debug.Log($"OnUnityAdsShowClick: {placementId}");
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        ShowCallback?.Invoke(true);
        Debug.Log($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    }
    #endregion
}
