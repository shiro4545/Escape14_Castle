using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kasa_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;
    public GameObject Syuriken2;


    public void Judge()
    {

        if (Status != "213")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearKasa = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Syuriken2.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
