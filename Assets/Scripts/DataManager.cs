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

    private DataCenter dataCenter;

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


        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        dataPath += "/SaveData.json";
        //dataPath = dataPath + "/SaveData.json";

        dataCenter = new DataCenter();
    }

    public bool CheckSaveData()
    {
        if (File.Exists(dataPath))
        {
            return IsDataExist();
        }
        else
        {
            File.Create(dataPath);
            Save();
        }

        return false;
    }

    private bool IsDataExist()
    {
        string fromJsonData = File.ReadAllText(dataPath);

        UserData data = JsonUtility.FromJson<UserData>(fromJsonData);
        if(data.playInfo.playtime != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Save()
    {
        UserData data = dataCenter.GetUserData();

        string toJsonData = JsonUtility.ToJson(data, true);

        File.WriteAllText(dataPath, toJsonData);
        Debug.Log("saved");

        if(SceneManager.GetActiveScene().name == "Upgrade")
        {
            FindObjectOfType<PlayerStatusWindow>().RedrawWindow();
        }
    }
    
    public void Load()
    {
        string fromJsonData = File.ReadAllText(dataPath);

        UserData data = JsonUtility.FromJson<UserData>(fromJsonData);
        Debug.Log("loaded");

        dataCenter.SetUserData(data);
    }
    
    public PlayInfo GetPlayInfo { get { return dataCenter.playInfo; } }
    public PlayerStatusInfo GetPlayerStatus { get { return dataCenter.playerStatusInfo; } }
    public Dictionary<WeaponType, Weapon> GetWeapons { get { return dataCenter.weapons; } }

    public void SetDungeonName(string name)
    {
        dataCenter.SetCurDungeon(name);
    }
    // play time
    public void AddPlayTime(float playtime)
    {
        dataCenter.SetPlaytime(dataCenter.playInfo.playtime + playtime);
    }
    // parts
    public void AddGainParts(int parts)
    {
        dataCenter.SetParts(dataCenter.playInfo.parts + parts);
    }
    // is acted??
    public void SetIsAct(bool value)
    {
        dataCenter.SetIsAct(value);
    }
    // player Hp
    public void UpMaxHp(int value)
    {
        dataCenter.SetMaxHp(dataCenter.playerStatusInfo.maxHp + value);
        dataCenter.SetRemainHp(dataCenter.playerStatusInfo.remainHp + value);
    }
    public void UpRemainHp(int value)
    {
        int remainHp = dataCenter.playerStatusInfo.remainHp;
        if(remainHp + value > dataCenter.playerStatusInfo.maxHp)
        {
            dataCenter.SetRemainHp(dataCenter.playerStatusInfo.maxHp);
        }
        else
        {
            dataCenter.SetRemainHp(remainHp + value);
        }
    }

    public void SetRemainHp(int value)
    {
        if (value > dataCenter.playerStatusInfo.maxHp)
        {
            dataCenter.SetRemainHp(dataCenter.playerStatusInfo.maxHp);
        }
        else
        {
            dataCenter.SetRemainHp(value);
        }
    }
    // player Atk
    public void UpAtk(int value)
    {
        dataCenter.SetAtk(dataCenter.playerStatusInfo.atk + value);
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

    public void UpStage()
    {
        dataCenter.SetStage(dataCenter.playInfo.stage + 1);
    }
    // 수치 조정 후 ui 수치 리셋
    public void DisplayReset()
    { }

    //datacenter test
    public void levelup(int val)
    {
        dataCenter.SetStage(val);
    }


    public void DamageToTarget(GameObject source, GameObject target)
    {
        //int damage = dataCenter.GetAttackResult(source, target);
        //fightSceneHandler.DamageToCharacter(source, damage);
    }
}
