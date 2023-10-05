using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll3_Tap : TapCollider
{

    public GameObject Def;
    public GameObject Act;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Water4");

        Def.SetActive(false);
        Act.SetActive(true);

        Invoke(nameof(After1), 2.0f);


        SaveLoadSystem.Instance.gameData.isClearDoll3 = true;
        SaveLoadSystem.Instance.Save();
    }
    private void After1()
    {
        AudioManager.Instance.SoundSE("Water4");
        Act.SetActive(false);
        Def.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}