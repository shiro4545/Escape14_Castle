using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window3_Tap : TapCollider
{

    public GameObject Close_in;
    public GameObject Close_out;

    public GameObject Open_in;
    public GameObject Open_out;

    public GameObject Wall_in; //inでは消す


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (SaveLoadSystem.Instance.gameData.isClearWindow3)
            return;

        AudioManager.Instance.SoundSE("OpenReizoko");

        Close_in.SetActive(false);
        Close_out.SetActive(false);

        Open_in.SetActive(true);
        Open_out.SetActive(true);

        Wall_in.SetActive(false);

        SaveLoadSystem.Instance.gameData.isClearWindow3 = true;
        SaveLoadSystem.Instance.Save();
    }
}
