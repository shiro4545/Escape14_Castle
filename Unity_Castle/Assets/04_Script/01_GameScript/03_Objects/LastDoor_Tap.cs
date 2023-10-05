using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoor_Tap : TapCollider
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
        if (ItemManager.Instance.SelectItem != "Key3")
            return;

        BlockPanel.Instance.ShowBlock();
        ItemManager.Instance.UseItem();

        Def.SetActive(false);
        Act1.SetActive(true);


        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearAll = true;
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
        CameraManager.Instance.ChangeCameraPosition("LastDoor");
        Act2.SetActive(false);

        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("OpenShelf3");
        Close.SetActive(false);
        Open.SetActive(true);
        Invoke(nameof(After4), 1.5f);
    }
    private void After4()
    {
        ClearManager.Instance.Escape();
        BlockPanel.Instance.HideBlock();
    }
}