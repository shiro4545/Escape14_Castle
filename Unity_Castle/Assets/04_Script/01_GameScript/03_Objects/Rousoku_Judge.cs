using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rousoku_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;
    public GameObject[] Fire;

    public void Judge(int Idx)
    {

        //ステータス更新
        Status = Status.Substring(1) + Idx;

        //答え合わせ
        if (Status.Contains("0"))
        {
            BlockPanel.Instance.HideBlock();
            return;
        }

        if (Status != "123")
        {
            Invoke(nameof(After0), 1.1f);
            return;
        }

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");


        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearRousoku = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After0()
    {
        Status = "000";
        foreach (var obj in Fire)
            obj.SetActive(false);
        BlockPanel.Instance.HideBlock();
    }
    //
    private void After1()
    {
        CameraManager.Instance.ChangeCameraPosition("RoomSensu");
        ItemManager.Instance.UseItem();
        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        CameraManager.Instance.ChangeCameraPosition("Rousoku3");
        BlockPanel.Instance.HideBlock();
    }
}