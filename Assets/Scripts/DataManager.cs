using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoSingleton<DataManager>
{
    private string dataPath;

    private DataCenter dataCenter;
    private DataSave dataSave = new DataSave();
    private DataLoad dataLoad = new DataLoad();
    private ExplorerMake explorerMake = new ExplorerMake();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
#if UNITY_IPHONE
        dataPath = Application.dataPath + "/Raw";
#endif

#if UNITY_ANDROID
        dataPath = "jar:file://" + Application.dataPath + "!/assets";
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
        dataPath = Application.dataPath + "/SaveData";
#endif
        //dataPath = dataPath + "/SaveData.bytes";
        dataPath = MakeNewStorage(dataPath);

        dataCenter = FindObjectOfType<DataCenter>();
    }

    private string MakeNewStorage(string dataPath)
    {
        explorerMake.MakeNewDirectory(dataPath);
        dataPath = dataPath + "/SaveData.json";
        explorerMake.MakeNewFile(dataPath);
        return dataPath;
    }
    
    public void Save()
    {
        UserData data = dataCenter.GetUserData;
        dataSave.SaveUserData(dataPath, data);
    }
    
    public void Load()
    {
        dataLoad.LoadUserData(dataPath);
    }
    

    public void SetUserDataToCenter(string jsonData)
    {
        dataCenter.SetLoadData(jsonData);
    }

    //datacenter test
    public void levelup(int val)
    {
        dataCenter.SetStage(val);
        Debug.Log(dataCenter.GetPlayInfo.stage);
    }

    public void testt()
    {

    }
}
