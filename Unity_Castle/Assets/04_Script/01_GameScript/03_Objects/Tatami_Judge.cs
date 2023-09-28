using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatami_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;

    public void Judge()
    {
        if (Status != "122")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearTatami = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("Shelf");
        Invoke(nameof(After2), 1.5f);
    }
    //
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
