using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using Define;
using System;

public class AdManager : Singleton<AdManager>
{
    private const string TEST_ID = "ca-app-pub-3940256099942544~3347511713";
    private const string ANDROID_ID = "ca-app-pub-1558168203898482~1386105414";

    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    public InterstitialAd InterstitialAd => interstitialAd;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (interstitialAd == null)
        {
            MobileAds.Initialize(initCompleteAction =>
            { Debug.Log("Googld Admob Initialized"); });
        }
    }

    private void Update()
    {
        //if(UnityEngine.InputSystem.Keyboard.current.digit1Key.wasPressedThisFrame)
        //{
        //    ShowAd(AdType.Interstitial);
        //}
    }

    private string RequestAds()
    {
        string id ;
#if UNITY_EDITOR
        id = ANDROID_ID;
#elif UNITY_ANDROID
        id = ANDROID_ID;
#else
        id = TEST_ID;
#endif
        return id;
    }

    public void ShowAd(AdType adType, Action<object, EventArgs> adLoadAction)
    {
        string adID = RequestAds();
        switch (adType)
        {
            case AdType.Banner:
                GameObject bannerObj = new("bannerObj");
                var banner = bannerObj.AddComponent<BannerAd>();
                banner.SetBanner(adID, adLoadAction);
                banner.ShowAd();
                break;
            case AdType.Interstitial:
                interstitialAd.Show();
                interstitialAd = new InterstitialAd(adID);
                AdRequest request = new AdRequest.Builder().Build();
                interstitialAd.LoadAd(request);
                interstitialAd.OnAdClosed += (arg, arg2) => { adLoadAction?.Invoke(arg, arg2);};
                break;
            case AdType.Reward:
                rewardedAd = new RewardedAd(adID);
                rewardedAd.Show();
                Debug.LogWarning("Rewarded Ad Not Impletemented");
                break;
            default:
                break;
        }
    }   
}
