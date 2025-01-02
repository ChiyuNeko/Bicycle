using Meta.WitAi.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	//放入要儲存的SaveData
	public virtual void Save(object save, string fileName)
	{
		// 設定檔案名稱
		//string fileName = string.Format("{0}_GameData", ID);
		// 將資料序列化為 JSON 格式
		string savingString = JsonUtility.ToJson(save);
		// 檢查資料夾是否存在，不存在則創建
		if (System.IO.Directory.Exists(Application.dataPath + "/StreamingAssets/GameData/") == false)
		{
			System.IO.Directory.CreateDirectory(Application.dataPath + "/StreamingAssets/GameData/");
		}
		// 將資料寫入到指定路徑的 JSON 檔案中
		System.IO.File.WriteAllText(Application.dataPath + "/StreamingAssets/GameData/" + fileName, savingString);
	}
}
