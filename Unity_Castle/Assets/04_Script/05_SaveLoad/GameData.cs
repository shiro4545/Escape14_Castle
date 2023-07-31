using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //所有アイテム
    public string getItems = "";

    //アイテム取得有無
    public bool isGetKey1;
    public bool isGetKey2;

    //謎クリア有無
    public bool isClearxxx; //1
    public bool isClearAll; //39

    //オブジェクト状態
    public string StatusWeight = "000";


    //**以下、全脱出共通のため編集不可****************************

    //矢印ガイド表示有無
    public bool isArrow;
    //ATT表示有無
    public bool isATT;
    //課金有無
    public bool isPurchase;


    //サウンドの音量
    public float VolumeBGM = 0.2f;
    public float VolumeSE = 0.8f;

    //各ヒントの数と動画視聴有無(0:未視聴,1:視聴済み)
    public string[] HintFlgArray = new string[] {
        "0",     //ダミー
        "0000",  //1
        "0000",
        "0000",
        "0000",
        "0000",  //5
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //10
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //15
        "0000",
        "0000", 
        "0000",
        "0000",
        "0000",  //20
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //25
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //30
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //35
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //40
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //45
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //50
    };
}
