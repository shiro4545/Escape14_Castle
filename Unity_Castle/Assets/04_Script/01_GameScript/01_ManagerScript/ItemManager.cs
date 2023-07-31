using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    //選択中のアイテム名
    public string SelectItem;
    //ヘッダーに並ぶアイテム画像
    public GameObject[] getItemsArray;
    //アイテムパネル全体
    public GameObject ItemPanel;
    //アイテム拡大画像
    public GameObject ItemImage;

    //特定アイテムでの透明ボタン
    public GameObject BtnBottle;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        foreach (var obj in getItemsArray)
        {
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.SoundSE("TapUIBtn");
                onTapItemImage(obj);
            });
        }

        //アイテム拡大画面でタップする場合
        BtnBottle.GetComponent<Button>().onClick.AddListener(() =>
        {
            TapBottle();
        });
    }

    /// <summary>
    /// アイテムを6こ持ってるか
    /// </summary>
    /// <returns></returns>
    public bool isItemMax()
    {
        bool result = false;
        if (getItemsArray[5].GetComponent<Image>().sprite != null)
            result = true;

        return result;
    }

    //<summary>
    //アイテム取得
    //</summary>
    //<param>アイテム名</param>
    public void GetItem(string itemName)
    {
        AudioManager.Instance.SoundSE("ItemGet");

        //セーブデータ
        //if (itemName == "Key1")
        //    SaveLoadSystem.Instance.gameData.isGetKey1 = true;
        //else if (itemName == "Key2")
        //    SaveLoadSystem.Instance.gameData.isGetKey2 = true;

        SaveLoadSystem.Instance.gameData.getItems += itemName + ";";
        SaveLoadSystem.Instance.Save();

        //多言語対応
        string _ItemName = ItemLocalize(itemName);

        //ヘッダーのアイテム欄に追加
        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if (getItemsArray[i].GetComponent<Image>().sprite == null)
            {
                getItemsArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("01_Item/" + _ItemName);
                getItemsArray[i].SetActive(true);
                break;
            }
        }

        //アイテム選択なしの場合、ゲットしたアイテムを選択
        if (SelectItem == "")
            ChangeSelect(itemName);

    }

    //<summary>
    //取得アイテムのロード
    //</summary>
    //<param>アイテム名</param>
    public void loadItem(string itemName)
    {
        //多言語対応
        string _ItemName = ItemLocalize(itemName);

        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if (getItemsArray[i].GetComponent<Image>().sprite == null)
            {
                getItemsArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("01_Item/" + _ItemName);
                getItemsArray[i].SetActive(true);
                break;
            }
        }
    }

    //<summary>
    //アイテム選択
    //</summary>
    //<param>アイテムオブジェクト</param>
    private void onTapItemImage(GameObject item)
    {
        //選択済みの場合
        if (item.GetComponent<Outline>().enabled)
        {
            ShowItem(item);
            return;
        }

        //未選択の場合
        foreach (var obj in getItemsArray)
        {
            if (item == obj)
            {
                obj.GetComponent<Outline>().enabled = true;
                SelectItem = obj.GetComponent<Image>().sprite.name;
            }
            else
            {
                obj.GetComponent<Outline>().enabled = false;
            }
        }
    }

    //<summary>
    //アイテム拡大画面の表示
    //</summary>
    //<param>アイテムオブジェクト</param>
    private void ShowItem(GameObject item)
    {
        ItemPanel.SetActive(true);
        ItemImage.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
        CameraManager.Instance.ButtonLeft.SetActive(false);
        CameraManager.Instance.ButtonRight.SetActive(false);
        CameraManager.Instance.ButtonBack.SetActive(true);

        //透明ボタンを非表示
        BtnBottle.SetActive(false);

        //透明ボタン表示
        if (SelectItem == "Bottle1")
            BtnBottle.SetActive(true);
         else if (SelectItem == "Can1")
            BtnBottle.SetActive(true);

    }

    //<summary>
    //アイテム使用時
    //</summary>
    //<param>アイテム名</param>
    public void UseItem()
    {
      for(int i = 0; i < getItemsArray.Length; i++)
      {
        if(getItemsArray[i].GetComponent<Image>().sprite.name == SelectItem)
        {
          //枠線を非表示に
          getItemsArray[i].GetComponent<Outline>().enabled = false;

          //持ち物数がMaxの時 最後のアイテムを非表示に
          if(i == getItemsArray.Length - 1)
          {
            getItemsArray[i].GetComponent<Image>().sprite = null;
            getItemsArray[i].SetActive(false);
            break;
          }

          //それ以降のアイテム画像を左に詰める
          for(int j = i + 1; j < getItemsArray.Length; j++)
          {
            if(getItemsArray[j].GetComponent<Image>().sprite == null)
            {
              getItemsArray[j - 1].GetComponent<Image>().sprite = null;
              getItemsArray[j - 1].SetActive(false);
              break;
            }
            else if(j == getItemsArray.Length - 1)
            {
              getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
              getItemsArray[j].GetComponent<Image>().sprite = null;
              getItemsArray[j].SetActive(false);
              break;
            }
            else
            {
              getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
            }
          }
          break;
        }
      }

        //_ja→_lang
        string _itemLang = GetLangName(SelectItem);

      //セーブデータ
      SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace(_itemLang + ";","");

      SelectItem = "";
      SaveLoadSystem.Instance.Save();
    }

    /// <summary>
    /// ヘッダーのアイテム画像を切り替える
    /// </summary>
    /// <param name="_before"></param>
    /// <param name="_after"></param>
    public void ChangeItem(string _before,string _after)
    {
        //多言語対応
        string Before = ItemLocalize(_before);
        string After = ItemLocalize(_after);

        //ヘッダーのアイテム画像を変える
        foreach (var obj in getItemsArray)
        {
            if (obj.GetComponent<Image>().sprite.name == Before)
            {
                //アイテム画像を変える
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("01_Item/" + After);
                //変更するアイテムが選択中の場合、選択名も変更
                if (SelectItem == Before)
                    SelectItem = After;
                break;
            }
        }

        //セーブデータ更新
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace(_before, _after);
        SaveLoadSystem.Instance.Save();
    }

    /// <summary>
    /// ヘッダーから１つアイテムを消す
    /// </summary>
    /// <param name="_name">アイテム名</param>
    public void DeleteItem(string _name)
    {
        //多言語対応
        string Name = ItemLocalize(_name);

        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if (getItemsArray[i].GetComponent<Image>().sprite.name == Name)
            {
                //持ち物数がMaxの時 最後のアイテムを非表示に
                if (i == getItemsArray.Length - 1)
                {
                    getItemsArray[i].GetComponent<Image>().sprite = null;
                    getItemsArray[i].SetActive(false);
                    break;
                }

                //それ以降のアイテム画像を左に詰める
                for (int j = i + 1; j < getItemsArray.Length; j++)
                {
                    if (getItemsArray[j].GetComponent<Image>().sprite == null)
                    {
                        getItemsArray[j - 1].GetComponent<Image>().sprite = null;
                        getItemsArray[j - 1].SetActive(false);
                        break;
                    }
                    else if (j == getItemsArray.Length - 1)
                    {
                        getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
                        getItemsArray[j].GetComponent<Image>().sprite = null;
                        getItemsArray[j].SetActive(false);
                        break;                    }
                    else
                    {
                        getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
                    }
                }
                break;
            }
        }

            SelectItem = "";

        //枠線消去
        foreach (var obj in getItemsArray)
                obj.GetComponent<Outline>().enabled = false;

        //セーブデータ
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace(_name + ";", "");
        SaveLoadSystem.Instance.Save();
    }


    /// <summary>
    /// アイテム拡大画面の変更
    /// </summary>
    /// <param name="_itemName">アイテム名</param>
    private void ChangeBigImage(string _itemName)
    {
        //多言語対応
        string ItemName = ItemLocalize(_itemName);

        ItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("01_Item/" + ItemName);

    }

    /// <summary>
    /// アイテム全消し
    /// </summary>
    public void ItemAllDelete()
    {
        foreach (var obj in getItemsArray)
        {
            obj.GetComponent<Outline>().enabled = false;
            obj.GetComponent<Image>().sprite = null;
            obj.SetActive(false);
        }
        SelectItem = "";
    }

    /// <summary>
    /// アイテム選択の変更
    /// </summary>
    public void ChangeSelect(string _itemName)
    {
        //多言語対応
        string ItemName = ItemLocalize(_itemName);

        SelectItem = ItemName;
        foreach (var obj in getItemsArray)
        {
            if (obj.GetComponent<Image>().sprite == null)
                continue;

            if(obj.GetComponent<Image>().sprite.name == SelectItem)
                obj.GetComponent<Outline>().enabled = true;
            else
                obj.GetComponent<Outline>().enabled = false;
        }
    }

    /// <summary>
    /// アイテム名の多言語対応
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public string ItemLocalize(string itemName)
    {
        string returnName;
        //多言語対応
        if (itemName.IndexOf("_lang") == -1)
            returnName = itemName;
        else
        {
            if (LocalizeManager.Instance.Lang == SystemLanguage.Japanese)
                returnName = itemName.Replace("_lang", "_ja");
            else if (LocalizeManager.Instance.Lang == SystemLanguage.Chinese || LocalizeManager.Instance.Lang == SystemLanguage.ChineseSimplified)
                returnName = itemName.Replace("_lang", "_ch");
            else if (LocalizeManager.Instance.Lang == SystemLanguage.Spanish)
                returnName = itemName.Replace("_lang", "_sp");
            else if (LocalizeManager.Instance.Lang == SystemLanguage.Korean)
                returnName = itemName.Replace("_lang", "_ko");
            else
                returnName = itemName.Replace("_lang", "_en");
        }

        return returnName;
    }

    /// <summary>
    /// _lang形式のアイテム名を取得
    /// </summary>
    /// <param name="_itemName"></param>
    /// <returns></returns>
    private string GetLangName(string _itemName)
    {
        string ItemName = _itemName.Replace("_ja", "_lang"); ;
        ItemName = ItemName.Replace("_en", "_lang");
        ItemName = ItemName.Replace("_ch", "_lang");
        ItemName = ItemName.Replace("_sp", "_lang");
        ItemName = ItemName.Replace("_ko", "_lang");
        return ItemName;
    }



    //*************************************************************************
    //アプリ固有メソッド
    //*************************************************************************

    /// <summary>
    /// 
    /// </summary>
    private void TapBottle()
    {
        BtnBottle.SetActive(false);

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("PETCap");
        ChangeBigImage("Bottle2");



        //SaveLoadSystem.Instance.gameData.isClearBottle = true;
        SaveLoadSystem.Instance.Save();
    }
}
