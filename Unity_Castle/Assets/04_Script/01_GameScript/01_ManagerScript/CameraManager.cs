using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }


    //タイトル画面後のスタート位置
    public string StartPosition;

    //画面サイズ
    private float width = Screen.width;
    private float height = Screen.height;

    //<summary>現在のカメラの位置名</summary>
    public string CurrentPositionName;
    //<summary>1つ前のカメラの位置名</summary>
    public string PrevPositionName { get; private set; }

    private bool isStart = false;

    //矢印ボタンオブジェクト
    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    public GameObject ButtonBack;

    //<summary>
    //カメラの位置情報クラス
    //</summary>
    private class CameraPositionInfo
    {
        //<summary>カメラの位置</summary>
        public Vector3 Position { get; set; }
        //<summary>カメラの角度</summary>
        public Vector3 Rotate { get; set; }
        //<summary>ボタンの移動先</summary>
        public MoveNames MoveNames { get; set; }
        //<summary>カメラの有効視野(スマホ用)</summary>
        public int[] FOV { get; set; }
        //<summary>カメラの有効視野(iPad用)</summary>
        public int[] iPad_FOV { get; set; }
    }

    //<summary>
    //ボタンの移動先クラス
    //</summary>
    private class MoveNames
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public string Back { get; set; }
    }

    /// <summary>
    /// ガイド矢印
    /// </summary>
    public TapCameraMove[] Arrows;
    public TapFadeCameraMove[] ArrowsFade;



    //オブジェクト(リセット用)
    public UIManager UI;

    //public CurtainBtn_Tap[] CurtainBtnArray;
    //public CurtainBtn_Judge CurtainBtn;



    //<summary>
    //全カメラ位置情報
    //</summary>
    private Dictionary<string, CameraPositionInfo> CameraPositionInfoes = new Dictionary<string, CameraPositionInfo>
    {
        {
            "Title",//タイトル画面
            new CameraPositionInfo
            {
               Position=new Vector3(54.166f,1.842168f,-37.41803f),
                Rotate =new Vector3(8,257,0),
                MoveNames=new MoveNames
                {
                },
                //FOV = new int[]{80},
                //iPad_FOV = new int[]{80},
            }
        },
        {
            "End",//エンディング画面
            new CameraPositionInfo
            {
                Position=new Vector3(50.761f,0.988f,-34.989f),
                Rotate =new Vector3(14,225,0),
                MoveNames=new MoveNames
                {
                },
            }
        },

        //****************************************************************************************
        //最初の部屋
        //****************************************************************************************
        {
            "RoomStart",//
            new CameraPositionInfo
            {
               Position=new Vector3(151.632f,10.5f,108.212f),
                Rotate =new Vector3(5,125,0),
                MoveNames=new MoveNames
                {
                    Right = "Room",
                    Left = "Room"
                },
            }
        },
        {
            "Room2",//
            new CameraPositionInfo
            {
               Position=new Vector3(151.421f,10.5f,109.366f),
                Rotate =new Vector3(5,-47,0),
                MoveNames=new MoveNames
                {
                    Right = "Room",
                    Left = "Room"
                },
            }
        },
        {
            "Room3",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.043f,10.5f,109.455f),
                Rotate =new Vector3(5,60,0),
                MoveNames=new MoveNames
                {
                    Right = "Room",
                    Left = "Room"
                },
            }
        },
        {
            "Room4",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.552f,10.5f,109.159f),
                Rotate =new Vector3(5,135,0),
                MoveNames=new MoveNames
                {
                    Right = "Room",
                    Left = "Room"
                },
            }
        },
        //====RoomStart派生==============
    };

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ChangeCameraPosition("Title");


        //左矢印ボタン押下時
        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Left);
        });

        //右矢印ボタン押下時
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");
            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Right);
        });

        //下矢印ボタン押下時
        ButtonBack.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.SoundSE("TapUIBtn");

            //ボタン状態のリセット
            if (CurrentPositionName == "CurtainBtn")
                ResetBtn();
            else if (CurrentPositionName == "Sankaku")
                ResetBtn();

            ChangeCameraPosition(CameraPositionInfoes[CurrentPositionName].MoveNames.Back);

        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    //<summary>
    //カメラ移動
    //</summary>
    //<param>位置名</param>
    public void ChangeCameraPosition(string positionName)
    {
        if (isStart)
        {
            //アイテム拡大画面表示時
            if (ItemManager.Instance.ItemPanel.activeSelf)
            {
                ItemManager.Instance.ItemPanel.SetActive(false);
                //ボタン表示・非表示
                UpdateButtonActive();
                return;
            }
        }
        isStart = true;

        if (positionName == null) return;

        //BackがAnyの場合,元の位置名を記録
        if (positionName == "Any")
            CurrentPositionName = PrevPositionName;
        else
        {
            if (CameraPositionInfoes[positionName].MoveNames.Back == "Any")
                PrevPositionName = CurrentPositionName;

            CurrentPositionName = positionName;
        }


        GetComponent<Camera>().transform.position = CameraPositionInfoes[CurrentPositionName].Position;
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(CameraPositionInfoes[CurrentPositionName].Rotate);



        //ボタン表示・非表示
        UpdateButtonActive();
        //FOV設定
        UpdateFOV();
        //ガイド矢印表示・非表示
        UpdateArrow();
        //矢印の説明文
        //if(CurrentPositionName == "RoomMap" && !SaveLoadSystem.Instance.gameData.isArrow)
        //{
        //    BlockPanel.Instance.ShowBlockNoIcon();
        //    SaveLoadSystem.Instance.gameData.isArrow = true;
        //    SaveLoadSystem.Instance.Save();
        //    StartCoroutine("FadeArrow");
        //}
    }

    //<summary>
    //ボタン表示非表示の切替
    //</summary>
    private void UpdateButtonActive()
    {
        //左ボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Left == null)
            ButtonLeft.SetActive(false);
        else ButtonLeft.SetActive(true);
        //右ボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Right == null)
            ButtonRight.SetActive(false);
        else ButtonRight.SetActive(true);
        //バックボタンの表示非表示を切替
        if (CameraPositionInfoes[CurrentPositionName].MoveNames.Back == null)
            ButtonBack.SetActive(false);
        else ButtonBack.SetActive(true);
    }


    /// <summary>
    /// 有効視野の調整
    /// </summary>
    private void UpdateFOV()
    {
        if (height / width > 1.6f)
        {
            //スマホの場合
            if (CameraPositionInfoes[CurrentPositionName].FOV == null)
                this.gameObject.GetComponent<Camera>().fieldOfView = 60;
            else
                this.gameObject.GetComponent<Camera>().fieldOfView = CameraPositionInfoes[CurrentPositionName].FOV[0];
        }
        else
        {
            //iPadの場合
            if (CameraPositionInfoes[CurrentPositionName].iPad_FOV == null)
                this.gameObject.GetComponent<Camera>().fieldOfView = 50;
            else
                this.gameObject.GetComponent<Camera>().fieldOfView = CameraPositionInfoes[CurrentPositionName].iPad_FOV[0];
        }
    }

    /// <summary>
    /// ガイド矢印の表示・非表示
    /// </summary>
    private void UpdateArrow()
    {
        foreach(var obj in Arrows)
        {
            if(obj.EnableCameraPositionName == CurrentPositionName)
            {
                obj.gameObject.SetActive(true);
            }
            else
                obj.gameObject.SetActive(false);
        }
        foreach (var obj in ArrowsFade)
        {
            if (obj.EnableCameraPositionName == CurrentPositionName)
            {
                obj.gameObject.SetActive(true);
            }
            else
                obj.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// 矢印の説明パネル
    /// </summary>
    //ストーリー画面のフェード表示
    IEnumerator FadeArrow()
    {
        yield return new WaitForSeconds(1.0f);

        Image PanelImg = UI.StoryPanel.GetComponent<Image>();
        Image ArrowImg = UI.TxtStory_Arrow.GetComponent<Image>();
        Image CloseImg = UI.BtnStory_Close.GetComponent<Image>();
        PanelImg.color = new Color(0, 0, 0, 0);
        ArrowImg.color = new Color(1, 1, 1, 0);
        CloseImg.color = new Color(1, 1, 1, 0);
        UI.StoryPanel.SetActive(true);
        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 0.1f;
        // ループ回数（0はエラー）★変更可
        const int loop_count = 10;
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
            if (alpha < 110)
            {
                Color newColor_Panel = PanelImg.color;
                newColor_Panel.a = alpha / 255.0f;
                PanelImg.color = newColor_Panel;
            }
            Color newColor_Txt = ArrowImg.color;
            newColor_Txt.a = alpha / 255.0f;
            ArrowImg.color = newColor_Txt;
            CloseImg.color = newColor_Txt;
        }
        BlockPanel.Instance.HideBlock();
    }
    //***アプリ固有のリセット処理***********************************************************


    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetBtn()
    {
        //    if (SaveLoadSystem.Instance.gameData.isClearCurtain1 &&
        //        SaveLoadSystem.Instance.gameData.isClearCurtain2 )
        //        return;
        //    CurtainBtn.Status = "00000000";

        //    foreach (var obj in CurtainBtnArray)
        //    {
        //        foreach (var btn in obj.Btns)
        //            btn.SetActive(false);
        //        obj.Btns[0].SetActive(true);
        //    }
    }

}