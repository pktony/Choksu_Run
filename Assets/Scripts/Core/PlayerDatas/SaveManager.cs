using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    string saveFileName = "PlayerData.json";

    public void SaveDatas(PlayerDatas datas)
    {
        string ToJsonData = JsonUtility.ToJson(datas);
        string filePath = $"{Application.persistentDataPath}/{saveFileName}";

        File.WriteAllText(filePath, ToJsonData);
    }

    public bool LoadDatas(out PlayerDatas datas)
    {
        bool result = false;
        string filePath = $"{Application.persistentDataPath}/{saveFileName}";

        if (File.Exists(filePath))
        {
            string fromJson = File.ReadAllText(filePath);
            print("Load Complete !");
            datas = JsonUtility.FromJson<PlayerDatas>(fromJson);
            result = true;
        }
        else
        {
            datas = null;
            print("Load Failed : No Such Directory Found");
        }

        return result;
    }
}
