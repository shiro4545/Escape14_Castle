using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;
    public GameObject Syuriken3;


    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearByobu1
            || !SaveLoadSystem.Instance.gameData.isClearByobu2
            || !SaveLoadSystem.Instance.gameData.isClearByobu3)
            return;

        if (Status != "213")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearAnimal = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("3FTansu");
        Invoke(nameof(After2), 1.5f);
    }
    //
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Syuriken3.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
