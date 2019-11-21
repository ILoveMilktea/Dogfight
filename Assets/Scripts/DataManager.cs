using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Const_Path
{
    public const string playInfoPath = "/PlayInfo.byte";
    public const string playerStatusInfoPath = "/PlayerStatusInfo.byte";
    public const string WeaponInfoPath = "/WeaponInfo.byte";
}

public class DataManager : MonoSingleton<DataManager>
{
    private string dataPath;
    private int maxExp = 100; // 임시 설정
    private float playtime;

    private DataCenter dataCenter;
    private DataSave dataSave;
    private DataLoad dataLoad;

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

        //dataPath += "/SaveData.json";

        dataCenter = new DataCenter();
        dataSave = new DataSave();
    }

    public PlayInfo GetPlayInfo { get { return dataCenter.playInfo; } }
    public PlayerStatusInfo GetPlayerStatus { get { return dataCenter.playerStatusInfo; } }
    public Dictionary<WeaponType, WeaponInfo> GetWeapons { get { return dataCenter.weapons; } }

    public void SetDungeonName(string name)
    {
        GetPlayInfo.SetCurDungeon(name);
    }
    public void SetPlayTime(float playtime)
    {
        GetPlayInfo.SetPlaytime(playtime);
    }
    public void SetStage(int stage)
    {
        GetPlayInfo.SetStage(stage);
    }
    public void SetGainParts(int parts)
    {
        GetPlayInfo.SetParts(parts);
    }
    public void SetAlreadyAct(bool value)
    {
        GetPlayInfo.SetAlreadyAct(value);
    }
    

    public void SetMaxHp(int value)
    {
        GetPlayerStatus.SetMaxHp(dataCenter.playerStatusInfo.MaxHp + value);
    }
    public void SetRemainHp(int value)
    {
        if (value > dataCenter.playerStatusInfo.MaxHp)
        {
            GetPlayerStatus.SetRemainHp(dataCenter.playerStatusInfo.MaxHp);
        }
        else
        {
            GetPlayerStatus.SetRemainHp(value);
        }
    }
    public void SetAtk(int value)
    {
        GetPlayerStatus.SetAtk(value);
    }
    public void SetBuffHp(int value)
    {
        GetPlayerStatus.SetBuffHp(value);
    }
    public void SetBuffAtk(int value)
    {
        GetPlayerStatus.SetBuffAtk(value);
    }
    
    public void AddWeapon(WeaponType type, WeaponInfo info)
    {
        GetWeapons.Add(type, info);
    }
    public void SetSkillNode(WeaponType type, Dictionary<int,WeaponSkill> tree)
    {
        GetWeapons[type].SetSkillTree(tree);
    }


    public bool CheckSaveData()
    {
        return true;
    }

    public void Save()
    {
        dataSave.SaveUserData(dataPath);

        if (SceneManager.GetActiveScene().name == "Upgrade")
        {
            FindObjectOfType<PlayerStatusWindow>().RedrawWindow();
        }
    }

    public void Load()
    {
        dataLoad.LoadUserData(dataPath);
    }



    public void DamageToTarget(GameObject source, GameObject target)
    {
        //int damage = dataCenter.GetAttackResult(source, target);
        //fightSceneHandler.DamageToCharacter(source, damage);
    }
}

