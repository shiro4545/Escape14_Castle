using UnityEngine;
using System.Runtime.InteropServices;

public class IDFA : MonoBehaviour
{
    public UIManager UI;

#if UNITY_IOS

    //MyObjc.mm で定義しているObjective-C(iOSで使用されている言語)の関数を以下のようにC#側で定義する
    [DllImport("__Internal")]//iOSのプラグイン読み込み 参考　https://docs.unity3d.com/ja/2018.4/Manual/NativePlugins.html
    private static extern void _requestIDFA();//外部で実装されるメソッドを宣言 参考　https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/extern

#endif


    public void Initial()
    {
#if UNITY_IOS
        if (!SaveLoadSystem.Instance.gameData.isATT)
            Invoke("DelayShowATTPanel", 1);//1秒遅らせてDelayIDFA()の呼び出し
#endif

    }

    //ATT説明文表示
    private void DelayShowATTPanel()
    {
        UI.ATTPanel.SetActive(true);
    }


    public void ShowIDFA()
    {
#if UNITY_IOS
        _requestIDFA();//IDFAリクエストの実行
#endif
    }

}
