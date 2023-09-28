using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark_Judge : MonoBehaviour
{
    public string Status = "0000";

    public GameObject Before;
    public GameObject After;
    public GameObject Katana;


    public void Judge()
    {

        if (Status != "3323")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearMark = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("MarkBox");
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Katana.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
