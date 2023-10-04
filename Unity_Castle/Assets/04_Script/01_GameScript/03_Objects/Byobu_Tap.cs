using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byobu_Tap : TapCollider
{
    public int Idx;

    public GameObject Close;
    public GameObject Open;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;

        if (Idx == 1 && SaveLoadSystem.Instance.gameData.isClearByobu1)
            return;
        else if (Idx == 2 && SaveLoadSystem.Instance.gameData.isClearByobu2)
            return;
        else if (Idx == 3 && SaveLoadSystem.Instance.gameData.isClearByobu3)
            return;


        if (Idx == 1)
            SaveLoadSystem.Instance.gameData.isClearByobu1 = true;
        else if(Idx ==2)
            SaveLoadSystem.Instance.gameData.isClearByobu2 = true;
        else if (Idx == 3)
            SaveLoadSystem.Instance.gameData.isClearByobu3 = true;

        SaveLoadSystem.Instance.Save();

        AudioManager.Instance.SoundSE("Kyatatsu");
        Close.SetActive(false);
        Open.SetActive(true);
    }
}