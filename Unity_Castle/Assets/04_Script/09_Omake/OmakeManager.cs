using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OmakeManager : MonoBehaviour
{
    public static OmakeManager Instance { get; private set; }

    public GameObject OmakeUI;
    public Text Txt_Count;

    public GameObject CameraCollider;

    public GameObject[] OmakeDolls;

    //本編オブジェクトの非表示
    public GameObject[] HideObjects;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }


    /// <summary>
    //おまけスタート時
    /// </summary>
    public void StartOmake()
    {
        //UI切替
        OmakeUI.SetActive(true);
        Txt_Count.text = SaveLoadSystem.Instance.gameData.OmakeCnt.ToString();

        //カメラコライダーOn
        CameraCollider.SetActive(true);

        //人形
        for(int i = 1; i <= OmakeDolls.Length; i++)
        {
            string status = SaveLoadSystem.Instance.gameData.OmakeStatus.Substring(i, 1);
            if(status == "0")
                OmakeDolls[i - 1].SetActive(true);
        }

        //本編オブジェクトOFF
        foreach (var obj in HideObjects)
            obj.SetActive(false);
    }


    /// <summary>
    //おまけリセット時
    /// </summary>
    public void ResetOmake()
    {
        //UI切替
        OmakeUI.SetActive(false);

        //カメラコライダーOff
        CameraCollider.SetActive(false);

        //人形
        foreach (var obj in OmakeDolls)
            obj.SetActive(false);

        foreach (var obj in HideObjects)
            obj.SetActive(true);
    }



    /// <summary>
    //人形ゲット時
    /// </summary>
    /// <param name="Idx"></param>
    public void GetDoll(int Idx)
    {
        //ステータス更新
        string status = SaveLoadSystem.Instance.gameData.OmakeStatus;
        status = status.Substring(0, Idx) + "1" + status.Substring(Idx + 1);
        SaveLoadSystem.Instance.gameData.OmakeStatus = status;

        //カウント表示
        SaveLoadSystem.Instance.gameData.OmakeCnt++;
        SaveLoadSystem.Instance.Save();

        Txt_Count.text = SaveLoadSystem.Instance.gameData.OmakeCnt.ToString();

        //動画表示
        if(SaveLoadSystem.Instance.gameData.OmakeCnt == 8 || SaveLoadSystem.Instance.gameData.OmakeCnt == 15)
            Invoke(nameof(ShowAd), 0.5f);

        //答え合わせ
        if (SaveLoadSystem.Instance.gameData.OmakeCnt != 20)
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        //ステータスリセット
        SaveLoadSystem.Instance.gameData.OmakeCnt = 0;
        SaveLoadSystem.Instance.gameData.OmakeStatus = "s00000000000000000000";
        SaveLoadSystem.Instance.Save();

        Invoke(nameof(After1), 1.5f);
    }

    private void After1()
    {
        ClearManager.Instance.EscapeOmake();
        BlockPanel.Instance.HideBlock();
    }

    private void ShowAd()
    {
        GoogleAds.Instance.ShowInterstitialAd2();
    }
}
