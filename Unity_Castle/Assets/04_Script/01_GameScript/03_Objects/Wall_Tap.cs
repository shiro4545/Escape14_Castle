using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Tap : TapCollider
{
    public int Idx;

    public GameObject[] Btns;


    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (!SaveLoadSystem.Instance.gameData.isOmake)
        {
            if (!SaveLoadSystem.Instance.gameData.isClearKoban)
                return;
            if (SaveLoadSystem.Instance.gameData.isClearWallBtn)
                return;
        }
        else
        {
            //おまけ
        }

        BlockPanel.Instance.ShowBlockNoIcon();

        //ステータス取得
        Btns[Idx].SetActive(false);
        ChangeIdx();
        Btns[Idx].SetActive(true);

        Invoke(nameof(After1), 1.0f);

        SaveLoadSystem.Instance.gameData.isClearWall = true;
        SaveLoadSystem.Instance.Save();

    }
    //
    private void ChangeIdx()
    {
        Idx++;
        if (Idx >= Btns.Length)
            Idx = 0;
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("SeacretWall");
        Btns[Idx].SetActive(false);
        ChangeIdx();
        Btns[Idx].SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}