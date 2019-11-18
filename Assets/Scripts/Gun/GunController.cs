using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun startingGun;
    private Gun equippedGun;

    void Start()
    {
        if(startingGun!=null)
        {           
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if(equippedGun!=null)
        {
            Destroy(equippedGun.gameObject);
        }
        //ObjectPool 방식으로 수정해야함
        equippedGun = Instantiate(gunToEquip,weaponHold.position,weaponHold.rotation);        
        equippedGun.transform.parent = weaponHold;
    }


    public void OnTirggerHold()
    {
        if(equippedGun!=null)
        {            
            equippedGun.OnTriggerHold();
        }
    }
    
    public void OnTriggerRelease()
    {
        if(equippedGun!=null)
        {
            equippedGun.OnTriggerRelease();
        }
    }
    
}
