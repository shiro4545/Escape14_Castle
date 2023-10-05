using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ike_Tap : TapCollider
{

    public GameObject Oke;

    public GameObject Act1;
    public GameObject Act2;
    public GameObject Act3;
    public GameObject Act4;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;
        if (ItemManager.Instance.SelectItem == "Oke1")
        {
            BlockPanel.Instance.ShowBlock();
            ItemManager.Instance.UseItem();

            AudioManager.Instance.SoundSE("Water4");
            Oke.SetActive(true);

            Invoke(nameof(AfterOke), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearOke = true;
            SaveLoadSystem.Instance.Save();
            return;
        }
        else if (ItemManager.Instance.SelectItem == "Makimono2_open1")
        {

            BlockPanel.Instance.ShowBlock();
            ItemManager.Instance.UseItem();

            Act1.SetActive(true);


            Invoke(nameof(After1), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearMakimono2 = true;
            SaveLoadSystem.Instance.Save();
        }
    }

    //Oke
    private void AfterOke()
    {
        ItemManager.Instance.GetItem("Oke2");
        Oke.SetActive(false);
        BlockPanel.Instance.HideBlock();
    }

    //Makimono2
    private void After1()
    {
        AudioManager.Instance.SoundSE("Water3");
        Act1.SetActive(false);
        Act2.SetActive(true);

        Invoke(nameof(After2), 1.5f);
    }
    private void After2()
    {
        Act2.SetActive(false);
        Act3.SetActive(true);

        Invoke(nameof(After3), 1.5f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("Water3");
        Act3.SetActive(false);
        Act4.SetActive(true);

        Invoke(nameof(After4), 1.5f);
    }
    private void After4()
    {
        Act4.SetActive(false);
        ItemManager.Instance.GetItem("Makimono2_open2");
        BlockPanel.Instance.HideBlock();
    }
}
