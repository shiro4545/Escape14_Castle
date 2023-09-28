using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hei_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;


    public void Judge()
    {

        if (Status != "213")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearDoor2 = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("Door2");
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("OpenShelf");
        Before.SetActive(false);
        After.SetActive(true);
        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        CameraManager.Instance.ChangeCameraPosition("Kajiya1");
        BlockPanel.Instance.HideBlock();
    }
}
