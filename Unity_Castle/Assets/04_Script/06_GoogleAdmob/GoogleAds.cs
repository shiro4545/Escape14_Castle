using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    public static GoogleAds Instance { get; private set; }

    //動画情報の取得有無
    public bool isGetMovie = true;

    private BannerView HeaderBannerView;
    private BannerView FooterBannerView;
    private BannerView squareBannerView;
    private InterstitialAd interstitial;
    private InterstitialAd interstitial2;
    private RewardedAd rewardedAd;

    public HintManager HintClass;

    public void Start()
    {
        Instance = this;
    }

    public void AdsInitial()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        //課金有無でバナー表示・非表示
        if (!SaveLoadSystem.Instance.gameData.isPurchase)
        {
            //ヘッダーバナー
            this.RequestHeaderBanner();
            //フッターバナー
            this.RequestFooterBanner();
        }


        //インタースティシャル(静止)
        this.RequestInterstitial();
        //インタースティシャル(動画)
        this.RequestInterstitial2();
        //リワード広告(動画)
        this.RequestReward();
    }

    //**********************************
    //**バナー広告(ヘッダー)
    //**********************************

    //画面上のバナーの表示
    private void RequestHeaderBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/7563395981"; //Android
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/2502640998"; //iOS
#else
        //テスト
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        HeaderBannerView = new BannerView(adUnitId, adSize, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        HeaderBannerView.LoadAd(request);
    }

    //**********************************
    //**バナー広告(フッター)
    //**********************************

    //画面下のバナーの表示
    private void RequestFooterBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/8911356427"; //Android
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/6350386668"; //iOS
#else
        //テスト
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        //バナーサイズ
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        FooterBannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        FooterBannerView.LoadAd(request);
    }



    //上下バナーの非表示
    public void unRequestBanner()
    {
        HeaderBannerView.Destroy();
        FooterBannerView.Destroy();
    }
    //**********************************
    //**バナー広告(Menu画面内の長方形)
    //**********************************
    //Menu画面内のバナーの表示
    public void RequestSquareBanner()
    {
#if UNITY_ANDROID
         string adUnitId = ""; //Android
#elif UNITY_IOS
        string adUnitId = ""; //iOS
#else
        //テスト
         string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        // Create a banner at the top of the screen.
        this.squareBannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.squareBannerView.LoadAd(request);
    }

    //Menu画面内のバナーの非表示
    public void unRequestSquareBanner()
    {
        squareBannerView.Destroy();
    }

    //**********************************
    //**インタースティシャル広告(静止)
    //**********************************
    //読み込み
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/6250314311"; //Andorid
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/7020470500"; //iOS
#else
        string adUnitId = "ca-app-pub-3940256099942544/8691691433";
#endif

        InterstitialAd.Load(adUnitId, new AdRequest(),
          (InterstitialAd ad, LoadAdError loadAdError) =>
          {
              if (loadAdError != null)
              {
                  // Interstitial ad failed to load with error
                  //interstitial.Destroy();
                  return;
              }
              else if (ad == null)
              {
                  // Interstitial ad failed to load.
                  return;
              }
              ad.OnAdFullScreenContentClosed += () => {
                  HandleOnAdClosed();
              };
              ad.OnAdFullScreenContentFailed += (AdError error) =>
              {
                  HandleOnAdClosed();
              };
              interstitial = ad;
          });
    }
    //広告表示
    public void ShowInterstitialAd()
    {
        if (SaveLoadSystem.Instance.gameData.isPurchase)
            return;

        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial Ad not load");
        }
    }
    //広告非表示
    public void HandleOnAdClosed()
    {
        interstitial.Destroy();
        RequestInterstitial();
    }

    //**********************************
    //**インタースティシャル広告(動画)
    //**********************************
    //読み込み
    public void RequestInterstitial2()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/3624150978"; //Andorid
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/2829944050"; //iOS
#else
        string adUnitId = "ca-app-pub-3940256099942544/8691691433";
#endif

        InterstitialAd.Load(adUnitId, new AdRequest(),
          (InterstitialAd ad, LoadAdError loadAdError) =>
          {
              if (loadAdError != null)
              {
                  // Interstitial ad failed to load with error
                  //interstitial2.Destroy();
                  return;
              }
              else if (ad == null)
              {
              // Interstitial ad failed to load.
              return;
              }
              ad.OnAdFullScreenContentClosed += () => {
                  HandleOnAdClosed2();
              };
              ad.OnAdFullScreenContentFailed += (AdError error) =>
              {
                  HandleOnAdClosed2();
              };
              interstitial2 = ad;
          });
    }
    //広告表示
    public void ShowInterstitialAd2()
    {
        if (SaveLoadSystem.Instance.gameData.isPurchase)
            return;

        if (interstitial2 != null && interstitial2.CanShowAd())
        {
            interstitial2.Show();
        }
        else
        {
            Debug.Log("Interstitial Ad not load");
        }
    }
    //広告非表示
    public void HandleOnAdClosed2()
    {
        interstitial2.Destroy();
        RequestInterstitial2();
    }

    //**********************************
    //**リワード広告
    //**********************************

    /// <summary>
    /// 動画のロード
    /// </summary>
       public void RequestReward()
    {
        string adUnitId = "";
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-7464443980940177/5707388833"; //Android
#elif UNITY_IOS
        adUnitId = "ca-app-pub-7464443980940177/6769189069"; //iOS
#else
        //テスト
        adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif

        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.Log(ad);
                    Debug.Log(error);
                    isGetMovie = false;
                    return;
                }

                RegisterEventHandlers(ad);
                RegisterReloadHandler(ad);
                isGetMovie = true;
                rewardedAd = ad;
            });
    }


    public void ShowReawrd()
    {

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                //ヒントを表示
                HintClass.AfterWatch();
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        //収益発生時
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
            //    adValue.Value,
            //    adValue.CurrencyCode));
        };
        // インプレッション発生時
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Rewarded ad recorded an impression.");
        };
        // 広告がクリックされた時
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded ad was clicked.");
        };
        // 全画面広告が開いた時
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Rewarded ad full screen content opened.");
        };
        // 全画面広告を閉じた時
        ad.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Rewarded ad full screen content closed.");
        };
        // 全画面広告を開けなかった時
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " +
            //"with error : " + error);
        };
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // 全画面広告を閉じた時
        ad.OnAdFullScreenContentClosed += () =>
        {
            RequestReward();
        };
        // 全画面広告を開けなかった時
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            RequestReward();
        };
    }
}
