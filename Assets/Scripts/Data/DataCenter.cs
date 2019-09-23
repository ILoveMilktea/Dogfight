using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    a,
    b,
    c
}
public class DataCenter : MonoSingleton<DataCenter>
{
    private UserData userData;

    public UserData GetUserData { get { return userData; } }
    public PlayerStatus GetPlayerStatus { get { return userData.playerStatus; } }
    public void SetMaxHp(int value) { userData.playerStatus.maxHp = value; }
    public void SetRemainHp(int value) { userData.playerStatus.remainHp = value; }
    public void SetAtk(int value) { userData.playerStatus.atk = value; }
    public void SetDef(int value) { userData.playerStatus.def = value; }


    public Weapons GetWeapons { get { return userData.weapons; } }
    public WeaponStatus GetWeaponByType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.a:
                return userData.weapons.wa;
            case WeaponType.b:
                return userData.weapons.wb;
            case WeaponType.c:
                return userData.weapons.wc;
            default:
                return userData.weapons.wa;
        }
    }
    public void SetWeaponLevel(WeaponType type, int value)
    {
        switch (type)
        {
            case WeaponType.a:
                userData.weapons.wa.level = value;
                break;
            case WeaponType.b:
                userData.weapons.wb.level = value;
                break;
            case WeaponType.c:
                userData.weapons.wc.level = value;
                break;
            default:
                userData.weapons.wa.level = value;
                break;
        }
    }
    public void SetWeaponExp(WeaponType type, int value)
    {
        switch (type)
        {
            case WeaponType.a:
                userData.weapons.wa.level = value;
                break;
            case WeaponType.b:
                userData.weapons.wb.level = value;
                break;
            case WeaponType.c:
                userData.weapons.wc.level = value;
                break;
            default:
                userData.weapons.wa.level = value;
                break;
        }
    }

    public PlayInfo GetPlayInfo { get { return userData.playInfo; } }
    public void SetPlaytime(float value) { userData.playInfo.playtime = value; }
    public void SetStage(int value) { userData.playInfo.stage = value; }
    public void SetEnergy(int value) { userData.playInfo.energy = value; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateCenter();
        SetDefaultData();
    }

    private void CreateCenter()
    {
        userData = new UserData();

        userData.playerStatus = new PlayerStatus();

        userData.weapons = new Weapons();
        userData.weapons.wa = new WeaponStatus();
        userData.weapons.wb = new WeaponStatus();
        userData.weapons.wc = new WeaponStatus();

        userData.playInfo = new PlayInfo();
    }

    private void SetDefaultData()
    {
        SetMaxHp(100);
        SetRemainHp(100);
        SetAtk(5);
        SetDef(5);

        SetWeaponLevel(WeaponType.a, 1);
        SetWeaponExp(WeaponType.a, 0);
        SetWeaponLevel(WeaponType.b, 1);
        SetWeaponExp(WeaponType.b, 0);
        SetWeaponLevel(WeaponType.b, 1);
        SetWeaponExp(WeaponType.b, 0);

        SetPlaytime(0f);
        SetStage(1);
        SetEnergy(0);
    }

    public void SetLoadData(string jsonData)
    {
        userData = JsonUtility.FromJson<UserData>(jsonData);
    }

    public void MakeNewStorage()
    {

    }
}
