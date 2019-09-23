using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{

    /// <summary>
    /// 게임 실행중에 현재 파일을 저장할 때에 호출합니다.
    /// </summary>
    /// <param name="slotName"></param>
    public void SaveUserData(string dataPath, UserData userData)
    {
        string toJsonData = JsonUtility.ToJson(userData);
        //string toJsonData = JsonHelper.ToJson(userData, prettyPrint: true);

        File.WriteAllText(dataPath, toJsonData);
        Debug.Log("saved");
        Debug.Log(toJsonData);
    }
    
}
