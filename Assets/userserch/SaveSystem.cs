using Meta.WitAi.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    //��J�n�x�s��SaveData
    public virtual void Save(object save, string fileName)
    {
        // �N��ƧǦC�Ƭ� JSON �榡
        string savingString = JsonUtility.ToJson(save);

        // �ھڳ]�ƧP�_�x�s���|
        string directoryPath;

#if UNITY_ANDROID && !UNITY_EDITOR
    // Android �]�ơG�s��b Download/Butterfly ��Ƨ�
    directoryPath = System.IO.Path.Combine(Application.persistentDataPath.Replace("files", "Download"), "Butterfly");
#else
        // �q���]�ơG�s��b StreamingAssets/GameData ��Ƨ�
        directoryPath = Application.dataPath + "/StreamingAssets/GameData";
#endif

        // �ˬd��Ƨ��O�_�s�b�A���s�b�h�Ы�
        if (!System.IO.Directory.Exists(directoryPath))
        {
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        // �]�w�����ɮ׸��|
        string filePath = System.IO.Path.Combine(directoryPath, fileName);

        // �N��Ƽg�J����w���|�� JSON �ɮפ�
        System.IO.File.WriteAllText(filePath, savingString);

        // Debug �T�{
        Debug.Log("�x�s�����A���|�G" + filePath);
    }
}
