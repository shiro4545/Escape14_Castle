using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doukutsu_Tap : TapCollider
{
    public int Idx;

    public Doukutsu_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isGetKey3)
            return;

        //AudioManager.Instance.SoundSE("TapButton");

        //答え合わせ
        JudgeClass.Judge(Idx);
    }
}