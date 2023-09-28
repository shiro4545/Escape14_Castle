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
    public bool isGetKey3;
    public bool isGetRousoku1;
    public bool isGetRousoku3;
    public bool isGetKatana;
    public bool isGetSyuriken1;
    public bool isGetSyuriken2;
    public bool isGetSyuriken3;
    public bool isGetMakimono1;
    public bool isGetMakimono2;
    public bool isGetYa;
    public bool isGetArrow;
    public bool isGetKuwa;
    public bool isGetOke;

    //謎クリア有無
    public bool isClearTea; //1
    public bool isClearTansu; //2,3
    public bool isClearRousoku; //4,5
    public bool isClearTatami; //6
    public bool isClearKoban; //7
    public bool isClearWall; //8
    public bool isClearWallBtn; //9
    public bool isClearSensu1; //10
    public bool isClearSensuBtn; //11
    public bool isClearDoor1; //12
    public bool isClearDoor2; //13
    public bool isClearPaper; //14
    public bool isClearFire; //15
    public bool isClearMark; //16
    public bool isClearKatana; //17,18,19
    public bool isClearTake; //20
    public bool isClearMakimono1; //21
    public bool isClearManji; //22
    public bool isClearBuki; //23
    public bool isClearKasa; //24
    public bool isClearAnimal; //25
    public bool isClearSyuriken; //26
    public bool isClearChain; //27
    public bool isClearView; //28
    public bool isClearWindow; //29,30
    public bool isClearArrow; //31,32
    public bool isClearMato; //33
    public bool isClearOke; //34
    public bool isClearNotFire; //35
    public bool isClearClean; //35
    public bool isClearOpen; //35
    public bool isClearDoll2; //36
    public bool isClearDoll3; //36
    public bool isClearDoll4; //36
    public bool isClearMap; //37
    public bool isClearKuwa; //38
    public bool isClearMakimono2; //39,40
    public bool isClearSensu2; //41
    public bool isClearDoukutsu; //42
    public bool isClearAll; //43

    //オブジェクト状態
    public string StatusSensu = "0000";


    //**以下、全脱出共通のため編集不可****************************

    //矢印ガイドの初期表示有無
    public bool isArrow;
    //ATT表示有無
    public bool isATT;
    //課金有無
    public bool isPurchase;
    //本編クリア有無
    public bool isClear;
    //おまけモード
    public bool isOmake;


    //サウンドの音量
    public float VolumeBGM = 0.2f;
    public float VolumeSE = 0.8f;

    //本編ヒントの動画視聴有無(0:未視聴,1:視聴済み)
    public string[] HintFlgArray1 = new string[] {
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

    //おまけヒントの動画視聴有無(0:未視聴,1:視聴済み)
    public string[] HintFlgArray2 = new string[] {
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
    };
}
