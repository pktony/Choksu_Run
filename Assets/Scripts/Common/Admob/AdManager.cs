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
    private const string Interstitial_AOS = "\nca-app-pub-1558168203898482~4627346428";

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

    private string RequestAds(AdType adType)
    {
        string id ;

        if (Application.platform == RuntimePlatform.Android)
        {
            id = Interstitial_AOS;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            id = string.Empty;
        }
        else
        {
            id = TEST_ID;
        }

        return id;
    }

    public void ShowAd(AdType adType, Action<object, EventArgs> adLoadAction)
    {
        string adID = RequestAds(adType);
        switch (adType)
        {
            case AdType.Banner:
                GameObject bannerObj = new("bannerObj");
                var banner = bannerObj.AddComponent<BannerAd>();
                banner.SetBanner(adID, adLoadAction);
                banner.ShowAd();
                break;
            case AdType.Interstitial:
                interstitialAd = new InterstitialAd(adID);
                AdRequest request = new AdRequest.Builder().Build();
                interstitialAd.LoadAd(request);
                interstitialAd.OnAdClosed += (arg, arg2) => { adLoadAction?.Invoke(arg, arg2);};
                interstitialAd.Show();
                break;
            case AdType.Reward:
                rewardedAd = new RewardedAd(adID);
                Debug.LogWarning("Rewarded Ad Not Impletemented");
                rewardedAd.Show();
                break;
            default:
                break;
        }
    }   
}
