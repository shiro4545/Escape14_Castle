using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaji_Tap : TapCollider
{

    public GameObject Def;
    public GameObject Act1;
    public GameObject Act2;
    public GameObject Act3;

    private float time = 0.5f;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (ItemManager.Instance.SelectItem != "Katana3")
            return;

        BlockPanel.Instance.ShowBlock();
        ItemManager.Instance.UseItem();

        Def.SetActive(false);
        Act1.SetActive(true);


        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearKatana = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Hummer");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After2), time);
    }
    private void After2()
    {
        Act1.SetActive(true);
        Act2.SetActive(false);
        Invoke(nameof(After3), time);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("Hummer");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After4), time);
    }
    private void After4()
    {
        Act1.SetActive(true);
        Act2.SetActive(false);

        Invoke(nameof(After5), time);
    }
    private void After5()
    {
        AudioManager.Instance.SoundSE("Hummer");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After6), time);
    }
    private void After6()
    {
        Act2.SetActive(false);
        Act3.SetActive(true);
        Invoke(nameof(After7), 1.5f);
    }
    private void After7()
    {
        Act3.SetActive(false);
        Def.SetActive(true);
        ItemManager.Instance.GetItem("Katana4");
        BlockPanel.Instance.HideBlock();
    }
}