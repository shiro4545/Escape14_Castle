using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manji_Tap : TapCollider
{
    public int Idx;

    public Manji_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearManji)
            return;

        AudioManager.Instance.SoundSE("TapButton");


        //ボタン切替
        this.gameObject.transform.Translate(new Vector3(0, 0, -0.004f));

        Invoke(nameof(AfterPush), 0.1f);


        //答え合わせ
        JudgeClass.Judge(Idx);
    }
    //
    private void AfterPush()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.004f));

    }
}