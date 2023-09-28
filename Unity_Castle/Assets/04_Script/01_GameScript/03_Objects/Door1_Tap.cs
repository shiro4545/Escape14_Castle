using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1_Tap : TapCollider
{

    public GameObject Def;
    public GameObject Act1;
    public GameObject Act2;

    public GameObject Close;
    public GameObject Open;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (ItemManager.Instance.SelectItem != "Key1")
            return;

        BlockPanel.Instance.ShowBlock();
        ItemManager.Instance.UseItem();

        Def.SetActive(false);
        Act1.SetActive(true);


        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearDoor1 = true;
        SaveLoadSystem.Instance.Save();
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
        CameraManager.Instance.ChangeCameraPosition("RoomDoor");
        Act2.SetActive(false);

        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("OpenShelf");
        Close.SetActive(false);
        Open.SetActive(true);
        Invoke(nameof(After4), 1.5f);
    }
    private void After4()
    {
        CameraManager.Instance.ChangeCameraPosition("OutYashiki");

        Invoke(nameof(After5), 1.5f);
    }
    private void After5()
    {
        GoogleAds.Instance.ShowInterstitialAd2();
        BlockPanel.Instance.HideBlock();
    }
}