using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const_PassiveSkill
{
    public const string AttackDamageUp = "ATKup";
    public const string AttackSpeedUp = "ASup";
    public const string AttackRangeUp = "ARup";
    public const string AttackAngleUp = "AAup";
    public const string CriticalChanceUp = "Criticalup";
}

public static class Const_ActiveSkill_1st
{ 
    public const string PanetratingShot = "U_PanetratingShot";
    public const string KnockBack = "U_KnockBack";
    public const string GravityField = "U_GravityField";
    public const string NodeKey = "4_1";
}

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun startingGun;
    private Gun equippedGun;

    private WeaponType equipingWeapon;

    void Start()
    {
        if(startingGun!=null)
        {
            equipingWeapon = (WeaponType)Enum.Parse(typeof(WeaponType), startingGun.name);
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if(equippedGun!=null)
        {
            Destroy(equippedGun.gameObject);
            //equippedGun.gameObject.SetActive(false);
        }
        //ObjectPool 방식으로 수정해야함
        //gunToEquip.gameObject.SetActive(true);
        //gunToEquip.transform.position = weaponHold.position;
        //gunToEquip.transform.rotation = weaponHold.rotation;

        //equippedGun = gunToEquip;
        equippedGun = Instantiate(gunToEquip,weaponHold.position,weaponHold.rotation);
        ApplyActivatedSkill(equipingWeapon, equippedGun);
        equippedGun.transform.parent = weaponHold;
    }


    public void OnTirggerHold()
    {
        if(equippedGun!=null)
        {            
            equippedGun.OnTriggerHold();
        }
    }

    //skill 공격 추가
    public void OnSkillTriggerHold()
    {
        if (equippedGun != null)
        {
            equippedGun.OnSkillTriggerHold();
        }
    }
    
    public void OnTriggerRelease()
    {
        if(equippedGun!=null)
        {
            equippedGun.OnTriggerRelease();
        }
    }



    // swappppp
    public WeaponType SwapWeapon()
    {
        if (equipingWeapon == WeaponType.ShotGun)
        {
            equipingWeapon = WeaponType.EnergySphereGun;
        }
        else
        {
            equipingWeapon++;
        }

        //Gun gun = ObjectPoolManager.Instance.ReadFirstOneFromUnusedList(equipingWeapon.ToString()).GetComponent<Gun>();
        //gunController.EquipGun(gun);
        Gun gun = Resources.Load<Gun>("Prefab/Weapon/" + equipingWeapon.ToString());
        EquipGun(gun);

        return equipingWeapon;
    }

    // 스킬 켜진거 적용하는곳
    private void ApplyActivatedSkill(WeaponType type, Gun gun)
    {
        Dictionary<string, WeaponSkill> skillTree = DataManager.Instance.GetWeapons[type].SkillTree;

        foreach (var node in skillTree)
        {
            if (node.Value.IsActivated && node.Key != "0")
            {
                WeaponSkillInfo skillInfo = WeaponSkillTable.Instance.GetTuple(type.ToString(), node.Key);

                switch (skillInfo.m_spriteName)
                {
                    case Const_PassiveSkill.AttackDamageUp:
                        gun.damage += skillInfo.m_value;
                        break;
                    case Const_PassiveSkill.AttackSpeedUp:
                        gun.msBetweenShots -= skillInfo.m_value;
                        break;
                    case Const_PassiveSkill.AttackRangeUp:
                        gun.maxRange += skillInfo.m_value;
                        break;
                    case Const_PassiveSkill.AttackAngleUp:
                        gun.projectileMaxAngle += skillInfo.m_value;
                        break;
                    case Const_PassiveSkill.CriticalChanceUp:
                        gun.criticalChance += skillInfo.m_value;
                        break;
                    case Const_ActiveSkill_1st.PanetratingShot:
                    case Const_ActiveSkill_1st.KnockBack:
                    case Const_ActiveSkill_1st.GravityField:
                        gun.isSkillEquiped = true;
                        gun.skillKey = Const_ActiveSkill_1st.NodeKey;
                        break;
                }
            }
        }
    }
}
