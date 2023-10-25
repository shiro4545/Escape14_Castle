using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omake_Tap : TapCollider
{
    public int Idx;

    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("ItemGet");

        this.gameObject.SetActive(false);
        OmakeManager.Instance.GetDoll(Idx);
    }
}