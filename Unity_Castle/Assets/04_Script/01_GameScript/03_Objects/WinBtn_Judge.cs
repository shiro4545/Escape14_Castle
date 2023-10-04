using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBtn_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;
    public GameObject Ya;


    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearWindow2
            || !SaveLoadSystem.Instance.gameData.isClearWindow3
            || !SaveLoadSystem.Instance.gameData.isClearWindow4)
            return;

        if (Status != "413")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearWindowBtn = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("Tansu2");
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Ya.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
