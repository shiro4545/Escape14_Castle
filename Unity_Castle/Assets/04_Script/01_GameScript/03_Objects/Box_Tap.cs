using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Tap : TapCollider
{

    public GameObject Close;
    public GameObject Open;

    public GameObject Key3;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;

        AudioManager.Instance.SoundSE("OpenCover");

        Close.SetActive(false);
        Open.SetActive(true);
        Key3.SetActive(true);


        SaveLoadSystem.Instance.gameData.isOpenBox = true;
        SaveLoadSystem.Instance.Save();

    }
}