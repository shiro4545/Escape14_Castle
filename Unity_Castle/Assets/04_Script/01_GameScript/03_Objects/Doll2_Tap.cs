using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll2_Tap : TapCollider
{

    public GameObject Before;
    public GameObject After;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearDoll2)
            return;

        AudioManager.Instance.SoundSE("PutCloth");

        Before.SetActive(false);
        After.SetActive(true);

        SaveLoadSystem.Instance.gameData.isClearDoll2 = true;
        SaveLoadSystem.Instance.Save();
    }
}