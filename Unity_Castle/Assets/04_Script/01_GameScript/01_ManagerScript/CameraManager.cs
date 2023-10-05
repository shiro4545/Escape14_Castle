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

    public Koban_Judge Koban;
    public Koban_Tap[] KobanAr;

    public WallBtn_Judge WallBtn;
    public WallBtn_Tap[] WallBtnAr;

    public SensuBtn_Judge SensuBtn;
    public SensuBtn_Tap[] SensuBtnAr;

    public Paper_Judge Paper;
    public Paper_Tap[] PaperAr;

    public Buki_Judge Buki;

    public Animal_Judge Animal;
    public Animal_Tap[] AnimalAr;

    public Syuri_Judge Syuri;

    public Window3_Tap Win3;

    public View_Judge View;
    public View_Tap[] ViewAr;

    public WinBtn_Judge WinBtn;
    public WinBtn_Tap[] WinBtnAr;

    public MatoBtn_Judge MatoBtn;

    public Map_Judge Map;

    public Doukutsu_Judge Doukutsu;

    //<summary>
    //全カメラ位置情報
    //</summary>
    private Dictionary<string, CameraPositionInfo> CameraPositionInfoes = new Dictionary<string, CameraPositionInfo>
    {
        {
            "Title",//タイトル画面
            new CameraPositionInfo
            {
               Position=new Vector3(225.88f,9.33f,124.52f),
                Rotate =new Vector3(-10,-48,0),
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
               Position=new Vector3(152.463f,10.5f,108.895f),
                Rotate =new Vector3(5,226,0),
                MoveNames=new MoveNames
                {
                    Right = "RoomTansu",
                    Left = "RoomSensu"
                },
            }
        },
        {
            "RoomTansu",//
            new CameraPositionInfo
            {
               Position=new Vector3(152.831f,10.5f,107.996f),
                Rotate =new Vector3(5,-46,0),
                MoveNames=new MoveNames
                {
                    Right = "RoomDoor",
                    Left = "RoomStart"
                },
            }
        },
        {
            "RoomDoor",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.009f,10.5f,105.385f),
                Rotate =new Vector3(5,49,0),
                MoveNames=new MoveNames
                {
                    Right = "RoomSensu",
                    Left = "RoomTansu"
                },
            }
        },
        {
            "RoomSensu",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.971f,10.5f,109.06f),
                Rotate =new Vector3(5,134,0),
                MoveNames=new MoveNames
                {
                    Right = "RoomStart",
                    Left = "RoomDoor"
                },
            }
        },
        //====RoomStart派生==============
        {
            "WallArea",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.336f,9.845f,104.162f),
                Rotate =new Vector3(14,240,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomStart"
                },
            }
        },
        {
            "WallBtn",//
            new CameraPositionInfo
            {
               Position=new Vector3(146.495f,11.086f,101.38f),
                Rotate =new Vector3(5,180,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomStart"
                },
            }
        },
        {
            "Koban",//
            new CameraPositionInfo
            {
               Position=new Vector3(143.6234f,9.9389f,102.6807f),
                Rotate =new Vector3(15,270,0),
                MoveNames=new MoveNames
                {
                    Back = "WallArea"
                },
            }
        },
        {
            "Wall",//
            new CameraPositionInfo
            {
               Position=new Vector3(143.906f,9.164f,103.33f),
                Rotate =new Vector3(26,239,0),
                MoveNames=new MoveNames
                {
                    Back = "WallArea"
                },
            }
        },
        //====RoomTansu派生==============
        {
            "Rousoku1",//
            new CameraPositionInfo
            {
               Position=new Vector3(144.535f,10.098f,116.204f),
                Rotate =new Vector3(0,-41,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomTansu"
                },
            }
        },
        {
            "Tea",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.36f,9.86f,113.58f),
                Rotate =new Vector3(59,0,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomTansu"
                },
            }
        },
        {
            "TansuBtn",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.356f,9.092879f,114.027f),
                Rotate =new Vector3(65,0,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomTansu"
                },
            }
        },
        {
            "Tansu",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.96f,9.53f,113.48f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomTansu"
                },
            }
        },
        {
            "Tansu_0",//
            new CameraPositionInfo
            {
               Position=new Vector3(146.544f,9.896f,116.312f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        {
            "Tansu_1",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.384f,9.896f,116.312f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        {
            "Tansu_2",//
            new CameraPositionInfo
            {
               Position=new Vector3(146.542f,9.302f,116.312f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        {
            "Tansu_3",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.756f,9.302f,116.312f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        {
            "Tansu_4",//
            new CameraPositionInfo
            {
               Position=new Vector3(146.55f,8.7f,116.312f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        {
            "Tansu_5",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.38f,8.7f,116.312f),
                Rotate =new Vector3(3,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        {
            "TansuOpen",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.6263f,9.6061f,116.2278f),
                Rotate =new Vector3(48,-27,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu"
                },
            }
        },
        //====RoomDoor派生==============
        {
            "Shelf",//
            new CameraPositionInfo
            {
               Position=new Vector3(154.698f,10.048f,115.085f),
                Rotate =new Vector3(35,37,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomDoor"
                },
            }
        },
        {
            "TeaHint",//
            new CameraPositionInfo
            {
               Position=new Vector3(155.659f,9.505f,116.559f),
                Rotate =new Vector3(70,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Shelf"
                },
            }
        },
        {
            "Tatami",//
            new CameraPositionInfo
            {
               Position=new Vector3(156.191f,8.875f,115.863f),
                Rotate =new Vector3(16,0,0),
                MoveNames=new MoveNames
                {
                    Back = "Shelf"
                },
            }
        },
        {
            "ShelfOpen",//
            new CameraPositionInfo
            {
               Position=new Vector3(155.972f,8.905f,116.182f),
                Rotate =new Vector3(60,39,0),
                MoveNames=new MoveNames
                {
                    Back = "Shelf"
                },
            }
        },
        {
            "Rousoku0",//
            new CameraPositionInfo
            {
               Position=new Vector3(158.468f,9.856f,115.778f),
                Rotate =new Vector3(0,67,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomDoor"
                },
            }
        },
        {
            "Door1",//
            new CameraPositionInfo
            {
               Position=new Vector3(155.544f,10.107f,110.833f),
                Rotate =new Vector3(5,69,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomDoor"
                },
            }
        },
        {
            "Key1",//
            new CameraPositionInfo
            {
               Position=new Vector3(159.645f,9.391f,112.321f),
                Rotate =new Vector3(15,57,0),
                MoveNames=new MoveNames
                {
                    Back = "Door1"
                },
            }
        },
        {
            "Paper",//
            new CameraPositionInfo
            {
               Position=new Vector3(151.843f,9.713f,110.314f),
                Rotate =new Vector3(51,33,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomDoor"
                },
            }
        },
        {
            "Rousoku2",//
            new CameraPositionInfo
            {
               Position=new Vector3(152.3063f,9.119604f,110.578f),
                Rotate =new Vector3(29,37,0),
                MoveNames=new MoveNames
                {
                    Back = "Paper"
                },
            }
        },
        //====RoomSensu派生==============
        {
            "Sensu",//
            new CameraPositionInfo
            {
               Position=new Vector3(154.926f,10.191f,102.589f),
                Rotate =new Vector3(10,146.1f,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomSensu"
                },
            }
        },
        {
            "Rousoku3",//
            new CameraPositionInfo
            {
               Position=new Vector3(158.1458f,10.8527f,100.9371f),
                Rotate =new Vector3(5,143,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomSensu"
                },
            }
        },
        {
            "SensuBtn",//
            new CameraPositionInfo
            {
               Position=new Vector3(159.0111f,11.12f,101.308f),
                Rotate =new Vector3(10,90,0),
                MoveNames=new MoveNames
                {
                    Back = "RoomSensu"
                },
            }
        },
        
        //****************************************************************************************
        //外
        //****************************************************************************************
        {
            "OutKajiya",//
            new CameraPositionInfo
            {
               Position=new Vector3(193.61f,12f,138.67f),
                Rotate =new Vector3(5,147,0),
                MoveNames=new MoveNames
                {
                    Right = "OutYashiki",
                    Left = "OutGate"
                },
            }
        },
        {
            "OutYashiki",//
            new CameraPositionInfo
            {
               Position=new Vector3(203.98f,12f,145.92f),
                Rotate =new Vector3(5,-127,0),
                MoveNames=new MoveNames
                {
                    Right = "OutShiro",
                    Left = "OutKajiya"
                },
            }
        },
        {
            "OutShiro",//
            new CameraPositionInfo
            {
               Position=new Vector3(202.9f,12f,116.4f),
                Rotate =new Vector3(-8,-42,0),
                MoveNames=new MoveNames
                {
                    Right = "OutIke",
                    Left = "OutYashiki"
                },
            }
        },
        {
            "OutIke",//
            new CameraPositionInfo
            {
               Position=new Vector3(183.935f,12f,153.862f),
                Rotate =new Vector3(5,41,0),
                MoveNames=new MoveNames
                {
                    Right = "OutGate",
                    Left = "OutShiro"
                },
            }
        },
        {
            "OutGate",//
            new CameraPositionInfo
            {
               Position=new Vector3(178.233f,12f,159.496f),
                Rotate =new Vector3(5,111,0),
                MoveNames=new MoveNames
                {
                    Right = "OutKajiya",
                    Left = "OutIke"
                },
            }
        },
        {
            "Map",//
            new CameraPositionInfo
            {
               Position=new Vector3(182.106f,10.893f,133.738f),
                Rotate =new Vector3(3,-90,0),
                MoveNames=new MoveNames
                {
                    Back = "Any"
                },
            }
        },
        //**********************************************
        //鍛冶屋
        //**********************************************
        {
            "Door2",//
            new CameraPositionInfo
            {
               Position=new Vector3(205.413f,10.106f,106.929f),
                Rotate =new Vector3(5,153,0),
                MoveNames=new MoveNames
                {
                    Back = "OutKajiya"
                },
            }
        },
        {
            "Door2Btn",//
            new CameraPositionInfo
            {
               Position=new Vector3(208.606f,9.525f,101.015f),
                Rotate =new Vector3(15,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Door2"
                },
            }
        },
        {
            "Kajiya1",//
            new CameraPositionInfo
            {
               Position=new Vector3(208.402f,9.525f,100.767f),
                Rotate =new Vector3(6,145,0),
                MoveNames=new MoveNames
                {
                    Right = "Kajiya2",
                    Back = "OutKajiya"
                },
            }
        },
        {
            "Kajiya2",//
            new CameraPositionInfo
            {
               Position=new Vector3(211.424f,9.525f,98.382f),
                Rotate =new Vector3(6,221.5f,0),
                MoveNames=new MoveNames
                {
                    Left = "Kajiya1",
                    Back = "OutKajiya"
                },
            }
        },
        
        //====Kajiya1派生==============
        {
            "Kamado",//
            new CameraPositionInfo
            {
               Position=new Vector3(211.915f,9.131083f,95.314f),
                Rotate =new Vector3(30,103,0),
                MoveNames=new MoveNames
                {
                    Back = "Kajiya1"
                },
            }
        },
        {
            "Kaji",//
            new CameraPositionInfo
            {
               Position=new Vector3(211.903f,9.408f,93.705f),
                Rotate =new Vector3(50,82,0),
                MoveNames=new MoveNames
                {
                    Back = "Kajiya1"
                },
            }
        },
        {
            "Katana",//
            new CameraPositionInfo
            {
               Position=new Vector3(212.401f,9.852f,90.246f),
                Rotate =new Vector3(40,142,0),
                MoveNames=new MoveNames
                {
                    Back = "Kajiya1"
                },
            }
        },
        //====Kajiya2派生==============
        {
            "Mikazuki",//
            new CameraPositionInfo
            {
               Position=new Vector3(206.74f,9.744557f,92.888f),
                Rotate =new Vector3(5,244,0),
                MoveNames=new MoveNames
                {
                    Back = "Kajiya2"
                },
            }
        },
        {
            "Tansu2",//
            new CameraPositionInfo
            {
               Position=new Vector3(206.64f,10.049f,91.091f),
                Rotate =new Vector3(30,211,0),
                MoveNames=new MoveNames
                {
                    Back = "Kajiya2"
                },
            }
        },
        {
            "ArrowBtn",//
            new CameraPositionInfo
            {
               Position=new Vector3(205.543f,9.878f,88.853f),
                Rotate =new Vector3(70,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu2"
                },
            }
        },
        {
            "Tansu2Open1",//
            new CameraPositionInfo
            {
               Position=new Vector3(206.225f,10.043f,89.552f),
                Rotate =new Vector3(50,225,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu2"
                },
            }
        },
        {
            "WindowBtn",//
            new CameraPositionInfo
            {
               Position=new Vector3(205.613f,8.542f,89.345f),
                Rotate =new Vector3(15,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu2"
                },
            }
        },
        {
            "Tansu2Open2",//
            new CameraPositionInfo
            {
               Position=new Vector3(206.122f,9.156f,89.555f),
                Rotate =new Vector3(50,220,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu2"
                },
            }
        },
        {
            "Doll2",//
            new CameraPositionInfo
            {
               Position=new Vector3(204.156f,8.678f,89.138f),
                Rotate =new Vector3(15,195,0),
                MoveNames=new MoveNames
                {
                    Back = "Tansu2"
                },
            }
        },
        //====弓道派生==============
        {
            "Kyudo",//
            new CameraPositionInfo
            {
               Position=new Vector3(206.854f,9.784f,115.84f),
                Rotate =new Vector3(10,95,0),
                MoveNames=new MoveNames
                {
                    Back = "OutKajiya"
                },
            }
        },
        {
            "Mato",//
            new CameraPositionInfo
            {
               Position=new Vector3(220.761f,9.447f,115.215f),
                Rotate =new Vector3(10,96,0),
                MoveNames=new MoveNames
                {
                    Back = "Kyudo"
                },
            }
        },
        //====洞窟派生==============
        {
            "Hashigo",//
            new CameraPositionInfo
            {
               Position=new Vector3(213.6512f,8.0681f,94.9328f),
                Rotate =new Vector3(35,90,0),
                MoveNames=new MoveNames
                {
                },
            }
        },
        {
            "Doukutsu1",//
            new CameraPositionInfo
            {
               Position=new Vector3(182.499f,10.042f,62.809f),
                Rotate =new Vector3(6,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Kamado"
                },
            }
        },
        {
            "Doukutsu2",//
            new CameraPositionInfo
            {
               Position=new Vector3(189.369f,10.042f,62.809f),
                Rotate =new Vector3(6,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Kamado"
                },
            }
        },
        {
            "Doukutsu3",//
            new CameraPositionInfo
            {
               Position=new Vector3(189.28f,10.042f,61.418f),
                Rotate =new Vector3(6,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Kamado"
                },
            }
        },
        {
            "DoukutsuTable",//
            new CameraPositionInfo
            {
               Position=new Vector3(189.394f,8.714f,54.446f),
                Rotate =new Vector3(48,180,0),
                MoveNames=new MoveNames
                {
                    Back = "Doukutsu3"
                },
            }
        },
        //**********************************************
        //城
        //**********************************************
        {
            "ShiroGate",//
            new CameraPositionInfo
            {
               Position=new Vector3(162.24f,14.86f,153.03f),
                Rotate =new Vector3(5,-57,0),
                MoveNames=new MoveNames
                {
                    Back = "OutShiro"
                },
            }
        },
        {
            "Hei",//
            new CameraPositionInfo
            {
               Position=new Vector3(153.6f,15.64356f,157.77f),
                Rotate =new Vector3(8,-78,0),
                MoveNames=new MoveNames
                {
                    Back = "ShiroGate"
                },
            }
        },
        {
            "Manji",//
            new CameraPositionInfo
            {
               Position=new Vector3(149.2881f,14.6289f,164.4891f),
                Rotate =new Vector3(15,0,0),
                MoveNames=new MoveNames
                {
                    Back = "ShiroGate"
                },
            }
        },
        {
            "ShiroGate2",//
            new CameraPositionInfo
            {
               Position=new Vector3(150.208f,14.82268f,157.74f),
                Rotate =new Vector3(4,1,0),
                MoveNames=new MoveNames
                {
                },
            }
        },
        {
            "Tree",//
            new CameraPositionInfo
            {
               Position=new Vector3(180.974f,10.37832f,151.723f),
                Rotate =new Vector3(4,-28,0),
                MoveNames=new MoveNames
                {
                    Back = "OutShiro"
                },
            }
        },
        {
            "Sand",//
            new CameraPositionInfo
            {
               Position=new Vector3(178.129f,10f,157.303f),
                Rotate =new Vector3(44,-40,0),
                MoveNames=new MoveNames
                {
                    Back = "Tree"
                },
            }
        },
        //====2階派生==============
        {
            "2F",//
            new CameraPositionInfo
            {
               Position=new Vector3(162.57f,21.433f,189.55f),
                Rotate =new Vector3(5,-124,0),
                MoveNames=new MoveNames
                {
                    Left = "2FL",
                    Right = "2FR"
                },
            }
        },
        {
            "2FL",//
            new CameraPositionInfo
            {
               Position=new Vector3(159.102f,21.433f,188.238f),
                Rotate =new Vector3(5,-206,0),
                MoveNames=new MoveNames
                {
                    Right = "2F"
                },
            }
        },
        {
            "2FR",//
            new CameraPositionInfo
            {
               Position=new Vector3(152.704f,21.433f,184.388f),
                Rotate =new Vector3(5,-70,0),
                MoveNames=new MoveNames
                {
                    Left = "2F",
                },
            }
        },
        {
            "Buki",//
            new CameraPositionInfo
            {
               Position=new Vector3(144.196f,20.953f,182.729f),
                Rotate =new Vector3(10,-123,0),
                MoveNames=new MoveNames
                {
                    Back = "2F"
                },
            }
        },
        {
            "Syuriken2",//
            new CameraPositionInfo
            {
               Position=new Vector3(161.853f,19.8159f,179.777f),
                Rotate =new Vector3(21,-191,0),
                MoveNames=new MoveNames
                {
                    Back = "2FL"
                },
            }
        },
        //====3階派生==============
        {
            "3F",//
            new CameraPositionInfo
            {
               Position=new Vector3(144.517f,29.377f,167.683f),
                Rotate =new Vector3(5,66,0),
                MoveNames=new MoveNames
                {
                    Left = "3FL"
                },
            }
        },
        {
            "3FL",//
            new CameraPositionInfo
            {
               Position=new Vector3(150.288f,29.377f,170.444f),
                Rotate =new Vector3(5,-16,0),
                MoveNames=new MoveNames
                {
                    Right = "3F"
                },
            }
        },
        {
            "Chain",//
            new CameraPositionInfo
            {
               Position=new Vector3(159.363f,28.242f,172.799f),
                Rotate =new Vector3(30,27,0),
                MoveNames=new MoveNames
                {
                    Back = "3F"
                },
            }
        },
        {
            "SyurikenHint",//
            new CameraPositionInfo
            {
               Position=new Vector3(161.75f,27.654f,172.165f),
                Rotate =new Vector3(5,90,0),
                MoveNames=new MoveNames
                {
                    Back = "3F"
                },
            }
        },
        {
            "BukiHint",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.93f,28.783f,180.1305f),
                Rotate =new Vector3(12,0,0),
                MoveNames=new MoveNames
                {
                    Back = "3FL"
                },
            }
        },
        {
            "Syuriken",//
            new CameraPositionInfo
            {
               Position=new Vector3(147.93f,28.086f,179.751f),
                Rotate =new Vector3(33,0,0),
                MoveNames=new MoveNames
                {
                    Back = "3FL"
                },
            }
        },
        {
            "3FTansu",//
            new CameraPositionInfo
            {
               Position=new Vector3(149.68f,28.952f,185.985f),
                Rotate =new Vector3(36,-22,0),
                MoveNames=new MoveNames
                {
                    Back = "3FL"
                },
            }
        },
        {
            "Animal",//
            new CameraPositionInfo
            {
               Position=new Vector3(148.839f,28.627f,187.722f),
                Rotate =new Vector3(68,0,0),
                MoveNames=new MoveNames
                {
                    Back = "3FTansu"
                },
            }
        },
        //====最上階派生==============
        {
            "Top",//
            new CameraPositionInfo
            {
               Position=new Vector3(148.913f,38.04181f,187.747f),
                Rotate =new Vector3(10,135,0),
                MoveNames=new MoveNames
                {
                    Right = "TopR"
                },
            }
        },
        {
            "TopR",//
            new CameraPositionInfo
            {
               Position=new Vector3(153.224f,37.66484f,184.713f),
                Rotate =new Vector3(10,218.5f,0),
                MoveNames=new MoveNames
                {
                    Left = "Top"
                },
            }
        },
        {
            "View",//
            new CameraPositionInfo
            {
               Position=new Vector3(162.072f,38.181f,180.243f),
                Rotate =new Vector3(43,90,0),
                MoveNames=new MoveNames
                {
                    Back = "Top"
                },
            }
        },
        {
            "Moji",//
            new CameraPositionInfo
            {
               Position=new Vector3(148.43f,36.211f,178.729f),
                Rotate =new Vector3(70,180,0),
                MoveNames=new MoveNames
                {
                    Back = "TopR"
                },
            }
        },
        {
            "Doll4",//
            new CameraPositionInfo
            {
               Position=new Vector3(148.989f,35.705f,176.055f),
                Rotate =new Vector3(12,205,0),
                MoveNames=new MoveNames
                {
                    Back = "TopR"
                },
            }
        },
        {
            "TopTansu",//
            new CameraPositionInfo
            {
               Position=new Vector3(146.299f,37.106f,179.128f),
                Rotate =new Vector3(32,216,0),
                MoveNames=new MoveNames
                {
                    Back = "TopR"
                },
            }
        },
        {
            "ViewBtn",//
            new CameraPositionInfo
            {
               Position=new Vector3(145.5851f,37.2037f,177.4517f),
                Rotate =new Vector3(56,270,0),
                MoveNames=new MoveNames
                {
                    Back = "TopTansu"
                },
            }
        },
        //**********************************************
        //池
        //**********************************************
        {
            "Take",//
            new CameraPositionInfo
            {
               Position=new Vector3(196.086f,10.46189f,178.682f),
                Rotate =new Vector3(22,50,0),
                MoveNames=new MoveNames
                {
                    Back = "OutIke"
                },
            }
        },
        {
            "Ike",//
            new CameraPositionInfo
            {
               Position=new Vector3(200.92f,10.88f,168.13f),
                Rotate =new Vector3(55,79,0),
                MoveNames=new MoveNames
                {
                    Back = "OutIke"
                },
            }
        },
        
        //**********************************************
        //門
        //**********************************************
        {
            "Flag1",//
            new CameraPositionInfo
            {
               Position=new Vector3(192.2194f,11.027f,156.8637f),
                Rotate =new Vector3(-1,107,0),
                MoveNames=new MoveNames
                {
                    Back = "OutGate"
                },
            }
        },
        {
            "Flag2",//
            new CameraPositionInfo
            {
               Position=new Vector3(192.019f,10.886f,149.298f),
                Rotate =new Vector3(-1,103,0),
                MoveNames=new MoveNames
                {
                    Back = "OutGate"
                },
            }
        },
        {
            "Bridge1",//
            new CameraPositionInfo
            {
               Position=new Vector3(196.84f,12.478f,153.51f),
                Rotate =new Vector3(10,95,0),
                MoveNames=new MoveNames
                {
                },
            }
        },
        {
            "Bridge2",//
            new CameraPositionInfo
            {
               Position=new Vector3(208.312f,12.146f,152.605f),
                Rotate =new Vector3(28,93,0),
                MoveNames=new MoveNames
                {
                },
            }
        },
        {
            "Gate",//
            new CameraPositionInfo
            {
               Position=new Vector3(222.599f,5.899f,151.091f),
                Rotate =new Vector3(5,81,0),
                MoveNames=new MoveNames
                {
                    Back = "OutGate"
                },
            }
        },
        {
            "MarkBox",//
            new CameraPositionInfo
            {
               Position=new Vector3(230.561f,5.561f,153.488f),
                Rotate =new Vector3(45,60,0),
                MoveNames=new MoveNames
                {
                    Back = "Gate"
                },
            }
        },
        {
            "Mark",//
            new CameraPositionInfo
            {
               Position=new Vector3(231.524f,4.935f,154.207f),
                Rotate =new Vector3(74,90,0),
                MoveNames=new MoveNames
                {
                    Back = "MarkBox"
                },
            }
        },
        {
            "LastKey",//
            new CameraPositionInfo
            {
               Position=new Vector3(231.714f,5.59f,151.528f),
                Rotate =new Vector3(20,64,0),
                MoveNames=new MoveNames
                {
                    Back = "Gate"
                },
            }
        },
        {
            "LastDoor",//
            new CameraPositionInfo
            {
               Position=new Vector3(229.977f,5.737f,151.985f),
                Rotate =new Vector3(5,90,0),
                MoveNames=new MoveNames
                {
                },
            }
        },


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
            if (CurrentPositionName == "Koban")
                ResetKoban();
            else if (CurrentPositionName == "WallBtn")
                ResetWallBtn();
            else if (CurrentPositionName == "SensuBtn")
                ResetSensuBtn();
            else if (CurrentPositionName == "Paper")
                ResetPaper();
            else if (CurrentPositionName == "Buki")
                ResetBuki();
            else if (CurrentPositionName == "Animal")
                ResetAnimal();
            else if (CurrentPositionName == "Syuriken")
                ResetSyuriken();
            else if (CurrentPositionName == "ViewBtn")
                ResetView();
            else if (CurrentPositionName == "WindowBtn")
                ResetWinBtn();
            else if (CurrentPositionName == "ArrowBtn")
                ResetMatoBtn();
            else if (CurrentPositionName == "Map")
                ResetMap();
            else if (CurrentPositionName == "Doukutsu1")
                ResetDoukutsu();

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

        if (positionName == "3F")
            InWin3();
        else if (positionName == "OutShiro")
            OutWin3();

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
                if (!SaveLoadSystem.Instance.gameData.isOmake)
                {
                    if (CurrentPositionName == "3F" && !SaveLoadSystem.Instance.gameData.isClearChain)
                        continue;
                }

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
    private void ResetKoban()
    {
        if (SaveLoadSystem.Instance.gameData.isClearTatami)
        return;


        Koban.Status = "000";
        Koban.Before.SetActive(true);
        Koban.After.SetActive(false);
        foreach (var tap in KobanAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetWallBtn()
    {
        if (SaveLoadSystem.Instance.gameData.isClearWall)
            return;


        WallBtn.Status = "000";
        WallBtn.Before.SetActive(true);
        WallBtn.After.SetActive(false);
        foreach (var tap in WallBtnAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetSensuBtn()
    {
        if (SaveLoadSystem.Instance.gameData.isClearWallBtn)
            return;

        SensuBtn.Status = "0000";
        SensuBtn.Before.SetActive(true);
        SensuBtn.After.SetActive(false);
        SensuBtn.Key.SetActive(false);
        foreach (var tap in SensuBtnAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetPaper()
    {
        if (SaveLoadSystem.Instance.gameData.isClearDoor2)
            return;

        Paper.Status = "000";
        Paper.Before.SetActive(true);
        Paper.Rousoku.SetActive(false);
        foreach (var tap in PaperAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetBuki()
    {
        if (SaveLoadSystem.Instance.gameData.isClearBuki)
            return;

        foreach (var tap in Buki.Selects)
        {
            foreach (var btn in tap.Bukis)
                btn.SetActive(false);
        }
        Buki.Defs[Buki.Idx].Bukis[Buki.status].SetActive(true);
        Buki.Idx = 0;
        Buki.status = 0;
        Buki.oldIdx = 9;
        Buki.oldStatus = 0;
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetAnimal()
    {
        if (SaveLoadSystem.Instance.gameData.isClearByobu1
            && SaveLoadSystem.Instance.gameData.isClearByobu2
            && SaveLoadSystem.Instance.gameData.isClearByobu3)
            return;

        Animal.Status = "000";
        foreach (var tap in AnimalAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetSyuriken()
    {
        if (SaveLoadSystem.Instance.gameData.isClearSyuriken)
            return;

        Syuri.SetsArray[Syuri.TapNewIdx].Sets[Syuri.Status[Syuri.TapNewIdx].Length - 1].Syurikens[Syuri.NewStatus].SetActive(true);
        Syuri.Selects[Syuri.TapNewIdx].Syurikens[Syuri.NewStatus].SetActive(false);
        Syuri.TapNewIdx = 0;
        Syuri.TapOldIdx = 9;
        Syuri.NewStatus = 9;
        Syuri.OldStatus = 9;
    }

    /// <summary>
    /// 3階の窓の見え方調整
    /// </summary>
    private void InWin3()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearWindow3)
            return;

        Win3.Wall_in.SetActive(false);
        Win3.Open_in.SetActive(true);
    }
    //
    private void OutWin3()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearWindow3)
            return;

        Win3.Wall_in.SetActive(true);
        Win3.Open_in.SetActive(false);
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetView()
    {
        if (SaveLoadSystem.Instance.gameData.isClearWindow4)
            return;

        View.Status = "000";
        foreach (var tap in ViewAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetWinBtn()
    {
        if (SaveLoadSystem.Instance.gameData.isClearWindow2
            && SaveLoadSystem.Instance.gameData.isClearWindow3
            && SaveLoadSystem.Instance.gameData.isClearWindow4)
            return;

        WinBtn.Status = "000";
        foreach (var tap in WinBtnAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetMatoBtn()
    {
        if (SaveLoadSystem.Instance.gameData.isClearMato)
            return;

        MatoBtn.Status = "00";
        foreach (var btn in MatoBtn.Btns0)
            btn.SetActive(false);
        MatoBtn.Btns0[0].SetActive(true);
        foreach (var btn in MatoBtn.Btns1)
            btn.SetActive(false);
        MatoBtn.Btns1[0].SetActive(true);
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetMap()
    {
        if (SaveLoadSystem.Instance.gameData.isClearDoll2
            && SaveLoadSystem.Instance.gameData.isClearDoll3
            && SaveLoadSystem.Instance.gameData.isClearDoll4)
            return;


        foreach (var obj in Map.Picks)
        {
            obj.Close.SetActive(true);
            obj.Open.SetActive(false);
        }
        foreach (var obj in Map.Sets)
        {
            foreach (var btn in obj.Peaces)
                btn.SetActive(false);
        }

        Map.Status = "99999999";
        Map.oldIdx = 9;
    }

    /// <summary>
    /// ボタンの表示をリセット　
    /// </summary>
    private void ResetDoukutsu()
    {
        Doukutsu.Mark.SetActive(true);
        Doukutsu.Status = "999";
    }

}