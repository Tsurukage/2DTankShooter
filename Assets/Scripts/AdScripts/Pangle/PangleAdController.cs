//using System;
//using ByteDance.Union;
//using UnityEngine;
//using UnityEngine.Events;

//public class PangleAdController
//{
//    private AdNative _adNative;
//    private RewardVideoAd rewardAd;
//    public bool IsInit { get; private set; }

//    public string AndroidSlotID = "952667286";
//    private AdNative AdNative
//    {
//        get
//        {
//            if (_adNative == null) _adNative = SDK.CreateAdNative();
//#if UNITY_ANDROID
//            SDK.RequestPermissionIfNecessary();
//#endif
//            return _adNative;
//        }
//    }

//    public void Init(Action<bool, string> callbackAction) => Pangle.InitializeSDK((success, message) =>
//    {
//        callbackAction?.Invoke(success, message);
//        InitCallback(success, message);
//    });
//    private void InitCallback(bool success, string message)
//    {
//        IsInit = success;
//        Debug.Log("`````````````````初始化``````" + success + "-----" + message);
//    }

//    public void RequestDirectRewardedAd(UnityAction<bool, string> requestAction)
//    {
//        if (rewardAd != null)
//        {
//            rewardAd.Dispose();
//            rewardAd = null;
//        }

//        var adSlot = new AdSlot.Builder()
//            .SetCodeId(AndroidSlotID)
//            .SetSupportDeepLink(true)
//            .SetImageAcceptedSize(1080, 1920)
//            .SetUserID("TestUser2") // 用户id,必传参数
//            //.SetMediaExtra("media_extra") // 附加参数，可选
//            .SetOrientation(AdOrientation.Vertical) // 必填参数，期望视频的播放方向
//            .SetAdLoadType(AdLoadType.Load)
//            .Build();

//        var li = new AdListener();
//        li.ExpressAdCallback += ExpressCallbackAction;
//        li.RewardAdCallback += RewardAdCallbackAction;
//        li.OnErrorCallback += ErrorCallbackAction;
//        AdNative.LoadRewardVideoAd(adSlot, li);

//        void ExpressCallbackAction(ExpressRewardVideoAd ad, string message)
//        {
//            li.ExpressAdCallback -= ExpressCallbackAction;
//            li.RewardAdCallback -= RewardAdCallbackAction;
//            li.OnErrorCallback -= ErrorCallbackAction;
//            if (ad == null)
//            {
//                requestAction?.Invoke(false, "无广告源.");
//                return;
//            }

//            ad?.ShowRewardVideoAd();
//            requestAction?.Invoke(true, "加载成功.");
//        }

//        void RewardAdCallbackAction(RewardVideoAd ad, string message)
//        {
//            li.ExpressAdCallback -= ExpressCallbackAction;
//            li.RewardAdCallback -= RewardAdCallbackAction;
//            li.OnErrorCallback -= ErrorCallbackAction;
//            if (ad == null)
//            {
//                requestAction?.Invoke(false, "无广告源.");
//                return;
//            }

//            ad?.ShowRewardVideoAd();
//            requestAction?.Invoke(true, "加载成功.");
//        }

//        void ErrorCallbackAction(string message)
//        {
//            li.ExpressAdCallback -= ExpressCallbackAction;
//            li.RewardAdCallback -= RewardAdCallbackAction;
//            li.OnErrorCallback -= ErrorCallbackAction;
//            requestAction?.Invoke(false, message);
//        }
//    }

//    private class AdListener : IRewardVideoAdListener
//    {
//        public event UnityAction<RewardVideoAd, string> RewardAdCallback;
//        public event UnityAction<ExpressRewardVideoAd ,string> ExpressAdCallback;
//        public event UnityAction<string> OnErrorCallback;
//        public void OnError(int code, string message) => OnErrorCallback?.Invoke(message);

//        public void OnRewardVideoAdLoad(RewardVideoAd ad) => RewardAdCallback?.Invoke(ad, "加载成功");
//        public void OnRewardVideoCached()
//        {

//        }
//        public void OnExpressRewardVideoAdLoad(ExpressRewardVideoAd ad) => ExpressAdCallback?.Invoke(ad, "加载成功");
//        public void OnRewardVideoCached(RewardVideoAd ad) => RewardAdCallback?.Invoke(ad, "加载成功");
//    }
//}
