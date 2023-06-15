using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class BannerAd : MonoBehaviour
{
    private BannerView bannerAd;
    private string adID;
    private string keyword;

    private Action<object, EventArgs> onBannerLoadedListener = null;

    public void SetBanner(string adID, Action<object, EventArgs> onBannerLoaded, string keyword = "unity-admob-sample")
    {
        this.adID = adID;
        this.keyword = keyword;

        onBannerLoadedListener += onBannerLoaded ;

        DontDestroyOnLoad(this);
    }

    public void ShowAd()
    {
        bannerAd = new BannerView(adID, AdSize.Banner, AdPosition.Bottom);
        //var adRequest = new AdRequest.Builder()
        //.AddKeyword(keyword).Build();

        //bannerAd.OnAdLoaded += (arg0,arg1) => onBannerLoadedListener?.Invoke(arg0, arg1);
        //bannerAd.LoadAd(adRequest);
    }
}
