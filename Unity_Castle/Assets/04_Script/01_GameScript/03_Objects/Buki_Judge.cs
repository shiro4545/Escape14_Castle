using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buki_Judge : MonoBehaviour
{
    public string Status = "12300";

    public int Idx = 0;
    public int status = 0;
    public int oldIdx = 9;
    public int oldStatus = 0;

    public Buki_Tap[] Defs;
    public Buki_Tap[] Selects;

    public GameObject Before;
    public GameObject After;
    public GameObject Syuriken1;

    public void Judge(int _idx)
    {
        Idx = _idx;
        status = int.Parse(Status.Substring(Idx, 1));

        //1つ目の選択時
        if (oldIdx == 9)
        {
            if (status != 0)
            {
                AudioManager.Instance.SoundSE("Slide3");
                Defs[Idx].Bukis[status].SetActive(false);

                Selects[Idx].Bukis[status].SetActive(true);
                oldIdx = Idx;
                return;
            }
            else
                return;
        }
        else
        //1つ選択済みの時
        {
            //選んでいるやつを再度タップ
            if (oldIdx == Idx)
            {
                AudioManager.Instance.SoundSE("PutItem");
                Defs[Idx].Bukis[status].SetActive(true);

                Selects[Idx].Bukis[status].SetActive(false);
                oldIdx = 9;
            }
            else
            {
                oldStatus = int.Parse(Status.Substring(oldIdx, 1));

                //
                //空いているところを選択
                if(status == 0)
                {
                    Selects[oldIdx].Bukis[oldStatus].SetActive(false);
                    Defs[oldIdx].Bukis[status].SetActive(true);

                    Defs[Idx].Bukis[status].SetActive(false);
                    Selects[Idx].Bukis[oldStatus].SetActive(true);
                    BlockPanel.Instance.ShowBlockNoIcon();
                    Invoke(nameof(After0), 1.0f);
                }
                else
                {
                    AudioManager.Instance.SoundSE("Slide3");
                    Selects[oldIdx].Bukis[oldStatus].SetActive(false);
                    Defs[oldIdx].Bukis[oldStatus].SetActive(true);

                    Defs[Idx].Bukis[status].SetActive(false);
                    Selects[Idx].Bukis[status].SetActive(true);
                    oldIdx = Idx;
                }

            }
        }

    }
    private void After0()
    {
        AudioManager.Instance.SoundSE("PutItem");
        Selects[Idx].Bukis[oldStatus].SetActive(false);
        Defs[Idx].Bukis[oldStatus].SetActive(true);

        //ステータス更新
        Status = Status.Substring(0, Idx) + oldStatus + Status.Substring(Idx + 1);
        Status = Status.Substring(0, oldIdx) + status + Status.Substring(oldIdx + 1);
        oldIdx = 9;


        //答え合わせ
        if (Status != "03102")
        {
            BlockPanel.Instance.HideBlock();
            return;
        }

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(After1), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearBuki = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After1()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Syuriken1.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}