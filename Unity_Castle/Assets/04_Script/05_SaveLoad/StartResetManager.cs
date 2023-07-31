using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;

    //ゲーム内オブジェクト**********************
    //1

    //クリアクラス
    public ClearManager Clear;

    //アイテムオブジェクト


    //<summary>
    //メニュー画面の「タイトルへ」の時
    //<summary>
    public void ResetObject()
    {
        //1

        //40



        //クリア
        Clear.ClearImage.transform.localScale = new Vector3(0, 0, 1);
        Clear.ToOtherApp.SetActive(false);
        Clear.ToTitle.SetActive(false);

        //アイテムUIリセット
        ItemManager.Instance.ItemAllDelete();
    }


    //***************************************************************************
    //<summary>
    //タイトル画面の「続きから」の時
    //<summary>
    public void GameContinue()
    {
        gameData = SaveLoadSystem.Instance.gameData;


        //1

        //39

        //40




        //アイテム
        //保有アイテム
        if (gameData.getItems == "")
        return;
        string[] arr = gameData.getItems.Split(';');
        foreach (var item in arr)
        {
            if (item != "")
                ItemManager.Instance.loadItem(item);
        }
    }




}