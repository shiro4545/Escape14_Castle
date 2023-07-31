using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]
public class HintData
{
    public Ja ja ; //日本語
    public En en ; //英語
    public ZH_CN zh_CN ; //中国語(簡体字)
    public Es es ; //スペイン語
    public Ko ko; //韓国語
}

[System.Serializable]
public class Ja
{
    public string[] hint1; //ヒント1
    public string[] hint2; //ヒント2
    public string[] hint3; //ヒント3
    public string[] hint4; //ヒント4
}
[System.Serializable]
public class En
{
    public string[] hint1; //ヒント1
    public string[] hint2; //ヒント2
    public string[] hint3; //ヒント3
    public string[] hint4; //ヒント4
}
[System.Serializable]
public class ZH_CN
{
    public string[] hint1; //ヒント1
    public string[] hint2; //ヒント2
    public string[] hint3; //ヒント3
    public string[] hint4; //ヒント4
}
[System.Serializable]
public class Es
{
    public string[] hint1; //ヒント1
    public string[] hint2; //ヒント2
    public string[] hint3; //ヒント3
    public string[] hint4; //ヒント4
}
[System.Serializable]
public class Ko
{
    public string[] hint1; //ヒント1
    public string[] hint2; //ヒント2
    public string[] hint3; //ヒント3
    public string[] hint4; //ヒント4
}

public class HintLoad : MonoBehaviour
{
    #region Singleton
    private static HintLoad instance = new HintLoad();
    public static HintLoad Instance => instance;
    #endregion
    private HintLoad() { }


    public HintData hintData = new HintData();

    /// <summary>
    /// Jsonファイルの読み込み
    /// </summary>
    public void Load()
    {
        string loadjson = Resources.Load<TextAsset>("04_Hint/HintData").ToString();
        JsonUtility.FromJsonOverwrite(loadjson, hintData);
    }

}



