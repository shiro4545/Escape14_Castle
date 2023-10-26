using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    /// <summary>
    /// 開発モード(true:ヒントオープン, false:動画広告)
    /// </summary>
    public bool DebugMode;


    //ゲーム進捗
    private int Progress;
    //現進捗のヒントフラグ (例:"0100")
    private string HintFlg;
    //現進捗で動画を視聴した回数
    private int CountWatch;

    //ヒント親オブジェクト
    public GameObject Hint2;
    public GameObject Hint3;
    public GameObject Hint4;

    //ヒント本文テキスト
    public Text HintTxt1;
    public Text HintTxt2;
    public Text HintTxt3;
    public Text HintTxt4;

    //ヒントテキストオブジェクト
    public GameObject TxtObject1;
    public GameObject TxtObject2;
    public GameObject TxtObject3;
    public GameObject TxtObject4;

    //動画説明&ボタンの親オブジェクト
    public GameObject Ads1;
    public GameObject Ads2;
    public GameObject Ads3;
    public GameObject Ads4;


    //「動画視聴しますか」ボタン
    public GameObject BtnAds1;
    public GameObject BtnAds2;
    public GameObject BtnAds3;
    public GameObject BtnAds4;

    //「ヒント１~4」見出しテキスト
    public Text[] HintTXTs;
    //「ヒントを見る必要があります」テキスト
    public Text[] BeforeTXTs;
    public GameObject[] BeforeTXTobjs;


    //本編orおまけのヒントデータをセット
    private HintData hintData;

    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        //ヒントJsonファイルの読み込み
        HintLoad.Instance.Load();

        //ヒント1視聴ボタン
        BtnAds1.GetComponent<Button>().onClick.AddListener(() =>
        {
            TapAds();
        });
        //ヒント2視聴ボタン
        BtnAds2.GetComponent<Button>().onClick.AddListener(() =>
        {
            TapAds();
        });
        //ヒント3視聴ボタン
        BtnAds3.GetComponent<Button>().onClick.AddListener(() =>
        {
            TapAds();
        });
        //ヒント4視聴ボタン
        BtnAds4.GetComponent<Button>().onClick.AddListener(() =>
        {
            TapAds();
        });

        //テキスト表示状態を初期化
        ResetHint();
    }

    /// <summary>
    /// 動画視聴ボタン押下時
    /// </summary>
    private void TapAds()
    {
        if (GoogleAds.Instance.isGetMovie && !SaveLoadSystem.Instance.gameData.isPurchase)
            GoogleAds.Instance.ShowReawrd();
        else
        {
            GoogleAds.Instance.RequestReward();
            AfterWatch();
            ChangeImage();
        }
    }


    //<summary>
    //ヒントをヒントテキストにセットし、動画視聴数に応じてテキスト表示・非表示する
    //</summary>
    public void SetHint()
    {
        //ゲーム進捗を取得
        Progress = CheckProgress();

        //現進捗のヒントフラグを取得
        if (DebugMode)
            HintFlg = "1111";
        else if (!SaveLoadSystem.Instance.gameData.isOmake)
            HintFlg = SaveLoadSystem.Instance.gameData.HintFlgArray1[Progress];
        else
            HintFlg = SaveLoadSystem.Instance.gameData.HintFlgArray2[Progress];

        //本編orおまけのヒントデータをセット
        if (!SaveLoadSystem.Instance.gameData.isOmake)
            hintData = HintLoad.Instance.hintData1;
        else
            hintData = HintLoad.Instance.hintData2;

        //ヒント1,2,3,4にテキストをセットする
        if(LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
        {
            HintTxt1.text = Repl(hintData.ja.hint1[Progress]);
            HintTxt2.text = Repl(hintData.ja.hint2[Progress]);
            HintTxt3.text = Repl(hintData.ja.hint3[Progress]);
            HintTxt4.text = Repl(hintData.ja.hint4[Progress]);
        }
        else if (LocalizeManager.Instance.Lang == SystemLanguage.Chinese || LocalizeManager.Instance.Lang == SystemLanguage.ChineseSimplified)
        {
            HintTxt1.text = Repl(hintData.zh_CN.hint1[Progress]);
            HintTxt2.text = Repl(hintData.zh_CN.hint2[Progress]);
            HintTxt3.text = Repl(hintData.zh_CN.hint3[Progress]);
            HintTxt4.text = Repl(hintData.zh_CN.hint4[Progress]);
        }
        //else if (LocalizeManager.Instance.Lang == SystemLanguage.Spanish)
        //{
        //    HintTxt1.text = Repl(hintData.es.hint1[Progress]);
        //    HintTxt2.text = Repl(hintData.es.hint2[Progress]);
        //    HintTxt3.text = Repl(hintData.es.hint3[Progress]);
        //    HintTxt4.text = Repl(hintData.es.hint4[Progress]);
        //}
        else if (LocalizeManager.Instance.Lang == SystemLanguage.Korean)
        {
            HintTxt1.text = Repl(hintData.ko.hint1[Progress]);
            HintTxt2.text = Repl(hintData.ko.hint2[Progress]);
            HintTxt3.text = Repl(hintData.ko.hint3[Progress]);
            HintTxt4.text = Repl(hintData.ko.hint4[Progress]);
        }
        else
        {
            HintTxt1.text = Repl(hintData.en.hint1[Progress]);
            HintTxt2.text = Repl(hintData.en.hint2[Progress]);
            HintTxt3.text = Repl(hintData.en.hint3[Progress]);
            HintTxt4.text = Repl(hintData.en.hint4[Progress]);
        }

        //動画視聴した数を取得
        CountWatch = 4;
        for (int i = 0; i < 4; i++)
        {
            if (HintFlg.Substring(i, 1) == "0")
            {
                CountWatch = i;
                break;
            }
        }

        //動画取得状況に応じて表示切替
        ChangeImage();

        //動画視聴数に合わせてオブジェクトを表示・非表示
        ShowHint();
    }

    /// <summary>
    /// 動画取得状況に応じて表示切替
    /// </summary>
    private void ChangeImage()
    {
        //「ヒント１〜４」見出し
        for (int i = 0; i < HintTXTs.Length; i++)
        {
            if (LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
                HintTXTs[i].text = "ヒント" + (i + 1);
            else
                HintTXTs[i].text = "Hint" + (i + 1);
        }


        string ImageName;

        if (GoogleAds.Instance.isGetMovie && !SaveLoadSystem.Instance.gameData.isPurchase)
        //取得成功時かつ未課金
        {
            //「動画広告を見る必要がある」オブジェ
            foreach (var obj in BeforeTXTobjs)
                    obj.SetActive(true);

            //「動画広告を見る必要がある」テキスト
            foreach (var txt in BeforeTXTs)
            {
                if (LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
                    txt.text = "ヒントを見るには動画広告を視聴する必要があります";
                else if (LocalizeManager.Instance.Lang == SystemLanguage.Chinese || LocalizeManager.Instance.Lang == SystemLanguage.ChineseSimplified)
                    txt.text = "您必须观看广告视频才能看到提示.";
                //else if (LocalizeManager.Instance.Lang == SystemLanguage.Spanish)
                //    txt.text = "Debes ver el video del anuncio para ver el consejo.";
                else if (LocalizeManager.Instance.Lang == SystemLanguage.Korean)
                    txt.text = "팁을 보려면 광고 동영상을 시청해야 합니다.";
                else
                    txt.text = "You must watch the ad video to see the tip";
            }


            //「広告動画を視聴する」ボタン
            if (LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
                ImageName = "02_UI/watch_ja";
            else if (LocalizeManager.Instance.Lang == SystemLanguage.Chinese || LocalizeManager.Instance.Lang == SystemLanguage.ChineseSimplified)
                ImageName = "02_UI/watch_ch";
            //else if (LocalizeManager.Instance.Lang == SystemLanguage.Spanish)
            //    ImageName = "02_UI/watch_sp";
            else if (LocalizeManager.Instance.Lang == SystemLanguage.Korean)
                ImageName = "02_UI/watch_ko";
            else
                ImageName = "02_UI/watch_en";
        }
        else
        //取得失敗時 or　課金済み
        {
            //「ヒントを見る必要がある」オブジェ
            foreach (var obj in BeforeTXTobjs)
                obj.SetActive(false);

            //「広告動画を視聴する」ボタン
            if (LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
                ImageName = "02_UI/HintBtn2_ja";
            else
                ImageName = "02_UI/HintBtn2_other";
        }

        //「広告動画を視聴する」ボタン
        BtnAds1.GetComponent<Image>().sprite = Resources.Load<Sprite>(ImageName);
        BtnAds2.GetComponent<Image>().sprite = Resources.Load<Sprite>(ImageName);
        BtnAds3.GetComponent<Image>().sprite = Resources.Load<Sprite>(ImageName);
        BtnAds4.GetComponent<Image>().sprite = Resources.Load<Sprite>(ImageName);
    }


    //<summary>
    //動画視聴後のヒント表示・非表示
    //</summary>
    public void AfterWatch()
    {
        CountWatch++;
        ShowHint();

        //セーブデータのフラグ更新
        string NewHintFlg = "0000";

        if (CountWatch == 1)
            NewHintFlg = "1000";
        else if (CountWatch == 2)
            NewHintFlg = "1100";
        else if (CountWatch == 3)
            NewHintFlg = "1110";
        else if (CountWatch == 4)
            NewHintFlg = "1111";

        if(!SaveLoadSystem.Instance.gameData.isOmake)
            SaveLoadSystem.Instance.gameData.HintFlgArray1[Progress] = NewHintFlg;
        else
            SaveLoadSystem.Instance.gameData.HintFlgArray2[Progress] = NewHintFlg;

        SaveLoadSystem.Instance.Save();
    }



    //<summary>
    //ヒントをヒントテキストにセットし、動画視聴数に応じてテキスト表示・非表示する
    //</summary>
    public void ShowHint()
    {
        //ヒント1の動画を視聴済みの場合
        if (CountWatch >= 1)
        {
            Ads1.SetActive(false);
            TxtObject1.SetActive(true);

            //ヒント2まである場合
            if (hintData.ja.hint2[Progress].Length > 5) //5文字以上
                Hint2.SetActive(true);
        }
        //ヒント2の動画を視聴済みの場合
        if (CountWatch >= 2)
        {
            Ads2.SetActive(false);
            TxtObject2.SetActive(true);

            //ヒント3まである場合
            if (hintData.ja.hint3[Progress].Length > 5) //5文字以上
                Hint3.SetActive(true);
        }
        //ヒント3の動画を視聴済みの場合
        if (CountWatch >= 3)
        {
            Ads3.SetActive(false);
            TxtObject3.SetActive(true);

            //ヒント4まである場合
            if (hintData.ja.hint4[Progress].Length > 5) //5文字以上
                Hint4.SetActive(true);
        }
        //ヒント4の動画を視聴済みの場合
        if (CountWatch >= 4)
        {
            Ads4.SetActive(false);
            TxtObject4.SetActive(true);
        }
    }


    //<summary>
    //ヒント画面を閉じるときにヒントを初期状態にリセットする
    //</summary>
    public void ResetHint()
    {
        Ads1.SetActive(true);
        Ads2.SetActive(true);
        Ads3.SetActive(true);
        Ads4.SetActive(true);

        TxtObject1.SetActive(false);
        TxtObject2.SetActive(false);
        TxtObject3.SetActive(false);
        TxtObject4.SetActive(false);

        Hint2.SetActive(false);
        Hint3.SetActive(false);
        Hint4.SetActive(false);
    }

    private string Repl(string str)
    {
        return str.Replace("##", "\"");
    }


    //***************************************************************************
    //<summary>
    //ゲーム進捗の算出
    //<summary>
    public int CheckProgress()
    {
        //インスタンスを代入(ソース短縮化のため)
        gameData = SaveLoadSystem.Instance.gameData;

        int progress = 1;

        //本編の進捗
        if (!gameData.isOmake)
        {

            //進捗算出
            if (!gameData.isClearTea)
                progress = 1;
            //2
            else if (!gameData.isClearTansu)
                progress = 3;
            //4
            else if (!gameData.isClearRousoku)
                progress = 5;
            else if (!gameData.isClearTatami)
                progress = 6;
            else if (!gameData.isClearKoban)
                progress = 7;
            //8
            else if (!gameData.isClearWallBtn)
                progress = 9;
            //10
            else if (!gameData.isClearSensuBtn)
                progress = 11;
            else if (!gameData.isClearDoor1)
                progress = 12;
            else if (!gameData.isClearDoor2)
                progress = 13;
            else if (!gameData.isClearPaper)
                progress = 14;
            else if (!gameData.isClearFire)
                progress = 15;
            else if (!gameData.isClearMark)
                progress = 16;
            //17
            //18
            else if (!gameData.isClearKatana)
                progress = 19;
            else if (!gameData.isClearTake)
                progress = 20;
            //else if (!gameData.isClearMakimono1)
            //    progress = 21;
            else if (!gameData.isClearManji)
                progress = 22;
            else if (!gameData.isClearBuki)
                progress = 23;
            else if (!gameData.isClearKasa)
                progress = 24;
            else if (!gameData.isClearAnimal)
                progress = 25;
            else if (!gameData.isClearSyuriken)
                progress = 26;
            else if (!gameData.isClearChain)
                progress = 27;
            else if (!gameData.isClearWindow2 || !gameData.isClearWindow3 || !gameData.isClearWindow4)
                progress = 28;
            else if (!gameData.isClearWindowBtn)
                progress = 29;
            else if (!gameData.isClearView)
                progress = 30;
            else if (!gameData.isClearArrow)
                progress = 31;
            else if (!gameData.isClearMato)
                progress = 32;
            else if (!gameData.isClearMatoBtn)
                progress = 33;
            else if (!gameData.isClearOke)
                progress = 34;
            else if (!gameData.isClearNotFire)
                progress = 35;
            else if (!gameData.isClearDoll2 || !gameData.isClearDoll3 || !gameData.isClearDoll4)
                progress = 36;
            else if (!gameData.isClearMap)
                progress = 37;
            else if (!gameData.isClearKuwa)
                progress = 38;
            //39
            else if (!gameData.isClearMakimono2)
                progress = 40;
            //41
            else if (!gameData.isGetKey3)
                progress = 42;
            else
                progress = 43;

            return progress;
        }

        //おまけの進捗
        for(int i = 0; i < gameData.OmakeStatus.Length; i++)
        {
            if(gameData.OmakeStatus.Substring(i,1) == "0")
            {
                progress = i;
                break;
            }
        }
        return progress;
    }


}
