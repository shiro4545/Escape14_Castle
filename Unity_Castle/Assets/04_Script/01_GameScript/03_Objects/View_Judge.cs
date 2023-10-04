using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;
    public GameObject Arrow;


    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearWindow4)
            return;

        if (Status != "746")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearView = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("TopTansu");
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Arrow.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
