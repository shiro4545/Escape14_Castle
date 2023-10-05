using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Doukutsu_Judge : MonoBehaviour
{
    public Camera MainCamera;

    public string Status = "999";

    public GameObject Mark;
    public GameObject[] Arrows;

    private int Idx;
    private bool isClear;

    public void Judge(int _idx)
    {
        Idx = _idx;

        BlockPanel.Instance.ShowBlockNoIcon();

        //矢印演出
        foreach (var obj in Arrows)
            obj.SetActive(false);

        Arrows[Idx].SetActive(true);

        //カメラズーム
        CameraAct(Idx);

        //ステータス更新
        Status = Status.Substring(1) + Idx;


        //答え合わせ
        if (Status == "012" && SaveLoadSystem.Instance.gameData.isClearMakimono2)
        {
            isClear = true;
            SaveLoadSystem.Instance.gameData.isClearDoukutsu = true;
            SaveLoadSystem.Instance.Save();
        }
        else
            isClear = false;

        //カメラフェード
        Invoke(nameof(Fade), 4.0f);
        //オブジェクト切替
        Invoke(nameof(SwitchObj), 4.9f);
    }

    //カメラズーム
    private void CameraAct(int Idx)
    {
        float Xvalue = 1.4f;
        if (Idx == 1)
            Xvalue = 0f;
        else if (Idx == 2)
            Xvalue = -1.4f;

        //カメラを徐々にズーム&移動
        float defaultFov = MainCamera.fieldOfView;
        DOTween.To(() => MainCamera.fieldOfView, fov => MainCamera.fieldOfView = fov, 30, 4f);
        MainCamera.transform.DOMove(new Vector3(Xvalue, 0.5f, 1f), 4f).SetRelative(true);
    }
    //カメラフェード
    private void Fade()
    {
        string Position = "Doukutsu1";
        if(isClear)
            Position = "Doukutsu2";

        FadeManager.Instance.FadeChangePositon(new string[] { Position });
    }
    //オブジェクト切替
    private void SwitchObj()
    {
        foreach (var obj in Arrows)
            obj.SetActive(true);

        Mark.SetActive(false);

        if (!Status.Contains("9"))
        {
            Mark.SetActive(true);
            Status = "999";
        }
    }
}
