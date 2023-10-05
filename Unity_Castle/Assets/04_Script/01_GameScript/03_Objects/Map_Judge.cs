using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Judge : MonoBehaviour
{
    public int oldIdx = 9;

    public string Status = "99999999";

    public Map_Pick[] Picks;
    public Map_Set[] Sets;

    public GameObject Before;
    public GameObject After;
    public GameObject Batsu;

    //選択時のタップ
    public void Select(int SelectIdx)
    {
        AudioManager.Instance.SoundSE("Slide3");

        //同じやつを選択
        if (oldIdx == SelectIdx)
        {
            Picks[SelectIdx].Open.SetActive(false);
            Picks[SelectIdx].Close.SetActive(true);
            oldIdx = 9;
        }
        //別のやつを選択時
        else
        {
            Picks[SelectIdx].Open.SetActive(true);
            Picks[SelectIdx].Close.SetActive(false);

            if (oldIdx != 9)
            {
                Picks[oldIdx].Open.SetActive(false);
                Picks[oldIdx].Close.SetActive(true);
            }
            oldIdx = SelectIdx;
        }
    }

    //セット時のタップ
    public void Set(int Idx)
    {
        int before = int.Parse(Status.Substring(Idx, 1));
        int after = 9;

        //既においたるやつをどかす
        if (before != 9)
        {
            Sets[Idx].Peaces[before].SetActive(false);
            Picks[before].Close.SetActive(true);
        }

        //新しいやつを置く
        if (oldIdx != 9)
        {
            AudioManager.Instance.SoundSE("Slide2");

            after = oldIdx;
            Sets[Idx].Peaces[after].SetActive(true);
            Picks[after].Open.SetActive(false);
            oldIdx = 9;
        }

        //ステータス更新
        Status = Status.Substring(0, Idx) + after + Status.Substring(Idx + 1);

        if (!SaveLoadSystem.Instance.gameData.isClearDoll2
            || !SaveLoadSystem.Instance.gameData.isClearDoll3
            || !SaveLoadSystem.Instance.gameData.isClearDoll4)
            return;

        //答え合わせ
        if (Status != "20999391")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearMap = true;
        SaveLoadSystem.Instance.Save();

    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Syuji");
        Before.SetActive(false);
        Batsu.SetActive(true);
        After.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}