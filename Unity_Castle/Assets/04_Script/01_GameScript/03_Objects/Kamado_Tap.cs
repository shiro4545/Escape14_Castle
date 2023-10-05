using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamado_Tap : TapCollider
{

    public GameObject Def_Sumi;
    public GameObject Act_Rousoku;
    public GameObject After_Sumi;

    public GameObject Act_Katana1;
    public GameObject Act_Katana2;

    public GameObject Act_Oke;

    public GameObject Sumi_Clean;

    public GameObject Close;
    public GameObject Open;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;

        //15
        if (ItemManager.Instance.SelectItem == "Rousoku3")
        {
            BlockPanel.Instance.ShowBlock();
            ItemManager.Instance.UseItem();
            Act_Rousoku.SetActive(true);

            Invoke(nameof(After1_1), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearFire = true;
            SaveLoadSystem.Instance.Save();
            return;
        }

        //18
        if(SaveLoadSystem.Instance.gameData.isClearFire
            && ItemManager.Instance.SelectItem == "Katana2")
        {
            BlockPanel.Instance.ShowBlock();
            ItemManager.Instance.UseItem();
            Act_Katana1.SetActive(true);
            AudioManager.Instance.SoundSE("Fire");

            Invoke(nameof(After2_1), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearKatana = true;
            SaveLoadSystem.Instance.Save();
            return;
        }

        //35 水かけ
        if (ItemManager.Instance.SelectItem == "Oke2")
        {
            BlockPanel.Instance.ShowBlock();
            ItemManager.Instance.UseItem();
            AudioManager.Instance.SoundSE("Water5");
            Act_Oke.SetActive(true);

            Invoke(nameof(After3_1), 1.5f);

            SaveLoadSystem.Instance.gameData.isClearNotFire = true;
            SaveLoadSystem.Instance.Save();
            return;
        }
        //35 炭はらい
        if (SaveLoadSystem.Instance.gameData.isClearNotFire
            && !SaveLoadSystem.Instance.gameData.isClearClean)
        {
            AudioManager.Instance.SoundSE("Slide1");
            Def_Sumi.SetActive(false);
            Sumi_Clean.SetActive(true);

            SaveLoadSystem.Instance.gameData.isClearClean = true;
            SaveLoadSystem.Instance.Save();
            return;
        }
        //35 床開け
        if (SaveLoadSystem.Instance.gameData.isClearClean
            && !SaveLoadSystem.Instance.gameData.isClearOpen)
        {
            AudioManager.Instance.SoundSE("Clear");
            Close.SetActive(false);
            Open.SetActive(true);

            SaveLoadSystem.Instance.gameData.isClearOpen = true;
            SaveLoadSystem.Instance.Save();
            return;
        }
        //35 地下へ
        if (SaveLoadSystem.Instance.gameData.isClearOpen)
        {
            FadeManager.Instance.FadeChangePositon(new string[] { "Hashigo","Doukutsu1" });
            return;
        }


    }
    //15
    private void After1_1()
    {
        AudioManager.Instance.SoundSE("Fire");
        Act_Rousoku.SetActive(false);
        Def_Sumi.SetActive(false);
        After_Sumi.SetActive(true);
        Invoke(nameof(After1_2), 2.3f);
    }
    private void After1_2()
    {
        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.HideBlock();
    }
    //18
    private void After2_1()
    {
        Act_Katana1.SetActive(false);
        Act_Katana2.SetActive(true);
        Invoke(nameof(After2_2), 1.5f);
    }
    private void After2_2()
    {
        ItemManager.Instance.GetItem("Katana3");
        Act_Katana2.SetActive(false);
        BlockPanel.Instance.HideBlock();
    }
    //35
    private void After3_1()
    {
        Act_Oke.SetActive(false);
        After_Sumi.SetActive(false);
        Def_Sumi.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }

}