using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeManager : MonoBehaviour
{
    /// <summary>
    /// 言語設定
    /// 0:本番(端末言語),1:日本語,2:英語,3:中国語,4スペイン語
    /// </summary>
    public int LanguageNo; 

    public static LocalizeManager Instance { get; private set; }

    public SystemLanguage Lang { get; private set; }

    public UIManager UIClass;

    public GameObject[] Objects_ja;
    public GameObject[] Objects_en;
    public GameObject[] Objects_ch;
    public GameObject[] Objects_ko;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //端末の言語設定を取得
        if (LanguageNo == 0)
            Lang = Application.systemLanguage;
        else if (LanguageNo == 1)
            Lang = SystemLanguage.Japanese;
        else if (LanguageNo == 2)
            Lang = SystemLanguage.English;
        else if (LanguageNo == 3)
            Lang = SystemLanguage.Chinese;
        else if (LanguageNo == 4)
            Lang = SystemLanguage.Korean;
        //else if (LanguageNo == 5)
        //    Lang = SystemLanguage.Spanish;
        else
            Lang = SystemLanguage.English;


        //UI画像の変動
        UIClass.ChangeUILang();


        //オブジェクトをリセット
        foreach (var obj in Objects_ja)
            obj.SetActive(false);
        foreach (var obj in Objects_en)
            obj.SetActive(false);
        foreach (var obj in Objects_ch)
            obj.SetActive(false);
        //foreach (var obj in Objects_sp)
        //    obj.SetActive(false);
        foreach (var obj in Objects_ko)
            obj.SetActive(false);

        //各言語用のオブジェクト表示
        if (Lang == SystemLanguage.Japanese)
        {
            foreach (var obj in Objects_ja)
                obj.SetActive(true);
        }
        else if(Lang == SystemLanguage.Chinese || Lang == SystemLanguage.ChineseSimplified)
        {
            foreach (var obj in Objects_ch)
                obj.SetActive(true);
        }
        //else if(Lang == SystemLanguage.Spanish)
        //{
        //    foreach (var obj in Objects_sp)
        //        obj.SetActive(true);
        //}
        else if (Lang == SystemLanguage.Korean)
        {
            foreach (var obj in Objects_ko)
                obj.SetActive(true);
        }
        else
        {
            foreach (var obj in Objects_en)
                obj.SetActive(true);
        }
    }

}
