using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Tap : TapCollider
{

    public GameObject Before;
    public GameObject After;
    public GameObject Makimono;

    public GameObject Act1_1;
    public GameObject Act1_2;

    public GameObject Act2_1;
    public GameObject Act2_2;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;

        if (ItemManager.Instance.SelectItem == "Katana2")
        {
            BlockPanel.Instance.ShowBlockNoIcon();
            Act1_1.SetActive(true);

            Invoke(nameof(After1), 1.5f);

            return;
        }

        if (ItemManager.Instance.SelectItem == "Katana4")
        {
            BlockPanel.Instance.ShowBlock();
            ItemManager.Instance.UseItem();

            Act2_1.SetActive(true);

            Invoke(nameof(After3), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearTake = true;
            SaveLoadSystem.Instance.Save();
        }
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Katana2");
        Act1_1.SetActive(false);
        Act1_2.SetActive(true);

        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        Act1_2.SetActive(false);
        BlockPanel.Instance.HideBlock();
    }

    //
    private void After3()
    {
        AudioManager.Instance.SoundSE("Katana1");
        Act2_1.SetActive(false);
        Act2_2.SetActive(true);


        Invoke(nameof(After4), 1.5f);
    }
    private void After4()
    {
        AudioManager.Instance.SoundSE("Clear");

        Act2_2.SetActive(false);

        Before.SetActive(false);
        After.SetActive(true);
        Makimono.SetActive(true);

        BlockPanel.Instance.HideBlock();
    }
}