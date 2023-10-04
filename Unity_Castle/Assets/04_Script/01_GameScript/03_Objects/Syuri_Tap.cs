using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syuri_Tap : TapCollider
{
    public int Idx;

    public Syuri_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearSyuriken)
            return;

        //答え合わせ
        JudgeClass.Judge(Idx);
    }
}