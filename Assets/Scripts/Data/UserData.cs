using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public PlayInfo playInfo;
    public PlayerStatusInfo playerStatusInfo;
    public WeaponInfo weaponInfo;

    public UserData(PlayInfo p1, PlayerStatusInfo p2, WeaponInfo p3)
    {
        playInfo = p1;
        playerStatusInfo = p2;
        weaponInfo = p3;
    }
}
[Serializable]
public class PlayInfo
{
    public float playtime;
    public int stage;
    public int energy;

    public PlayInfo()
    {
        playtime = 0;
        stage = 1;
        energy = 0;
    }
}
[Serializable]
public class PlayerStatusInfo
{
    public int maxHp;
    public int remainHp;
    public int atk;

    public PlayerStatusInfo()
    {
        maxHp = 100;
        remainHp = 100;
        atk = 10;
    }
}

[Serializable]
public class WeaponInfo
{
    public List<Weapon> weaponsList;

    public WeaponInfo(List<Weapon> weapons)
    {
        weaponsList = weapons;
    }
}

[Serializable]
public class Weapon
{
    public string name;
    public int level;
    public int exp;

    public Weapon(WeaponType type)
    {
        name = type.ToString();
        level = 1;
        exp = 0;
    }
}