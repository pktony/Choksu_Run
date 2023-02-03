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

    public InterstitialAd InterstitialAd => interstitialAd;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (interstitialAd == null)
        {
            MobileAds.Initialize(initCompleteAction => { });
        }
    }

    private void Update()
    {
        //if(UnityEngine.InputSystem.Keyboard.current.digit1Key.wasPressedThisFrame)
        //{
        //    ShowAd(AdType.Interstitial);
        //}
    }

    private void RequestAds()
    {
        string id ;
#if UNITY_EDITOR
        id = ANDROID_ID;
#elif UNITY_ANDROID
        id = ANDROID_ID;
#else
        id = TEST_ID;
#endif

        interstitialAd = new InterstitialAd(id);
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    public void ShowAd(AdType adType)
    {
        RequestAds();
        switch(adType)
        {
            case AdType.Banner:
                break;
            case AdType.Interstitial:
                interstitialAd.Show();
                break;
            case AdType.Reward:
                break;
            default:
                break;
        }
    }
}
