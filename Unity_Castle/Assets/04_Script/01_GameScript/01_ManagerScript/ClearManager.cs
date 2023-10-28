using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using Google.Play.Review;

public class ClearManager : MonoBehaviour
{

    public static ClearManager Instance { get; private set; }

    //UIパネル
    public GameObject ClearPanel;
    public GameObject GamePanel;
    //「脱出成功」
    public GameObject ClearImage;
    //「他の脱出ゲーム」
    public GameObject ToOtherApp;
    //「タイトルへ」
    public GameObject ToTitle;
    //「おまけ」
    public GameObject ToOmake;

    //真っ白パネル
    public GameObject White;

    //カメラ
    public Camera MainCamera;

    //クリア画面用の部分
    public GameObject ForEnd;
    public GameObject ForOmakeEnd;
    public GameObject NotEnd;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    //本編　脱出演出
    public void Escape()
    {
        ForEnd.SetActive(true);

        SaveLoadSystem.Instance.gameData.isClear = true;
        SaveLoadSystem.Instance.Save();

        //NotEnd.SetActive(false);

        //クリアパネル表示
        ClearPanel.SetActive(true);
        //カメラを徐々にズーム&移動
        float defaultFov = MainCamera.fieldOfView;
        DOTween.To(() => MainCamera.fieldOfView, fov => MainCamera.fieldOfView = fov, 30, 5.9f);
        MainCamera.transform.DOMove(new Vector3(0.15f, 0.1f, 0f), 5.9f).SetRelative(true);

        //白パネルのフェード
        Invoke(nameof(DelayFade), 1.7f);
        //クリア画面の演出
        Invoke(nameof(AfterClear1), 6);
    }

    //おまけ　脱出演出
    public void EscapeOmake()
    {
        ClearPanel.SetActive(true);

        //窓切替 (固有)
        CameraManager.Instance.OutWin3();

        //白パネルのフェード
        Invoke(nameof(DelayFade), 0.2f);
        //クリア画面の演出
        Invoke(nameof(AfterClear1), 4.5f);
        Invoke(nameof(ShowOmakeEnd), 4.5f);
    }

    //おまけエンド表示
    private void ShowOmakeEnd()
    {
        ForOmakeEnd.SetActive(true);
    }

    //白パネルのフェードディレイ
    private void DelayFade()
    {
        StartCoroutine("FadeFunc");
    }

    //クリア画面の演出
    private void AfterClear1()
    {
        GamePanel.SetActive(false);

        //カメラ移動
        MainCamera.fieldOfView = 60;
        CameraManager.Instance.ChangeCameraPosition("End");

        //各パーツを表示
        ClearImage.SetActive(true);

        AudioManager.Instance.SoundSE("Ending");

        //「脱出成功」をズームイン
        ClearImage.transform.DOScale(new Vector3(7.2f, 2.9f, 2), 4f)
                .SetDelay(0.5f)
                .SetEase(Ease.OutBounce);

        //「おすすめアプリ」と「タイトルへ」をフェードイン
        Invoke(nameof(DelayFadeBtn), 6.5f);


        //フェードアウト後に非表示に
        Invoke(nameof(HideWhite), 5.9f);
        //アプリレビュー表示
        Invoke(nameof(ShowReview), 8.5f);
    }

    //「おすすめアプリ」と「タイトルへ」をフェードイン
    private void DelayFadeBtn()
    {
        ToOmake.SetActive(false);
        ToTitle.SetActive(false);
        StartCoroutine("FadeBtn");
    }
    //白パネルを非表示に
    private void HideWhite()
    {
        BlockPanel.Instance.HideBlock();
    }

    //白パネルフェードイン・フェードアウト
    IEnumerator FadeFunc()
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている
        White.SetActive(true);

        Image fade = White.GetComponent<Image>(); //パネルのイメージ取得;
        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((255.0f / 255.0f), (255.0f / 255.0f), (255.0f / 255.0f), (0.0f / 255.0f));
        // フェードインにかかる時間（秒）★変更可
        float fade_time = 2.5f;
        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;
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

        //白画面の維持時間
        yield return new WaitForSeconds(1.5f);

        fade_time = 4.0f;
        wait_time = fade_time / loop_count;

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

        White.SetActive(false);
    }

    //「タイトルへ」と「他のアプリへ」ボタンのフェーイン
    IEnumerator FadeBtn()
    {

        Image fade1;
        Image fade2 = ToOtherApp.GetComponent<Image>(); //パネルのイメージ取得;

        if (!SaveLoadSystem.Instance.gameData.isOmake)
        {
            //おまけ
            fade1 = ToOmake.GetComponent<Image>(); //パネルのイメージ取得;
            ToOmake.SetActive(true);
        }
        else
        {
            //本編
            fade1 = ToTitle.GetComponent<Image>(); //パネルのイメージ取得;
            ToTitle.SetActive(true);
        }
        ToOtherApp.SetActive(true);

        // フェード後の色を設定 ★変更可
        fade1.color = new Color(1,1,1,0);
        fade2.color = new Color(1,1,1,0);

        // フェードインにかかる時間（秒）★変更可
        float fade_time = 4f;
        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;
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
            Color new_color = fade1.color;
            new_color.a = alpha / 255.0f;
            fade1.color = new_color;
            fade2.color = new_color;
        }

    }

    /// <summary>
    /// 端末ごとで評価依頼を表示する
    /// </summary>
    private void ShowReview()
    {
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
        //StartCoroutine(ShowReviewCoroutine());
#endif
    }


    /// <summary>
    /// Android端末でIn-App Review APIを呼ぶサンプル
    /// </summary>
    //private IEnumerator ShowReviewCoroutine()
    //{
    //    // https://developer.android.com/guide/playcore/in-app-review/unity
    //    var reviewManager = new ReviewManager();
    //    var requestFlowOperation = reviewManager.RequestReviewFlow();
    //    yield return requestFlowOperation;
    //    if (requestFlowOperation.Error != ReviewErrorCode.NoError)
    //    {
    //        // エラーの場合はここで止まる.
    //        yield break;
    //    }
    //    var playReviewInfo = requestFlowOperation.GetResult();
    //    var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
    //    yield return launchFlowOperation;
    //    if (launchFlowOperation.Error != ReviewErrorCode.NoError)
    //    {
    //        // エラーの場合はここで止まる.
    //        yield break;
    //    }
    //}

}