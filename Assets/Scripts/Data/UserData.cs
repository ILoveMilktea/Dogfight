//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UserData
//{
//    public PlayInfo playInfo;
//    public PlayerStatusInfo playerStatusInfo;
//    public WeaponInfo weaponInfo;

//    public UserData(PlayInfo p1, PlayerStatusInfo p2, WeaponInfo p3)
//    {
//        playInfo = p1;
//        playerStatusInfo = p2;
//        weaponInfo = p3;
//    }
//}
//[Serializable]
//public class PlayInfo
//{
//    public string curDungeon;
//    public float playtime;
//    public int stage;
//    public int parts;
//    public bool isAct;

//    public PlayInfo()
//    {
//        playtime = 0;
//        stage = 1;
//        parts = 0;
//        isAct = false;
//    }
//}
//[Serializable]
//public class PlayerStatusInfo
//{
//    public int maxHp;
//    public int remainHp;
//    public int atk;

//    public PlayerStatusInfo()
//    {
//        maxHp = 100;
//        remainHp = 100;
//        atk = 10;
//    }
//}

//[Serializable]
//public class WeaponInfo
//{
//    public List<Weapon> weaponsList;

//    public WeaponInfo(List<Weapon> weapons)
//    {
//        weaponsList = weapons;
//    }
//}

//[Serializable]
//public class Weapon
//{
//    public string name;
//    public List<WeaponSkill> skillTree;

//    public Weapon(WeaponType type, List<WeaponSkill> tree)
//    {
//        name = type.ToString();
//        skillTree = tree;
//    }
//}

//[Serializable]
//public class WeaponSkill
//{
//    public int key;
//    public bool isActivated;
//    public int usedParts;

//    public WeaponSkill(int p_key, bool p_isActivated, int p_usedParts)
//    {
//        key = p_key;
//        isActivated = p_isActivated;
//        usedParts = p_usedParts;
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInfo
{
    public string CurDungeon { get; private set; }
    public float Playtime { get; private set; }
    public int Stage { get; private set; }
    public int Parts { get; private set; }
    public bool AlreadyAct { get; private set; }

    public PlayInfo()
    {
        CurDungeon = "";
        Playtime = 0;
        Stage = 1;
        Parts = 0;
        AlreadyAct = false;
    }

    public void SetCurDungeon(string value) { CurDungeon = value; }
    public void SetPlaytime(float value) { Playtime = value; }
    public void SetStage(int value) { Stage = value; }
    public void SetParts(int value) { Parts = value; }
    public void SetAlreadyAct(bool value) { AlreadyAct = value; }

}

public class PlayerStatusInfo
{
    public int MaxHp { get; private set; }
    public int RemainHp { get; private set; }
    public int Atk { get; private set; }
    public int BuffHp { get; private set; }
    public int BuffAtk { get; private set; }

    public PlayerStatusInfo()
    {
        MaxHp = 100;
        RemainHp = 100;
        Atk = 10;
        BuffHp = 0;
        BuffAtk = 0;
    }

    public void SetMaxHp(int value) { MaxHp = value; }
    public void SetRemainHp(int value) { RemainHp = value; }
    public void SetAtk(int value) { Atk = value; }
    public void SetBuffHp(int value) { BuffHp = value; }
    public void SetBuffAtk(int value) { BuffAtk = value; }
}

public class WeaponInfo
{
    public string Name { get; private set; }
    public Dictionary<int, WeaponSkill> SkillTree { get; private set; }

    public WeaponInfo()
    {
        Name = "";
        SkillTree = new Dictionary<int, WeaponSkill>();
    }

    public void SetName(WeaponType value) { Name = value.ToString(); }
    public void SetSkillTree(Dictionary<int, WeaponSkill> value) { SkillTree = value; }

}

public class WeaponSkill
{
    public int ParentKey { get; private set; }
    public bool IsActivated { get; private set; }
    public int UsedParts { get; private set; }

    public WeaponSkill()
    {
        ParentKey = 0;
        IsActivated = false;
        UsedParts = 0;
    }

    public void SetParentKey(int value) { ParentKey = value; }
    public void SetIsActivated(bool value) { IsActivated = value; }
    public void SetUsedParts(int value) { UsedParts = value; }
}