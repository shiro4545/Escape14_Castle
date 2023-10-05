using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Set : TapCollider
{
    public int Idx;

    public GameObject[] Peaces;

    public Map_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();


        if (SaveLoadSystem.Instance.gameData.isClearMap)
            return;

        JudgeClass.Set(Idx);
    }
}