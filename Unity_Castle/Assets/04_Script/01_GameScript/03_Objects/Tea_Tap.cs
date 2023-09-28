using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea_Tap : TapCollider
{
    public int Idx;

    public GameObject[] Btns;

    public Tea_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearTea)
            return;

        AudioManager.Instance.SoundSE("Cup");

        //ステータス取得
        string _status = JudgeClass.Status;
        int Before = int.Parse(_status.Substring(Idx, 1));
        int After = Before + 1;
        if (After >= Btns.Length)
            After = 0;

        //ステータス更新
        _status = _status.Substring(0, Idx) + After + _status.Substring(Idx + 1);
        JudgeClass.Status = _status;

        //ボタン切替
        Btns[Before].SetActive(false);
        Btns[After].SetActive(true);


        //答え合わせ
        JudgeClass.Judge();
    }
}