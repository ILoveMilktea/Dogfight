using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoad
{

    public void LoadUserData(string dataPath)
    {
        string fromJsonData = File.ReadAllText(dataPath);

        UserData data = JsonUtility.FromJson<UserData>(fromJsonData);
        Debug.Log("loaded");
    }
    
}
