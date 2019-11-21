﻿//using System;
//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum WeaponType
//{
//    EnergySphereGun,
//    LinearGun,
//    ShotGun
//}
//public class DataCenter 
//{
//    private UserData userData;

//    public PlayInfo playInfo { get; private set; }
//    public PlayerStatusInfo playerStatusInfo { get; private set; }
//    public Dictionary<WeaponType, Weapon> weapons { get; private set; }
//    public Dictionary<WeaponType, Dictionary<int, WeaponSkill>> skillTrees { get; private set; }


//    public void SetCurDungeon(string value) { playInfo.curDungeon = value; }
//    public void SetPlaytime(float value) { playInfo.playtime = value; }
//    public void SetStage(int value) { playInfo.stage = value; }
//    public void SetParts(int value) { playInfo.parts = value; }
//    public void SetIsAct(bool value) { playInfo.isAct = value; }

//    public void SetMaxHp(int value) { playerStatusInfo.maxHp = value; }
//    public void SetRemainHp(int value) { playerStatusInfo.remainHp = value; }
//    public void SetAtk(int value) { playerStatusInfo.atk = value; }

//    public Weapon GetWeaponByType(WeaponType type) { return weapons[type]; }
//    public void SetWeaponSkillTree(WeaponType type, List<WeaponSkill> value) { weapons[type].skillTree = value; }
//    public void SetIsActivated(WeaponType type, int key, bool value) { skillTrees[type][key].isActivated = value; }
//    public void SetUsedParts(WeaponType type, int key, int value) { skillTrees[type][key].usedParts = value; }

//    public DataCenter()
//    {
//        CreateCenter();
//    }

//    private void CreateCenter()
//    {
//        playInfo = new PlayInfo();
//        playerStatusInfo = new PlayerStatusInfo();

//        weapons = new Dictionary<WeaponType, Weapon>();
//        skillTrees = new Dictionary<WeaponType, Dictionary<int, WeaponSkill>>();

//        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
//        {
//            Dictionary<int, WeaponSkill> tree = new Dictionary<int, WeaponSkill>();
//            List<WeaponSkill> skillTree = new List<WeaponSkill>();
//            Weapon weapon = new Weapon(type, skillTree);
//            weapons.Add(type, weapon);
//        }

//        WeaponInfo weaponInfo = new WeaponInfo(weapons.Values.ToList());
//        userData = new UserData(playInfo, playerStatusInfo, weaponInfo);
//    }

//    public UserData GetUserData()
//    {
//        WeaponInfo weaponsInfo = new WeaponInfo(weapons.Values.ToList());

//        userData.playInfo = playInfo;
//        userData.playerStatusInfo = playerStatusInfo;
//        userData.weaponInfo = weaponsInfo;

//        return userData;
//    }

//    public void SetUserData(UserData data)
//    {
//        playInfo = data.playInfo;
//        playerStatusInfo = data.playerStatusInfo;

//        foreach(var weapon in data.weaponInfo.weaponsList)
//        { 
//            WeaponType type = (WeaponType)Enum.Parse(typeof(WeaponType), weapon.name);
//            weapons[type] = weapon;
//        }

//        userData = data;
//    }

//}

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    EnergySphereGun,
    LinearGun,
    ShotGun
}
public class DataCenter
{
    public PlayInfo playInfo { get; private set; }
    public PlayerStatusInfo playerStatusInfo { get; private set; }
    public Dictionary<WeaponType, WeaponInfo> weapons { get; private set; }


    //public void SetCurDungeon(string value) { playInfo.curDungeon = value; }
    //public void SetPlaytime(float value) { playInfo.playtime = value; }
    //public void SetStage(int value) { playInfo.stage = value; }
    //public void SetParts(int value) { playInfo.parts = value; }
    //public void SetIsAct(bool value) { playInfo.isAct = value; }

    //public void SetMaxHp(int value) { playerStatusInfo.maxHp = value; }
    //public void SetRemainHp(int value) { playerStatusInfo.remainHp = value; }
    //public void SetAtk(int value) { playerStatusInfo.atk = value; }

    //public Weapon GetWeaponByType(WeaponType type) { return weapons[type]; }
    //public void SetWeaponSkillTree(WeaponType type, List<WeaponSkill> value) { weapons[type].skillTree = value; }
    //public void SetIsActivated(WeaponType type, int key, bool value) { skillTrees[type][key].isActivated = value; }
    //public void SetUsedParts(WeaponType type, int key, int value) { skillTrees[type][key].usedParts = value; }

    public DataCenter()
    {
        CreateCenter();
    }
    private void CreateCenter()
    {
        playInfo = new PlayInfo();
        playerStatusInfo = new PlayerStatusInfo();

        weapons = new Dictionary<WeaponType, WeaponInfo>();
        
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            WeaponInfo info = new WeaponInfo();

            weapons.Add(type, info);
        }
    }

}