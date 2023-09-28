using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapFadeCameraMove : TapCollider
{
    public string[] MovePositionName;

    protected override void OnTap()
    {
      base.OnTap();
      FadeManager.Instance.FadeChangePositon(MovePositionName);
    }
}
