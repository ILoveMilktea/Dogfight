using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public PlayerStatus playerStatus;
    public Weapons weapons;
    public PlayInfo playInfo;
}
public class PlayerStatus
{
    public int maxHp;
    public int remainHp;
    public int atk;
    public int def;
}

public class Weapons
{
    public WeaponStatus wa;
    public WeaponStatus wb;
    public WeaponStatus wc;
}

public class WeaponStatus
{
    public int level;
    public int exp;
}

public class PlayInfo
{
    public float playtime;
    public int stage;
    public int energy;
}
