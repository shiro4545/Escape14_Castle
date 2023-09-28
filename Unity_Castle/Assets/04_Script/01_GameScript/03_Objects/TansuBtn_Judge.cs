using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TansuBtn_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;
    public GameObject Rousoku;

    public void Judge()
    {
        if (Status != "493")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearTansu = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("Tansu");
        Invoke(nameof(After2), 1.5f);
    }
    //
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Rousoku.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
