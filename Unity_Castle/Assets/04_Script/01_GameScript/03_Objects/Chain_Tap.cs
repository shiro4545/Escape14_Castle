using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain_Tap : TapCollider
{

    public GameObject Def;
    public GameObject Act1;
    public GameObject Act2;

    public GameObject Close;
    public GameObject Open;

    public GameObject Arrow;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (ItemManager.Instance.SelectItem != "Key2")
            return;

        BlockPanel.Instance.ShowBlock();
        ItemManager.Instance.UseItem();

        Def.SetActive(false);
        Act1.SetActive(true);


        Invoke(nameof(After1), 1.5f);

    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Key");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        CameraManager.Instance.ChangeCameraPosition("3F");
        Act2.SetActive(false);

        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("Chain");
        Close.SetActive(false);
        Open.SetActive(true);
        Invoke(nameof(After4), 1.5f);
    }
    private void After4()
    {
        AudioManager.Instance.SoundSE("Clear");
        Arrow.SetActive(true);
        Invoke(nameof(After5), 1.5f);
    }
    private void After5()
    {
        SaveLoadSystem.Instance.gameData.isClearChain = true;
        SaveLoadSystem.Instance.Save();

        GoogleAds.Instance.ShowInterstitialAd2();
        BlockPanel.Instance.HideBlock();
    }
}