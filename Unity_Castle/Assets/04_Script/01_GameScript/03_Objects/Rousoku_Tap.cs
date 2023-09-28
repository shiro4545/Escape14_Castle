using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rousoku_Tap : TapCollider
{
    public int Idx;

    public GameObject Act0;
    public GameObject Act1;

    public Rousoku_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (ItemManager.Instance.SelectItem != "Rousoku2")
            return;


        BlockPanel.Instance.ShowBlockNoIcon();
        Act0.SetActive(true);
        Invoke(nameof(After1), 1.5f);
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Match2");
        Act0.SetActive(false);
        Act1.SetActive(true);

        JudgeClass.Judge(Idx);
    }
}