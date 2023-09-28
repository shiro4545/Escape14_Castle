using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject Rousoku;


    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearDoor2)
            return;

        if (Status != "321")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearPaper = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("OpenCover");
        Before.SetActive(false);
        Rousoku.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
