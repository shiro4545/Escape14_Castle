using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensu_Judge : MonoBehaviour
{
    public string Status = "0000";

    public void Judge()
    {
        if (!SaveLoadSystem.Instance.gameData.isClearSensuBtn)
        {
            if (Status == "1011")
            {
                SaveLoadSystem.Instance.gameData.isClearSensu1 = true;
                SaveLoadSystem.Instance.Save();
            }
        }
        else if(SaveLoadSystem.Instance.gameData.isClearMakimono2)
        {
            if (Status == "0101")
            {
                SaveLoadSystem.Instance.gameData.isClearSensu2 = true;
                SaveLoadSystem.Instance.Save();
            }
        }
    }
}
