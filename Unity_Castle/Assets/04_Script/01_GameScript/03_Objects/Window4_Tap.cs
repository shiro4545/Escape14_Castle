using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window4_Tap : TapCollider
{

    public GameObject Close;
    public GameObject Open;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isClearWindow4 ||
            SaveLoadSystem.Instance.gameData.isOmake)
        {
            CameraManager.Instance.ChangeCameraPosition("View");
            return;
        }

        AudioManager.Instance.SoundSE("OpenReizoko");

        Close.SetActive(false);
        Open.SetActive(true);

        SaveLoadSystem.Instance.gameData.isClearWindow4 = true;
        SaveLoadSystem.Instance.Save();
    }
}