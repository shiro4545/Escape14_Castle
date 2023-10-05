using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll4_Tap : TapCollider
{

    public GameObject Before;
    public GameObject After;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearDoll4)
            return;

        AudioManager.Instance.SoundSE("Slide3");

        Before.SetActive(false);
        After.SetActive(true);

        SaveLoadSystem.Instance.gameData.isClearDoll4 = true;
        SaveLoadSystem.Instance.Save();
    }
}