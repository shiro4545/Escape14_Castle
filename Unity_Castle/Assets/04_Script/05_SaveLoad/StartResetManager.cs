using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;

    //ゲーム内オブジェクト**********************
    //1
    public Tea_Judge Tea;
    public Tea_Tap[] TeaAr;
    //2
    public Tansu_Tap[] TansuAr;
    //3
    public TansuBtn_Judge TansuBtn;
    public TansuBtn_Tap[] TansuBtnAr;
    //4 null
    //5
    public Rousoku_Judge Rousoku;
    //6
    public Tatami_Judge Tatami;
    public Tatami_Tap[] TatamiAr;
    //7
    public Koban_Judge Koban;
    public Koban_Tap[] KobanAr;
    //8
    public Wall_Tap Wall;
    //9
    public WallBtn_Judge WallBtn;
    public WallBtn_Tap[] WallBtnAr;
    //10
    public Sensu_Judge Sensu;
    public Sensu_Tap[] SensuAr;
    //11
    public SensuBtn_Judge SensuBtn;
    public SensuBtn_Tap[] SensuBtnAr;
    //12
    public Door1_Tap Door1;
    //13
    public Hei_Judge Hei;
    public Hei_Tap[] HeiAr;
    //14
    public Paper_Judge Paper;
    public Paper_Tap[] PaperAr;
    //15,18,35
    public Kamado_Tap Kamado;
    //16
    public Mark_Judge Mark;
    public Mark_Tap[] MarkAr;
    //17,18,19 null
    //20
    public Take_Tap Take;
    //21 null
    //22
    public Manji_Judge Manji;

    //クリアクラス
    public ClearManager Clear;

    //アイテムオブジェクト


    //<summary>
    //メニュー画面の「タイトルへ」の時
    //<summary>
    public void ResetObject()
    {
        //1
        Tea.Status = "000";
        Tea.Before.SetActive(true);
        Tea.After.SetActive(false);
        foreach(var tap in TeaAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //2
        foreach (var tap in TansuAr)
        {
            tap.Close.SetActive(true);
            tap.Open.SetActive(false);
        }

        //3
        TansuBtn.Status = "000";
        TansuBtn.Before.SetActive(true);
        TansuBtn.After.SetActive(false);
        TansuBtn.Rousoku.SetActive(false);
        foreach (var tap in TansuBtnAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //4 null

        //5
        Rousoku.Status = "000";
        Rousoku.Before.SetActive(true);
        Rousoku.After.SetActive(false);
        foreach (var obj in Rousoku.Fire)
            obj.SetActive(false);

        //6
        Tatami.Status = "000";
        Tatami.Before.SetActive(true);
        Tatami.After.SetActive(false);
        foreach (var tap in TatamiAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //7
        Koban.Status = "000";
        Koban.Before.SetActive(true);
        Koban.After.SetActive(false);
        foreach (var tap in KobanAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //8
        Wall.Idx = 0;
        foreach (var btn in Wall.Btns)
            btn.SetActive(false);
        Wall.Btns[0].SetActive(true);

        //9
        WallBtn.Status = "000";
        WallBtn.Before.SetActive(true);
        WallBtn.After.SetActive(false);
        foreach (var tap in WallBtnAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //10
        Sensu.Status = "0000";
        foreach (var tap in SensuAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //11
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

        //12
        Door1.Def.SetActive(true);
        Door1.Close.SetActive(true);
        Door1.Open.SetActive(false);

        //13
        Hei.Status = "000";
        Hei.Before.SetActive(true);
        Hei.After.SetActive(false);
        foreach (var tap in HeiAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //14
        Paper.Status = "000";
        Paper.Before.SetActive(true);
        Paper.Rousoku.SetActive(false);
        foreach (var tap in PaperAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //15,18,35
        Kamado.Def_Sumi.SetActive(true);
        Kamado.After_Sumi.SetActive(false);
        Kamado.Sumi_Clean.SetActive(false);
        Kamado.Close.SetActive(true);
        Kamado.Open.SetActive(false);

        //16
        Mark.Status = "0000";
        Mark.Before.SetActive(true);
        Mark.After.SetActive(false);
        Mark.Katana.SetActive(false);
        foreach (var tap in MarkAr)
        {
            foreach (var btn in tap.Btns)
                btn.SetActive(false);
            tap.Btns[0].SetActive(true);
        }

        //17 null
        //18 null
        //19 null

        //20
        Take.Before.SetActive(true);
        Take.After.SetActive(false);
        Take.Makimono.SetActive(false);

        //21 null

        //22
        Manji.Status = "00000";
        Manji.Before.SetActive(true);
        Manji.After.SetActive(false);

        //35 null

        //41 null

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
        if (gameData.isClearTea)
        {
            Tea.Before.SetActive(false);
            Tea.After.SetActive(true);
        }

        //2 null

        //3
        if (gameData.isClearTansu)
        {
            TansuBtn.Before.SetActive(false);
            TansuBtn.After.SetActive(true);
            TansuBtn.Rousoku.SetActive(true);
            foreach (var tap in TansuBtnAr)
                tap.Btns[0].SetActive(false);

            TansuBtnAr[0].Btns[4].SetActive(true);
            TansuBtnAr[1].Btns[9].SetActive(true);
            TansuBtnAr[2].Btns[3].SetActive(true);
        }
        if(gameData.isGetRousoku1)
            TansuBtn.Rousoku.SetActive(false);

        //4 null

        //5
        if (gameData.isClearRousoku)
        {
            Rousoku.Before.SetActive(false);
            Rousoku.After.SetActive(true);
            foreach (var obj in Rousoku.Fire)
                obj.SetActive(true);
        }

        //6
        if (gameData.isClearTatami)
        {
            Tatami.Before.SetActive(false);
            Tatami.After.SetActive(true);
            foreach (var tap in TatamiAr)
                tap.Btns[0].SetActive(false);

            TatamiAr[0].Btns[1].SetActive(true);
            TatamiAr[1].Btns[2].SetActive(true);
            TatamiAr[2].Btns[2].SetActive(true);
        }

        //7
        if (gameData.isClearKoban)
        {
            Koban.Before.SetActive(false);
            Koban.After.SetActive(true);
            foreach (var tap in KobanAr)
                tap.Btns[0].SetActive(false);

            KobanAr[0].Btns[2].SetActive(true);
            KobanAr[1].Btns[4].SetActive(true);
            KobanAr[2].Btns[1].SetActive(true);
        }

        //8 null

        //9
        if (gameData.isClearWallBtn)
        {
            Wall.Btns[0].SetActive(false);
            Wall.Btns[2].SetActive(true);
            WallBtn.Before.SetActive(false);
            WallBtn.After.SetActive(true);
        }

        //10 null

        //11
        if (gameData.isClearSensuBtn)
        {
            Sensu.Status = "1011";
            foreach (var tap in SensuAr)
                tap.Btns[0].SetActive(false);
            SensuAr[0].Btns[1].SetActive(true);
            SensuAr[1].Btns[0].SetActive(true);
            SensuAr[2].Btns[1].SetActive(true);
            SensuAr[3].Btns[1].SetActive(true);

            SensuBtn.Before.SetActive(false);
            SensuBtn.After.SetActive(true);
            SensuBtn.Key.SetActive(true);

            foreach (var tap in SensuBtnAr)
                tap.Btns[0].SetActive(false);
            SensuBtnAr[0].Btns[3].SetActive(true);
            SensuBtnAr[1].Btns[2].SetActive(true);
            SensuBtnAr[2].Btns[1].SetActive(true);
            SensuBtnAr[3].Btns[2].SetActive(true);
        }
        if(gameData.isGetKey1)
            SensuBtn.Key.SetActive(false);

        //12
        if (gameData.isClearDoor1)
        {
            Door1.Def.SetActive(false);
            Door1.Close.SetActive(false);
            Door1.Open.SetActive(true);
        }

        //13
        if (gameData.isClearDoor2)
        {
            Hei.Before.SetActive(false);
            Hei.After.SetActive(true);
        }

        //14
        if (gameData.isClearPaper)
        {
            Paper.Before.SetActive(false);
            Paper.Rousoku.SetActive(true);
            foreach (var tap in PaperAr)
                tap.Btns[0].SetActive(false);

            PaperAr[0].Btns[3].SetActive(true);
            PaperAr[1].Btns[2].SetActive(true);
            PaperAr[2].Btns[1].SetActive(true);
        }
        if(gameData.isGetRousoku3)
            Paper.Rousoku.SetActive(false);

        //15
        if (gameData.isClearFire)
        {
            Kamado.Def_Sumi.SetActive(false);
            Kamado.After_Sumi.SetActive(true);
        }

        //16
        if (gameData.isClearMark)
        {
            Mark.Before.SetActive(false);
            Mark.After.SetActive(true);
            Mark.Katana.SetActive(true);
        }
        if(gameData.isGetKatana)
            Mark.Katana.SetActive(false);

        //17 null
        //18 null
        //19 null

        //20
        if (gameData.isClearTake)
        {
            Take.Before.SetActive(false);
            Take.After.SetActive(true);
            Take.Makimono.SetActive(true);
        }
        if(gameData.isGetMakimono1)
            Take.Makimono.SetActive(false);

        //21 null

        //22
        if (gameData.isClearManji)
        {
            Manji.Before.SetActive(false);
            Manji.After.SetActive(true);
        }

        //23





        //35
        if (gameData.isClearNotFire)
        {
            Kamado.After_Sumi.SetActive(false);
            Kamado.Def_Sumi.SetActive(true);
        }
        if (gameData.isClearClean)
        {
            Kamado.Def_Sumi.SetActive(false);
            Kamado.Sumi_Clean.SetActive(true);
        }
        if (gameData.isClearOpen)
        {
            Kamado.Close.SetActive(false);
            Kamado.Open.SetActive(true);
        }


        //41 null

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