using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPanel : MonoBehaviour
{
    public GameObject Panel;
    public GameObject LightIcon;

    public static BlockPanel Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
      Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //画面ブロックを表示(ひらめき有り)
    public void ShowBlock()
    {
        Panel.SetActive(true);
        LightIcon.SetActive(true);
    }


    //画面ブロックを表示(ひらめき無し)
    public void ShowBlockNoIcon()
    {
        Panel.SetActive(true);
    }

    //画面ブロックを非表表示
    public void HideBlock()
    {
        Panel.SetActive(false);
        LightIcon.SetActive(false);
    }
}
