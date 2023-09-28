using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeManager : MonoBehaviour
{

    public static FadeManager Instance { get; private set; }

    public GameObject Panel;

    public Image fade;

    private string[] Positions;

    void Start()
    {
        Instance = this;
        fade = Panel.GetComponent<Image>(); //パネルのイメージ取得
    }

    public void FadeChangePositon(string[] _positions)
    {
        BlockPanel.Instance.ShowBlockNoIcon();

        Positions = _positions;
        StartCoroutine("FadeFunc");
    }

    IEnumerator FadeFunc()
    {
        for (int i = 0; i < Positions.Length; i++)
        {

            // 画面をフェードインさせるコールチン
            // 前提：画面を覆うPanelにアタッチしている
            Panel.SetActive(true);

            // フェード後の色を設定（黒）★変更可
            fade.color = new Color((48f / 255.0f), (48f / 255.0f), (48f / 255.0f), (0.0f / 255.0f));

            // フェードインにかかる時間（秒）★変更可
            const float fade_time = 0.2f;

            // ループ回数（0はエラー）★変更可
            const int loop_count = 30;

            // ウェイト時間算出
            float wait_time = fade_time / loop_count;

            // 色の間隔を算出
            float alpha_interval = 255.0f / loop_count;

            // 徐々に黒
            for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
            {
                // 待ち時間
                yield return new WaitForSeconds(wait_time);

                // Alpha値を少しずつ上げる
                Color new_color = fade.color;
                new_color.a = alpha / 255.0f;
                fade.color = new_color;
            }

            CameraManager.Instance.ChangeCameraPosition(Positions[i]);

            // 徐々に透明
            for (float alpha = 255.0f; alpha >= 0f; alpha -= alpha_interval)
            {
                // 待ち時間
                yield return new WaitForSeconds(wait_time);

                // Alpha値を少しずつ上げる
                Color new_color = fade.color;
                new_color.a = alpha / 255.0f;
                fade.color = new_color;
            }
            if (i < Positions.Length)
                yield return new WaitForSeconds(0.5f);

            Panel.SetActive(false);
        }

        BlockPanel.Instance.HideBlock();
    }

}
