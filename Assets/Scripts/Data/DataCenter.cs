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

    public void SetWeapons(Dictionary<WeaponType, WeaponInfo> value)
    {
        weapons.Clear();
        weapons = value;
    }
}