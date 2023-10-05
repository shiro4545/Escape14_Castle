using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Pick : TapCollider
{
    public int Idx;

    public GameObject Close;
    public GameObject Open;

    public Map_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (SaveLoadSystem.Instance.gameData.isClearMap)
            return;

        JudgeClass.Select(Idx);
    }
}