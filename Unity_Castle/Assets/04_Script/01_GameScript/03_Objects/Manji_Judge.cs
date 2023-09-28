using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manji_Judge : MonoBehaviour
{
    public string Status = "00000";

    public GameObject Def;
    public GameObject Act;

    public GameObject Before;
    public GameObject After;

    public void Judge(int Idx)
    {
        if (!SaveLoadSystem.Instance.gameData.isClearMakimono1)
            return;

        //ステータス更新
        Status = Status.Substring(1) + Idx;

        //答え合わせ
        if (Status != "35241")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearManji = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Key");
        Def.SetActive(false);
        Act.SetActive(true);
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        CameraManager.Instance.ChangeCameraPosition("ShiroGate");
        ItemManager.Instance.DeleteItem("Makimono1_open");
        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("OpenShelf");
        Before.SetActive(false);
        After.SetActive(true);

        Def.SetActive(true);
        Act.SetActive(false);

        BlockPanel.Instance.HideBlock();
    }
}