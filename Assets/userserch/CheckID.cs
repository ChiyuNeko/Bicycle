using Meta.WitAi.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckID : SaveSystem
{
    public static string playerID;
    public static float bestScore = 0;
    public static float bestDis = 0;
    //確認是否有建檔以及讀取最高紀錄
    public static bool checkID()
    {
        string fileName = string.Format("{0}_GameData", playerID);
        if (System.IO.File.Exists(Application.dataPath + "/StreamingAssets/GameData/" + fileName)) 
        {
            string readData = System.IO.File.ReadAllText(Application.dataPath + "/StreamingAssets/GameData/" + fileName);
            bestScore = JsonConvert.DeserializeObject<GameController.SaveData>(readData).score;
            bestDis = JsonConvert.DeserializeObject<GameController.SaveData>(readData).moveDis;
            return true;
        }
        else
        {
            bestScore = 0;
            bestDis = 0;
            return false;
        }
    }
}
