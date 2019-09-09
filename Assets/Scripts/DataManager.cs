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
        dataPath = Application.persistentDataPath;
#endif
        dataPath = dataPath + "/SaveData.bytes";

    }

    /// <summary>
    /// 현재 게임 플레이 상태 저장
    /// </summary>
    public void Save()
    {
        UserData data = new UserData();
        dataSave.SaveUserData(dataPath, data);
    }
    /// <summary>
    /// 저장된 세이브 데이터 불러오기,
    /// 시작 화면에서 이어서 하기 할 때만 호출됩니다.
    /// </summary>
    public void Load()
    {
        dataLoad.LoadUserData(dataPath);
    }
    /// <summary>
    /// SaveFile 존재하는지 확인하기
    /// </summary>
    /// <returns></returns>
    public bool IsSaveFileExist()
    {
        return File.Exists(dataPath);
    }
    /// <summary>
    /// SaveFile이 없을 경우에, 혹은 새로 시작하는 경우에 GameManager에서 호출됩니다.
    /// </summary>
    public void MakeSaveFile()
    {
        explorerMake.MakeFile();
    }

}
