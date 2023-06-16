using UnityEngine;
using GoogleMobileAds.Api;

using Define;
using System;

using Random = UnityEngine.Random;

public class AdManager : Singleton<AdManager>
{
    //private const string TEST_ID = "ca-app-pub-3940256099942544~3347511713";
    [SerializeField] private string TEST_ID = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private string Interstitial_AOS = "ca-app-pub-1558168203898482~4627346428";
    [SerializeField] private string bannerID = "ca-app-pub-1558168203898482/8440889654";

    private InterstitialAd interstitialAd;
    private BannerView bannerView;
    private RewardedAd rewardedAd;

    [Header("±¤°í °ü·Ã")]
    [SerializeField] AdType gameStartAd;
    [Range(0f, 1f)]
    [SerializeField] float adExposureProbability;

    public InterstitialAd InterstitialAd => interstitialAd;

    protected override void Awake()
    {
        base.Awake();

        MobileAds.Initialize(initStatus =>
        {
            LoadInterstitialAd();

            Debug.Log("Googld Admob Initialized");
        });
    }

    public bool IsAdShow()
    {
        var randValue = Random.Range(0f, 1f);
        if (randValue < adExposureProbability)
        {
            return true;
        }

        return false;
    }

    private void LoadInterstitialAd()
    {
        InterstitialAd.Load(Interstitial_AOS, new AdRequest(),
              (InterstitialAd ad, LoadAdError error) =>
              {
                  if (error != null)
                  {
                      Debug.LogError($"Failed to Load interstitial : {error}");
                      return;
                  }

                  interstitialAd = ad;
              });
    }

    private void Update()
    {
        //if(UnityEngine.InputSystem.Keyboard.current.digit1Key.wasPressedThisFrame)
        //{
        //    ShowAd(AdType.Interstitial);
        //}
    }

    private string RequestAdID(AdType adType)
    {
        string id ;

        if (Application.platform == RuntimePlatform.Android)
        {
            switch(adType)
            {
                case AdType.Interstitial:
                    id = Interstitial_AOS;
                    break;
                case AdType.Banner:
                    id = bannerID;
                    break;
                default:
                    id = TEST_ID;
                    break;
            }
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

    public void ShowAd(AdType adType, Action adLoadAction)
    {
        string adID = RequestAdID(adType);
        switch (adType)
        {
            case AdType.Banner:
                if(bannerView == null)
                {
                    bannerView = new BannerView(adID, AdSize.Banner, AdPosition.Bottom);
                }

                AdRequest adRequest = new AdRequest();
                bannerView.LoadAd(adRequest);
                bannerView.OnAdFullScreenContentClosed += () =>
                {
                    bannerView.Destroy();
                };
                adLoadAction?.Invoke();

                break;
            case AdType.Interstitial:
                if(interstitialAd == null || !interstitialAd.CanShowAd())
                {
                    Debug.LogWarning($"Cannot Show {adType}");
                    LoadInterstitialAd();
                }
                interstitialAd.Show();
                interstitialAd.OnAdFullScreenContentClosed += () =>
                {
                    adLoadAction?.Invoke();
                    interstitialAd.Destroy();
                    interstitialAd = null;
                };
                break;
            case AdType.Reward:
                new System.NotImplementedException();
                //rewardedAd = new RewardedAd(adID);
                //rewardedAd.Show();
                break;
            default:
                break;
        }
    }   
}
