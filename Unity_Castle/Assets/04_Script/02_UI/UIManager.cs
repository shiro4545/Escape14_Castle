using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //広告クラス
    public IDFA _IDFA;

    //Panelオブジェクト
    public GameObject TitlePanel;
    public GameObject StartCheckPanel;
    public GameObject GamePanel;
    public GameObject MenuPanel;
    public GameObject HintPanel;
    public GameObject SoundPanel;
    public GameObject ClearPanel;
    public GameObject ItemPanel;
    public GameObject StoryPanel;


    //タイトル
    public GameObject Title;
    public GameObject BtnTitle_Start;
    public GameObject BtnTitle_Continue;

    //はじめから確認パネル内
    public GameObject Title2;
    public GameObject YesNo;
    public GameObject CheckTxt;
    public GameObject BtnStart_Yes;
    public GameObject BtnStart_No;


    //GamePanel内
    public GameObject GameHeader;
    public GameObject GameFooter;
    //ヘッダー
    public GameObject BtnHeader_Menu;
    public GameObject BtnHeader_Hint;
    //フッター
    public GameObject BtnFooter_Left;
    public GameObject BtnFooter_Right;
    public GameObject BtnFooter_Back;

    //メニューパネル
    public GameObject BtnMenu_Hint;
    public GameObject BtnMenu_Sound;
    public GameObject BtnMenu_Title;
    public GameObject BtnMenu_Back;

    //ヒントパネル内
    public GameObject BtnHint_Back;
    public GameObject BtnHint_Back2;
    public GameObject BtnHint_ToPur;

    //ブロック時のひらめきアイコン
    public GameObject LightIcon;

    //BGM・効果音パネル内
    public GameObject ImgSound_BGM;
    public GameObject BtnSound_BGM;
    public GameObject ImgSound_SE;
    public GameObject BtnSound_SE;
    public GameObject BtnSound_Back;

    //クリアパネル内
    public GameObject ImageClear;
    public GameObject BtnClear_OtherApp;
    public GameObject BtnClear_Title;

    //ストーリーパネル内
    public GameObject BtnStory_Close;
    public GameObject TxtStory_Start;
    public GameObject TxtStory_Mid;
    public GameObject TxtStory_Arrow;

    //ATTパネル内
    public GameObject ATTPanel;
    public GameObject BtnATT;

    //課金パネル内
    public GameObject PurchasePanel;
    public GameObject TxtPurchase;
    public GameObject BtnPur_Purchase;
    public GameObject BtnPur_Restore;
    public GameObject BtnPur_Back;

    //ローディングパネル
    public GameObject SpinnerPanel;



    //アイテム拡大画像
    public GameObject ItemImage;

    //ヒントクラス
    public HintManager HintClass;
    //ゲームスタートクラス
    public StartResetManager StartResetClass;
    //クリアクラス
    public ClearManager ClearClass;
    //広告クラス
    public GoogleAds GoogleAds;
    //IDFAクラス
    public IDFA IDFAClass;
    //iOS IAPクラス
    public IAPiOS IAPiOSClass;

    //タイトル画面用
    public GameObject ForTitle;

    //セーブデータ有無フラグ
    private bool isExistFile;
    //全クリアフラグ
    private bool _isClearAll;
    //続きからか
    private bool isContinue;

    //オーディオオブジェクト
    public GameObject audioSE;
    public GameObject audioBGM;
    //BGMボリューム
    private const float ConstBGM = 0.2f;
    //SEボリューム
    private const float ConstSE = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        //パネルの初期化
        TitlePanel.SetActive(true);
        GamePanel.SetActive(false);
        //GamePanel.SetActive(true);

        //セーブデータの有無→有ればデータロード
        isExistFile = SaveLoadSystem.Instance.CheckFileExist();


        //セーブデータがあればサウンドの切替
        if (isExistFile)
            ChangeSound();

        //セーブデータがあれば全クリ有無を判定
        if (isExistFile)
        {
            if (SaveLoadSystem.Instance.gameData.isClearAll)
                _isClearAll = true;
        }
        //「続きから」ボタンの活性切替
        if (!isExistFile || _isClearAll)
            BtnTitle_Continue.GetComponent<Button>().interactable = false;

        if (!isExistFile)
            SaveLoadSystem.Instance.GameStart();

        //IDFA表示
        IDFAClass.Initial();

        //広告クラス初期化
        GoogleAds.Instance.AdsInitial();
            




        //画面サイズごとにUIサイズと位置を調整
        AjudstSizePosition();


        //***ボタン処理を登録*************

        //タイトル画面の「はじめから」
        BtnTitle_Start.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapStart();
        });
        //タイトル画面の「続きから」
        BtnTitle_Continue.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapContinue();
        });
        //はじめから確認画面のYES
        BtnStart_Yes.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapStartYes();
        });
        //はじめから確認画面のNo
        BtnStart_No.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapStartNo();
        });
        //ヘッダー画面の「MENU」
        BtnHeader_Menu.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapMenu();
        });
        //ヘッダー画面の「Hint」
        BtnHeader_Hint.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHeadHint();
        });
        //メニュー画面の「ヒント」
        BtnMenu_Hint.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHint();
        });
        //メニュー画面の「効果音・BGM」
        BtnMenu_Sound.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapSound();
        });
        //メニュー画面の「タイトルへ」
        BtnMenu_Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapTitle();
        });
        //メニュー画面の「ゲームに戻る」
        BtnMenu_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapMenuBack();
        });
        //ヒント画面の「ゲームに戻る」
        BtnHint_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHintBack();
        });
        //ヒント画面の「ゲームに戻る」2
        BtnHint_Back2.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHintBack();
        });
        //ヒント画面の「課金へ」
        BtnHint_ToPur.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapHintToPur();
        });
        //サウンド画面の「BGM」トグル
        BtnSound_BGM.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapSoundBGM();
        });
        //サウンド画面の「効果音」トグル
        BtnSound_SE.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapSoundSE();
        });
        //サウンド画面の「ゲームに戻る」
        BtnSound_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapSoundBack();
        });
        //クリア画面の「おすすめアプリ」
        BtnClear_OtherApp.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapClearOtherApp();
        });
        //クリア画面の「タイトルへ」
        BtnClear_Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapClearTitle();
        });
        //ストーリー画面の「Close」
        BtnStory_Close.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapStoryClose();
        });
        //ATT推奨画面の「画像全体」
        BtnATT.GetComponent<Button>().onClick.AddListener(() =>
        {
            HideATTAPnel();
        });
        //課金画面の「購入」
        BtnPur_Purchase.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapPurchase();
        });
        //課金画面の「復元」
        BtnPur_Restore.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapRestore();
        });
        //課金画面の「ゲームに戻る」
        BtnPur_Back.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnTapPurBack();
        });


    }
    /// タップ処理 //////////////////////////////////////////////////////////////////
    
    //タイトル画面の「はじめから」ボタン
    private void OnTapStart()
    {
        if (isExistFile && !_isClearAll)
        {
            //セーブデータがあって、かつ、全クリしてない場合
            AudioManager.Instance.SoundSE("TapUIBtn");
            TitlePanel.SetActive(false);
            StartCheckPanel.SetActive(true);
        }
        else
        {
            //セーブデータがない、もしくは全クリしている
            isExistFile = true;
            BlockPanel.Instance.ShowBlockNoIcon();
            AudioManager.Instance.SoundSE("GameStart");
            if(_isClearAll)
                SaveLoadSystem.Instance.GameStart2nd();

            isContinue = false;
            StartCoroutine("FadeStart");
        }
    }

    //タイトル画面の「続きから」ボタン
    private void OnTapContinue()
    {
        BlockPanel.Instance.ShowBlockNoIcon();
        AudioManager.Instance.SoundSE("GameStart");
        Invoke(nameof(ObjectLoad), 2.4f);
        isContinue = true;
        StartCoroutine("FadeStart");
    }
    //データロード
    private void ObjectLoad()
    {
        StartResetClass.GameContinue();
    }

    //はじめから確認画面のYESボタン
    private void OnTapStartYes()
    {
        BlockPanel.Instance.ShowBlockNoIcon();
        AudioManager.Instance.SoundSE("GameStart");
        SaveLoadSystem.Instance.GameStart2nd();
        isContinue = false;
        StartCoroutine("FadeStart");
    }

    //はじめから確認画面のNoボタン
    private void OnTapStartNo()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        StartCheckPanel.SetActive(false);
        TitlePanel.SetActive(true);
    }

    //スタート時のフェード
    IEnumerator FadeStart()
    {
        FadeManager.Instance.Panel.SetActive(true);
        // フェード後の色を設定（黒）★変更可
        FadeManager.Instance.fade.color = new Color(1, 1, 1, (0.0f / 255.0f));
        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 2f;
        // ループ回数（0はエラー）★変更可
        const int loop_count = 15;
        // ウェイト時間算出
        float wait_time = fade_time / loop_count;
        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;
        // フェードイン
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = FadeManager.Instance.fade.color;
            new_color.a = alpha / 255.0f;
            FadeManager.Instance.fade.color = new_color;
        }

        CameraManager.Instance.ChangeCameraPosition(CameraManager.Instance.StartPosition);
        TitlePanel.SetActive(false);
        StartCheckPanel.SetActive(false);
        //タイトル画面用
        //ForTitle.SetActive(false);
        BtnTitle_Continue.GetComponent<Button>().interactable = true;
        if (isContinue)
        {
            GamePanel.SetActive(true);
            BlockPanel.Instance.HideBlock();
        }

        // フェードアウト
        for (float alpha = 255.0f; alpha >= 0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = FadeManager.Instance.fade.color;
            new_color.a = alpha / 255.0f;
            FadeManager.Instance.fade.color = new_color;
        }

        FadeManager.Instance.Panel.SetActive(false);

        if (!isContinue)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine("FadeStory");
        }
    }

    //ヘッダーの「MENU」ボタン
    private void OnTapMenu()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(true);
        //バナー広告表示(長方形)
        //GoogleAds.RequestSquareBanner();
    }
    //ヘッダーの「Hint」ボタン
    private void OnTapHeadHint()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        BtnHint_Back.SetActive(false);
        BtnHint_Back2.SetActive(false);
        BtnHint_ToPur.SetActive(false);

        //課金ボタンの表示・非表示
        if (!SaveLoadSystem.Instance.gameData.isPurchase
            && Application.platform == RuntimePlatform.IPhonePlayer
            && IAPiOSClass.IsInitialized())
            //&& GoogleAda.Instance.isGetMovie)
        {
            BtnHint_Back2.SetActive(true);
            BtnHint_ToPur.SetActive(true);
        }
        else
            BtnHint_Back.SetActive(true);

        HintPanel.SetActive(true);
        //進捗に応じてヒントを表示する
        HintClass.SetHint();
    }
    //メニュー画面の「ヒント」ボタン
    private void OnTapHint()
    {
        MenuPanel.SetActive(false);
        OnTapHeadHint();
    }
    //メニュー画面の「効果音・BGM」ボタン
    private void OnTapSound()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(false);
        SoundPanel.SetActive(true);
        //インタースティシャル広告
        GoogleAds.Instance.ShowInterstitialAd();
    }
    //メニュー画面の「タイトルへ」ボタン
    private void OnTapTitle()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        TitlePanel.SetActive(true);
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
        ItemPanel.SetActive(false);

        CameraManager.Instance.ChangeCameraPosition("Title");
        //タイトル画面用
        //ForTitle.SetActive(true);
        MenuPanel.SetActive(false);
        //オブジェクトリセット
        StartResetClass.ResetObject();
    }

    //メニュー画面の「ゲームに戻る」ボタン
    private void OnTapMenuBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        MenuPanel.SetActive(false);
    }
    //ヒント画面の「ゲームに戻る」ボタン
    private void OnTapHintBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        HintPanel.SetActive(false);
        HintClass.ResetHint();
    }
    //ヒント画面の「課金へ」ボタン
    private void OnTapHintToPur()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        HintPanel.SetActive(false);
        HintClass.ResetHint();
        PurchasePanel.SetActive(true);
    }
    //課金画面の「購入」ボタン
    private void OnTapPurchase()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        SpinnerPanel.SetActive(true);
        IAPiOSClass.BuyNonConsumable();
    }
    //課金画面の「復元」ボタン
    private void OnTapRestore()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        SpinnerPanel.SetActive(true);
        IAPiOSClass.RestorePurchases();
    }
    //課金画面の「ゲームに戻る」ボタン
    private void OnTapPurBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        PurchasePanel.SetActive(false);
    }
    //サウンド画面の「BGM」トグル
    private void OnTapSoundBGM()
    {
        if(SaveLoadSystem.Instance.gameData.VolumeBGM == 0)
            SaveLoadSystem.Instance.gameData.VolumeBGM = ConstBGM;
        else
            SaveLoadSystem.Instance.gameData.VolumeBGM = 0;

        SaveLoadSystem.Instance.Save();
        ChangeBGMToggle();
        AudioManager.Instance.ChangeVolumeBGM();
    }
    //サウンド画面の「SE」トグル
    private void OnTapSoundSE()
    {
        if (SaveLoadSystem.Instance.gameData.VolumeSE == 0)
            SaveLoadSystem.Instance.gameData.VolumeSE = ConstSE;
        else
            SaveLoadSystem.Instance.gameData.VolumeSE = 0;

        SaveLoadSystem.Instance.Save();
        ChangeSEToggle();
        AudioManager.Instance.ChangeVolumeSE();

        if(SaveLoadSystem.Instance.gameData.VolumeSE != 0)
            AudioManager.Instance.SoundSE("TapUIBtn");
    }
    //サウンド画面の「ゲームに戻る」ボタン
    private void OnTapSoundBack()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        SoundPanel.SetActive(false);
    }

    //クリア画面の「他の脱出ゲーム」ボタン
    private void OnTapClearOtherApp()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        //URLを開く
