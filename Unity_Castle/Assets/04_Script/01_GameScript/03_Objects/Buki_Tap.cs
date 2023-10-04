using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buki_Tap : TapCollider
{
    public int Idx;

    public GameObject[] Bukis;

    public Buki_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearBuki)
            return;

        //答え合わせ
        JudgeClass.Judge(Idx);
    }
}