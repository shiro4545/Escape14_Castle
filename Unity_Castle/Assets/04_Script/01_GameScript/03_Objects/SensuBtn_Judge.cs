using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensuBtn_Judge : MonoBehaviour
{
    public string Status = "0000";

    public GameObject Before;
    public GameObject After;
    public GameObject Key;


    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearWallBtn)
            return;

        if (Status != "3212")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearSensuBtn = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("Sensu");
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Key.SetActive(true);
        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        CameraManager.Instance.ChangeCameraPosition("SensuBtn");
        BlockPanel.Instance.HideBlock();
    }
}
