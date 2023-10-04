using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window2_Tap : TapCollider
{

    public GameObject Close;
    public GameObject Open;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearWindow2)
            return;

        AudioManager.Instance.SoundSE("OpenReizoko");

        Close.SetActive(false);
        Open.SetActive(true);

        SaveLoadSystem.Instance.gameData.isClearWindow2 = true;
        SaveLoadSystem.Instance.Save();
    }
}
