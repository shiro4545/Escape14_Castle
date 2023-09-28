using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tansu_Tap : TapCollider
{
    public int Idx;
    public GameObject Close;
    public GameObject Open;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (!SaveLoadSystem.Instance.gameData.isOmake)
        {
            if (SaveLoadSystem.Instance.gameData.isClearTansu)
                return;
        }

        AudioManager.Instance.SoundSE("Slide3");

        if (Idx == 0)
        {
            Close.SetActive(false);
            Open.SetActive(true);
        }
        else
        {
            Close.SetActive(true);
            Open.SetActive(false);
        }

    }
}