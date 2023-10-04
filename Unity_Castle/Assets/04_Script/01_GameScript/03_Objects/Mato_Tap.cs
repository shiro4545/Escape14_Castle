using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mato_Tap : TapCollider
{

    public GameObject Act1;
    public GameObject Act2;
    public GameObject Act3;
    public GameObject Act4;

    public GameObject Ya1;
    public GameObject Ya2;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake
            || SaveLoadSystem.Instance.gameData.isClearMato)
        {
            CameraManager.Instance.ChangeCameraPosition("Mato");
            return;
        }

        if (ItemManager.Instance.SelectItem != "Arrow2")
            return;

        BlockPanel.Instance.ShowBlock();

        Act1.SetActive(true);

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearMato = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Arrow1");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        AudioManager.Instance.SoundSE("Arrow2");
        Act2.SetActive(false);
        Act3.SetActive(true);

        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("Ya");
        Act3.SetActive(false);
        Act4.SetActive(true);
        Ya1.SetActive(true);

        Invoke(nameof(After4), 1.5f);
    }
    //
    private void After4()
    {
        Act4.SetActive(false);
        Act1.SetActive(true);

        Invoke(nameof(After5), 1.5f);
    }
    private void After5()
    {
        AudioManager.Instance.SoundSE("Arrow1");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After6), 1.5f);
    }
    private void After6()
    {
        AudioManager.Instance.SoundSE("Arrow2");
        Act2.SetActive(false);
        Act3.SetActive(true);

        Invoke(nameof(After7), 1.5f);
    }
    private void After7()
    {
        AudioManager.Instance.SoundSE("Ya");
        Act3.SetActive(false);
        Act4.SetActive(true);
        Ya2.SetActive(true);

        Invoke(nameof(After8), 1.0f);
    }
    private void After8()
    {
        ItemManager.Instance.UseItem();
        Act4.SetActive(false);
        BlockPanel.Instance.HideBlock();
    }
}