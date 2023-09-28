using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rousoku0_Tap : TapCollider
{
    public GameObject Act0;
    public GameObject Act1;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (ItemManager.Instance.SelectItem != "Rousoku1")
            return;


        BlockPanel.Instance.ShowBlock();
        ItemManager.Instance.UseItem();
        Act0.SetActive(true);
        Invoke(nameof(After1), 1.5f);
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Clear");
        Act0.SetActive(false);
        Act1.SetActive(true);
        Invoke(nameof(After2), 1.5f);

    }
    private void After2()
    {
        Act1.SetActive(false);
        ItemManager.Instance.GetItem("Rousoku2");
        BlockPanel.Instance.HideBlock();
    }
}