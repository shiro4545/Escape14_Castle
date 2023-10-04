using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syuri_Judge : MonoBehaviour
{
    public GameObject Before;
    public GameObject After;
    public GameObject Key2;


    public int TapNewIdx = 0;
    public int TapOldIdx = 9;

    public int NewStatus = 9;
    public int OldStatus = 9;

    public string[] Status;

    public Syuriken_SetsArray[] SetsArray;
    public Syuriken_Sets[] Selects;

    public void Judge(int _idx)
    {
        TapNewIdx = _idx;
        Status = SaveLoadSystem.Instance.gameData.StatusSyuri;

        if (Status[TapNewIdx] != "")
            NewStatus = int.Parse(Status[TapNewIdx].Substring(0, 1));

        //1つ目の選択時
        if (TapOldIdx == 9)
        {
            //アイテムを置く場合
            if (Status[TapNewIdx].Length < 5
                && ItemManager.Instance.SelectItem.Contains("Syuriken"))
            {
                BlockPanel.Instance.ShowBlockNoIcon();

                NewStatus = int.Parse(ItemManager.Instance.SelectItem.Substring(8));
                ItemManager.Instance.UseItem();

                Selects[TapNewIdx].Syurikens[NewStatus].SetActive(true);

                //ステータス更新
                UpdateStatus();

                //置く演出
                Invoke(nameof(PutAct1), 1.2f);
            }
            //手裏剣がある場合
            else if (Status[TapNewIdx] != "")
            {
                AudioManager.Instance.SoundSE("Slide3");

                SetsArray[TapNewIdx].Sets[Status[TapNewIdx].Length-1].Syurikens[NewStatus].SetActive(false);
                Selects[TapNewIdx].Syurikens[NewStatus].SetActive(true);

                TapOldIdx = TapNewIdx;
                OldStatus = NewStatus;
            }
            //何にもない場合
            else
                return;
        }
        //2つ目の選択
        else
        {
            //同じ場所をタップした時
            if (TapNewIdx == TapOldIdx)
            {
                AudioManager.Instance.SoundSE("Syuriken");

                SetsArray[TapNewIdx].Sets[Status[TapNewIdx].Length-1].Syurikens[NewStatus].SetActive(true);
                Selects[TapNewIdx].Syurikens[NewStatus].SetActive(false);

                TapOldIdx = 9;
            }
            else
            //違う場所をタップした時
            {
                if (Status[TapNewIdx].Length == 5)
                    return;

                BlockPanel.Instance.ShowBlockNoIcon();
                Selects[TapOldIdx].Syurikens[OldStatus].SetActive(false);
                Selects[TapNewIdx].Syurikens[OldStatus].SetActive(true);

                //ステータス更新
                UpdateStatus();
                //置く演出
                Invoke(nameof(PutAct2), 1.2f);
            }
        }
    }

    /// <summary>
    /// ステーツ更新
    /// </summary>
    private void UpdateStatus()
    {
        string NewStr;
        string OldStr;

        //アイテム置く時
        if (TapOldIdx == 9)
        {
            NewStr = NewStatus + Status[TapNewIdx];
            SaveLoadSystem.Instance.gameData.StatusSyuri[TapNewIdx] = NewStr;
        }
        //交換の場合
        else
        {
            NewStr = OldStatus + Status[TapNewIdx];
            if (Status[TapOldIdx].Length == 1)
                OldStr = "";
            else
                OldStr = Status[TapOldIdx].Substring(1);

            SaveLoadSystem.Instance.gameData.StatusSyuri[TapNewIdx] = NewStr;
            SaveLoadSystem.Instance.gameData.StatusSyuri[TapOldIdx] = OldStr;

        }

        SaveLoadSystem.Instance.Save();
    }

    //アイテム置く時
    private void PutAct1()
    {
        AudioManager.Instance.SoundSE("Syuriken");
        Selects[TapNewIdx].Syurikens[NewStatus].SetActive(false);
        SetsArray[TapNewIdx].Sets[Status[TapNewIdx].Length-1].Syurikens[NewStatus].SetActive(true);

        BlockPanel.Instance.HideBlock();
        //答え合わせ
        Judge();
    }

    //交換の時
    private void PutAct2()
    {
        AudioManager.Instance.SoundSE("Syuriken");
        Selects[TapNewIdx].Syurikens[OldStatus].SetActive(false);
        SetsArray[TapNewIdx].Sets[Status[TapNewIdx].Length-1].Syurikens[OldStatus].SetActive(true);

        TapOldIdx = 9;

        BlockPanel.Instance.HideBlock();
        //答え合わせ
        Judge();
    }

    //答え合わせ
    private void Judge()
    {
        Status = SaveLoadSystem.Instance.gameData.StatusSyuri;

        if (Status[0] != "1" || Status[1] != "213" || Status[2] != "32")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearSyuriken = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Key2.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}