using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatoBtn_Judge : MonoBehaviour
{
    public string Status = "00";

    public GameObject Before;
    public GameObject After;
    public GameObject Kuwa;
    public GameObject Oke;

    public GameObject[] Btns0;
    public GameObject[] Btns1;

    public void Judge()
    {

        if (!SaveLoadSystem.Instance.gameData.isClearMato)
            return;

        if (Status != "71")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearMatoBtn = true;
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
        Kuwa.SetActive(true);
        Oke.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
