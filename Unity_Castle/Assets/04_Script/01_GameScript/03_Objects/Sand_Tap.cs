using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand_Tap : TapCollider
{

    public GameObject Act1;
    public GameObject Act2;

    public GameObject Def;
    public GameObject Makimono2;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isOmake)
            return;

        if (SaveLoadSystem.Instance.gameData.isClearKuwa
            && !SaveLoadSystem.Instance.gameData.isGetMakimono2)
        {
            Makimono2.SetActive(false);
            ItemManager.Instance.GetItem("Makimono2");
            Invoke(nameof(AfterGet), 0.4f);

        }
        else if (ItemManager.Instance.SelectItem == "Kuwa")
        {
            BlockPanel.Instance.ShowBlock();

            Act1.SetActive(true);

            Invoke(nameof(After1), 1.0f);


            SaveLoadSystem.Instance.gameData.isClearKuwa = true;
            SaveLoadSystem.Instance.Save();
        }
    }
    private void After1()
    {
        AudioManager.Instance.SoundSE("Kuwa");
        Act1.SetActive(false);
        Act2.SetActive(true);
        Invoke(nameof(After2), 0.6f);
    }
    private void After2()
    {
        Act2.SetActive(false);
        Act1.SetActive(true);
        Invoke(nameof(After3), 0.6f);
    }
    private void After3()
    {
        AudioManager.Instance.SoundSE("Kuwa");
        Act1.SetActive(false);
        Act2.SetActive(true);
        Invoke(nameof(After4), 1.3f);
    }
    private void After4()
    {
        AudioManager.Instance.SoundSE("Clear");
        ItemManager.Instance.UseItem();
        Act2.SetActive(false);
        Def.SetActive(false);
        Makimono2.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
    //
    private void AfterGet()
    {
        GoogleAds.Instance.ShowInterstitialAd2();
        BlockPanel.Instance.HideBlock();
    }
}