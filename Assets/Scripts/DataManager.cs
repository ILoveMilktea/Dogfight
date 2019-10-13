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
    private int maxExp = 100; // 임시 설정
    private float playtime;

    private DataCenter dataCenter = new DataCenter();
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
        UserData data = dataCenter.GetUserData();

        string toJsonData = JsonUtility.ToJson(data, true);

        File.WriteAllText(dataPath, toJsonData);
        Debug.Log("saved");
    }
    
    public void Load()
    {
        string fromJsonData = File.ReadAllText(dataPath);

        UserData data = JsonUtility.FromJson<UserData>(fromJsonData);
        Debug.Log("loaded");

        dataCenter.SetUserData(data);
    }
    
    // play time
    public void AddPlayTime(float playtime)
    {
        dataCenter.SetPlaytime(dataCenter.GetPlayInfo.playtime + playtime);
    }

    // player Hp
    public void UpMaxHp(int value)
    {
        dataCenter.SetMaxHp(dataCenter.GetPlayerStatus.maxHp + value);
        dataCenter.SetRemainHp(dataCenter.GetPlayerStatus.remainHp + value);
    }
    public void UpRemainHp(int value)
    {
        int remainHp = dataCenter.GetPlayerStatus.remainHp;
        if(remainHp + value > dataCenter.GetPlayerStatus.maxHp)
        {
            dataCenter.SetRemainHp(dataCenter.GetPlayerStatus.maxHp);
        }
        else
        {
            dataCenter.SetRemainHp(remainHp + value);
        }
    }

    public void SetRemainHp(int value)
    {
        if (value > dataCenter.GetPlayerStatus.maxHp)
        {
            dataCenter.SetRemainHp(dataCenter.GetPlayerStatus.maxHp);
        }
        else
        {
            dataCenter.SetRemainHp(value);
        }
    }
    // player Atk
    public void UpAtk(int value)
    {
        dataCenter.SetAtk(dataCenter.GetPlayerStatus.atk + value);
    }
    // Weapon
    public void UpWeaponExp(WeaponType type, int value)
    {
        Weapon weapon = dataCenter.GetWeaponByType(type);
        if(weapon.exp + value >= maxExp)
        {
            dataCenter.SetWeaponLevel(type, weapon.level + 1);
            dataCenter.SetWeaponExp(type, weapon.exp + value - maxExp);
        }
        else
        {
            dataCenter.SetWeaponExp(type, weapon.exp + value);
        }
    }

    // 수치 조정 후 ui 수치 리셋
    public void DisplayReset()
    { }

    //datacenter test
    public void levelup(int val)
    {
        dataCenter.SetStage(val);
        Debug.Log(dataCenter.GetPlayInfo.stage);
    }
 
}