#if UNITY_IOS
        Application.OpenURL("https://apps.apple.com/jp/developer/shiro-miyahara/id1538525165");
#elif UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8452907723810852629");
#endif
    }

    //クリア画面の「タイトルへ」ボタン
    private void OnTapClearTitle()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        //タイトルから遷移時
        ClearPanel.SetActive(false);
        TitlePanel.SetActive(true);
        CameraManager.Instance.ChangeCameraPosition("Title");

        //タイトル画面用
        //ForTitle.SetActive(true);
        //クリア画面用の部分
         ClearClass.ForEnd.SetActive(false);
         //ClearClass.NotEnd.SetActive(true);

        _isClearAll = true;
        BtnTitle_Continue.GetComponent<Button>().interactable = false;

        //オブジェクトリセット
        StartResetClass.ResetObject();
    }

    //ストーリー画面のフェード表示
    IEnumerator FadeStory()
    {
        Image PanelImg = StoryPanel.GetComponent<Image>();
        Image StoryImg = TxtStory_Start.GetComponent<Image>();
        Image CloseImg = BtnStory_Close.GetComponent<Image>();
        PanelImg.color = new Color(0, 0, 0, 0);
        StoryImg.color = new Color(1, 1, 1, 0);
        CloseImg.color = new Color(1, 1, 1, 0);
        BlockPanel.Instance.HideBlock();
        StoryPanel.SetActive(true);
        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 2f;
        // ループ回数（0はエラー）★変更可
        const int loop_count = 30;
        // ウェイト時間算出
        float wait_time = fade_time / loop_count;
        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;
        // フェードイン
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            if(alpha < 110)
            {
                Color newColor_Panel = PanelImg.color;
                newColor_Panel.a = alpha / 255.0f;
                PanelImg.color = newColor_Panel;
            }
            Color newColor_Txt = StoryImg.color;
            newColor_Txt.a = alpha / 255.0f;
            StoryImg.color = newColor_Txt;
            CloseImg.color = newColor_Txt;
        }
    }


    //ストーリー画面の「Close」ボタン
    private void OnTapStoryClose()
    {
        AudioManager.Instance.SoundSE("TapUIBtn");
        TxtStory_Start.GetComponent<Image>().color = new Color(1,1,1,0);
        TxtStory_Mid.GetComponent<Image>().color = new Color(1,1,1,0);
        TxtStory_Arrow.GetComponent<Image>().color = new Color(1,1,1,0);
        StoryPanel.SetActive(false);
        GamePanel.SetActive(true);

        //if(SaveLoadSystem.Instance.gameData.isGetLetter)
            //Google.Instance.ShowInterstitialAd2(); //インタースティシャル動画
    }

    //IDFAの表示
    public void HideATTAPnel()
    {
        ATTPanel.SetActive(false);
        SaveLoadSystem.Instance.gameData.isATT = true;
        SaveLoadSystem.Instance.Save();
        _IDFA.ShowIDFA();
    }



    /// <summary>
    /// サウンドの初期切替
    /// </summary>
    private void ChangeSound()
    {
        //BGM音量切替
        AudioSource _audioBGM = audioBGM.GetComponent<AudioSource>();
        _audioBGM.volume = SaveLoadSystem.Instance.gameData.VolumeBGM;
        //SE音量切替
        AudioSource _audioSE = audioSE.GetComponent<AudioSource>();
        _audioSE.volume = SaveLoadSystem.Instance.gameData.VolumeSE;

        //BGMボタン切替
        ChangeBGMToggle();
        //SEボタン切替
        ChangeSEToggle();
    }

    /// <summary>
    /// BGMのトグル画像変更
    /// </summary>
    public void ChangeBGMToggle()
    {
        if(SaveLoadSystem.Instance.gameData.VolumeBGM == 0)
            BtnSound_BGM.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_UI/Btn_off");
        else
            BtnSound_BGM.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_UI/Btn_on");
    }
    /// <summary>
    /// SEのトグル画像変更
    /// </summary>
    public void ChangeSEToggle()
    {
        if (SaveLoadSystem.Instance.gameData.VolumeSE == 0)
            BtnSound_SE.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_UI/Btn_off");
        else
            BtnSound_SE.GetComponent<Image>().sprite = Resources.Load<Sprite>("02_UI/Btn_on");
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UIパースの位置とサイズ調整
    /// </summary>
    private void AjudstSizePosition()
    {

        //各パネルを画面サイズごとで変動させる
        GamePanel.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        ClearPanel.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;

        float _width = Screen.width;
        float _height = Screen.height;


        if (Application.platform == RuntimePlatform.Android) //Androidの場合
        {
            //Debug.Log("Android");
            //ヘッダーフッター
            GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -185 - (Screen.height - Screen.safeArea.height));
            GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
            //メニューパネル
            MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height);
            //ヒントパネル
            HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
            //サウンドパネル
            SoundPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
            //アイテムパネル
            ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 940);
            //ブロック
            LightIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, Screen.height / 2 * 0.4f);
        }
        else //iOSの場合
        {
            if (_width <= 750) //iPhone8,SE
            {
                //Debug.Log("iPhoneSE");
                //タイトル
                Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 440);
                BtnTitle_Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -335);
                BtnTitle_Continue.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -519);
                //はじめから確認パネル
                Title2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 440);
                YesNo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -185);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                BtnMenu_Hint.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -140);
                BtnMenu_Sound.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -270);
                BtnMenu_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -400);
                BtnMenu_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -530);

                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //サウンドパネル
                SoundPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                ImgSound_BGM.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 10);
                BtnSound_BGM.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -90);
                ImgSound_SE.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -260);
                BtnSound_SE.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -360);
                BtnSound_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -530);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 940);
                //クリアパネル
                BtnClear_OtherApp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -360);
                BtnClear_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -510);
                //ブロック
                LightIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 380);
                //ストーリー
                BtnStory_Close.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -530);
                //課金パネル
                BtnPur_Purchase.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -220);
                BtnPur_Restore.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -360);
                BtnPur_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -510);
            }
            //iPhone7plus,8plus
            else if (_height / _width > 1.6f && _height / _width < 2)
            {
                //Debug.Log("iPhone8+");
                //タイトル
                Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 440);
                BtnTitle_Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -318);
                BtnTitle_Continue.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -502);
                //はじめから確認パネル
                Title2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 440);
                YesNo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -182);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 240);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                BtnMenu_Hint.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -145);
                BtnMenu_Sound.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -275);
                BtnMenu_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -405);
                BtnMenu_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -535);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                //サウンドパネル
                SoundPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -200);
                ImgSound_BGM.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 10);
                BtnSound_BGM.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -90);
                ImgSound_SE.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -260);
                BtnSound_SE.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -360);
                BtnSound_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -535);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 650);
                //クリアパネル
                BtnClear_OtherApp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);
                BtnClear_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -500);
                //ブロック
                LightIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 385);
                //ストーリー
                BtnStory_Close.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -535);
                //課金パネル
                BtnPur_Purchase.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -220);
                BtnPur_Restore.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -360);
                BtnPur_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -510);
            }
            //iPhone10以上
            else if (_width <= 1300)
            {
                //Debug.Log("iPhone12");
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -295);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 300);
                //ブロック
                LightIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 430);
            }
            else //iPad
            {
                //Debug.Log("iPad");
                //タイトルパネル
                Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 210);
                BtnTitle_Start.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 25);
                BtnTitle_Continue.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 25);
                BtnTitle_Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250);
                BtnTitle_Continue.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -400);
                //はじめから確認パネル
                Title2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 210);
                YesNo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -210);
                //ヘッダーフッター
                GameHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -135);
                GameFooter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200);
                BtnFooter_Left.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 80);
                BtnFooter_Right.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 80);
                BtnFooter_Back.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 90);
                BtnFooter_Left.GetComponent<RectTransform>().anchoredPosition = new Vector2(160, 0);
                BtnFooter_Right.GetComponent<RectTransform>().anchoredPosition = new Vector2(-160, 0);
                //メニューパネル
                MenuPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 920);
                BtnMenu_Hint.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                BtnMenu_Sound.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                BtnMenu_Title.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                BtnMenu_Back.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                BtnMenu_Hint.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -105);
                BtnMenu_Sound.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -205);
                BtnMenu_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -305);
                BtnMenu_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -405);
                //ヒントパネル
                HintPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -140);
                BtnHint_Back.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                //サウンドパネル
                SoundPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -140);
                ImgSound_BGM.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 150);
                BtnSound_BGM.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 100);
                ImgSound_SE.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 150);
                BtnSound_SE.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 100);
                BtnSound_Back.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                ImgSound_BGM.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 10);
                BtnSound_BGM.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -70);
                ImgSound_SE.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -190);
                BtnSound_SE.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -270);
                BtnSound_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -405);
                //アイテムパネル
                ItemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 680);
                ItemImage.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
                //クリアパネル
                ImageClear.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 300);
                BtnClear_OtherApp.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                BtnClear_Title.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                BtnClear_OtherApp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -230);
                BtnClear_Title.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -360);
                //ブロック
                LightIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 270);
                //ストーリー
                TxtStory_Start.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 550);
                TxtStory_Mid.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 550);
                TxtStory_Arrow.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 550);
                BtnStory_Close.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -405);
                //ATT推奨画像
                BtnATT.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 600);
                //課金パネル
                TxtPurchase.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 140);
                BtnPur_Purchase.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120);
                BtnPur_Restore.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -235);
                BtnPur_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -370);

            }
        }
    }

    /// <summary>
    /// 言語に合わせてUI画像を変動させる
    /// (LocallizeManagerのStart()関数からの呼び出し)
    /// </summary>
    public void ChangeUILang()
    {
        string Lang1; //4カ国対応画像
        string Lang2 = "_other"; //日本語とそれ以外(英語)

        if (LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
        {
            Lang1 = "_ja";
            Lang2 = "_ja";
        }
        else if (LocalizeManager.Instance.Lang == SystemLanguage.Chinese || LocalizeManager.Instance.Lang == SystemLanguage.ChineseSimplified)
            Lang1 = "_ch";
        //else if (LocalizeManager.Instance.Lang == SystemLanguage.Spanish)
        //    Lang1 = "_sp";
        else if (LocalizeManager.Instance.Lang == SystemLanguage.Korean)
            Lang1 = "_ko";
        else
            Lang1 = "_en";

        string path = "02_UI/";
        //タイトル画面
        Title.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Title" + Lang1);
        BtnTitle_Start.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Start" + Lang2);
        BtnTitle_Continue.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Continue" + Lang2);
        //スタートチェック画面
        Title2.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Title" + Lang1);
        CheckTxt.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "StartCheck" + Lang1);
        //Menu画面
        BtnMenu_Hint.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Hint" + Lang2);
        BtnMenu_Sound.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Sound" + Lang2);
        BtnMenu_Title.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "toTitle" + Lang2);
        BtnMenu_Back.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Back" + Lang2);
        //ヒント画面
        BtnHint_Back.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Back" + Lang2);
        BtnHint_Back2.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Back2" + Lang2);
        BtnHint_ToPur.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "toPurchase" + Lang1);
        //課金画面
        TxtPurchase.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "TxtPurchase" + Lang1);
        BtnPur_Purchase.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Purchase" + Lang1);
        BtnPur_Restore.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Restore" + Lang1);
        BtnPur_Back.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Back" + Lang2);
        //サウンド画面
        ImgSound_BGM.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "BGM" + Lang2);
        ImgSound_SE.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "SE" + Lang2);
        BtnSound_Back.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Back" + Lang2);
        //クリア画面
        ImageClear.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Clear" + Lang1);
        BtnClear_OtherApp.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "Other" + Lang2);
        BtnClear_Title.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "toTitle" + Lang2);
        //ストーリー画面
        TxtStory_Start.GetComponent<Image>().sprite = Resources.Load<Sprite>("05_Story/" + "Start" + Lang1);
        TxtStory_Mid.GetComponent<Image>().sprite = Resources.Load<Sprite>("05_Story/" + "Mid" + Lang1);
        TxtStory_Arrow.GetComponent<Image>().sprite = Resources.Load<Sprite>("05_Story/" + "Arrow" + Lang1);
        //ATT画面
        BtnATT.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "ATT" + Lang1);
    }
}
