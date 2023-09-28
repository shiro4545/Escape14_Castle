using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;

    public void Judge()
    {
        if (Status != "312")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearTea = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("RoomTansu");
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
