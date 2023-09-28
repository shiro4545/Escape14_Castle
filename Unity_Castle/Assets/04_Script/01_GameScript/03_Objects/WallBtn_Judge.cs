using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBtn_Judge : MonoBehaviour
{
    public string Status = "000";

    public GameObject Before;
    public GameObject After;

    public Wall_Tap Wall;

    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearWall)
            return;

        if (Status != "312")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearWallBtn = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);

        foreach (var btn in Wall.Btns)
            btn.SetActive(false);
        Wall.Btns[2].SetActive(true);

        BlockPanel.Instance.HideBlock();
    }
}
