using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    gun,
    bow,
    nuclear
}
public class DataCenter : MonoSingleton<DataCenter>
{
    private UserData userData;
    private PlayInfo playInfo;
    private PlayerStatusInfo playerStatusInfo;
    private Dictionary<WeaponType, Weapon> weapons;


    public PlayInfo GetPlayInfo { get { return playInfo; } }
    public void SetPlaytime(float value) { playInfo.playtime = value; }
    public void SetStage(int value) { playInfo.stage = value; }
    public void SetEnergy(int value) { playInfo.energy = value; }

    public PlayerStatusInfo GetPlayerStatus { get { return playerStatusInfo; } }
    public void SetMaxHp(int value) { playerStatusInfo.maxHp = value; }
    public void SetRemainHp(int value) { playerStatusInfo.remainHp = value; }
    public void SetAtk(int value) { playerStatusInfo.atk = value; }
    public void SetDef(int value) { playerStatusInfo.def = value; }


    public Dictionary<WeaponType, Weapon> GetWeapons { get { return weapons; } }
    public Weapon GetWeaponByType(WeaponType type) { return weapons[type]; }
    public void SetWeaponLevel(WeaponType type, int value) { weapons[type].level = value; }
    public void SetWeaponExp(WeaponType type, int value) { weapons[type].exp = value; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CreateCenter();
    }

    private void CreateCenter()
    {
        playInfo = new PlayInfo();
        playerStatusInfo = new PlayerStatusInfo();

        weapons = new Dictionary<WeaponType, Weapon>();
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            Weapon weapon = new Weapon(type);
            weapons.Add(type, weapon);
        }

        WeaponInfo weaponInfo = new WeaponInfo(weapons.Values.ToList());
        userData = new UserData(playInfo, playerStatusInfo, weaponInfo);
    }

    public UserData GetUserData()
    {
        WeaponInfo weaponsInfo = new WeaponInfo(weapons.Values.ToList());

        userData.playInfo = playInfo;
        userData.playerStatusInfo = playerStatusInfo;
        userData.weaponInfo = weaponsInfo;

        return userData;
    }

    public void SetUserData(UserData data)
    {
        playInfo = data.playInfo;
        playerStatusInfo = data.playerStatusInfo;

        foreach(var weapon in data.weaponInfo.weaponsList)
        {
            WeaponType type = (WeaponType)Enum.Parse(typeof(WeaponType), weapon.name);
            weapons[type] = weapon;
        }

        userData = data;
    }
}