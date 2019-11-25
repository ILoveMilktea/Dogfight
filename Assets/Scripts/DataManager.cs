﻿using System;
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
        dataLoad = new DataLoad();
    }

    public PlayInfo GetPlayInfo { get { return dataCenter.playInfo; } }
    public PlayerStatusInfo GetPlayerStatus { get { return dataCenter.playerStatusInfo; } }
    public Dictionary<WeaponType, WeaponInfo> GetWeapons { get { return dataCenter.weapons; } }

    public void SetPlayInfo(PlayInfo info)
    {
        SetDungeonName(info.CurDungeon);
        SetPlayTime(info.Playtime);
        SetStage(info.Stage);
        SetParts(info.Parts);
        SetAlreadyAct(info.AlreadyAct);
    }
    public void SetDungeonName(string curDungeon)
    {
        GetPlayInfo.SetCurDungeon(curDungeon);
    }
    public void SetPlayTime(float playtime)
    {
        GetPlayInfo.SetPlaytime(playtime);
    }
    public void AddPlayTime(float playtime)
    {
        SetPlayTime(GetPlayInfo.Playtime + playtime);
    }
    public void SetStage(int stage)
    {
        GetPlayInfo.SetStage(stage);
    }
    public void AddStage(int value)
    {
        SetStage(GetPlayInfo.Stage + value);
    }
    public void SetParts(int parts)
    {
        GetPlayInfo.SetParts(parts);
    }
    public void AddParts(int parts)
    {
        SetParts(GetPlayInfo.Parts + parts);
    }
    public void SetAlreadyAct(bool value)
    {
        GetPlayInfo.SetAlreadyAct(value);
    }

    public void SetPlayerStatusInfo(PlayerStatusInfo info)
    {
        SetMaxHp(info.MaxHp);
        SetRemainHp(info.RemainHp);
        SetAtk(info.Atk);
        SetBuffHp(info.BuffHp);
        SetBuffAtk(info.BuffAtk);
    }
    public void SetMaxHp(int value)
    {
        GetPlayerStatus.SetMaxHp(value);
    }
    public void AddMaxHp(int value)
    {
        SetMaxHp(GetPlayerStatus.MaxHp + value);
    }
    public void SetRemainHp(int value)
    {
        if (value > dataCenter.playerStatusInfo.MaxHp)
        {
            GetPlayerStatus.SetRemainHp(GetPlayerStatus.MaxHp);
        }
        else
        {
            GetPlayerStatus.SetRemainHp(value);
        }
    }
    public void AddRemainHp(int value)
    {
        SetRemainHp(GetPlayerStatus.RemainHp + value);
    }
    public void SetAtk(int value)
    {
        GetPlayerStatus.SetAtk(value);
    }
    public void AddAtk(int value)
    {
        SetAtk(GetPlayerStatus.Atk + value);
    }
    public void SetBuffHp(int value)
    {
        GetPlayerStatus.SetBuffHp(value);
    }
    public void AddBuffHp(int value)
    {
        SetBuffHp(GetPlayerStatus.BuffHp + value);
    }
    public void SetBuffAtk(int value)
    {
        GetPlayerStatus.SetBuffAtk(value);
    }
    public void AddBuffAtk(int value)
    {
        SetBuffAtk(GetPlayerStatus.BuffAtk + value);
    }

    public void SetWeapons(Dictionary<WeaponType, WeaponInfo> weapons)
    {
        dataCenter.SetWeapons(weapons);
    }
    public void AddWeapon(WeaponType type, WeaponInfo info)
    {
        GetWeapons.Add(type, info);
    }
    public void SetSkillTree(WeaponType type, Dictionary<int,WeaponSkill> tree)
    {
        GetWeapons[type].SetSkillTree(tree);
    }
    
    public bool CheckSaveData()
    {
        if(File.Exists(dataPath+Const_Path.playInfoPath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveSaveData()
    {
        File.Delete(dataPath + Const_Path.playInfoPath);
        File.Delete(dataPath + Const_Path.playerStatusInfoPath);
        File.Delete(dataPath + Const_Path.WeaponInfoPath);
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

