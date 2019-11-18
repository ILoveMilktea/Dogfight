using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발사무기 클래스
public abstract class Gun : MonoBehaviour
{
    //Auto : 자동(계속 연발)
    //Burst : 한번에 n발 연속으로 발사
    //Singe : 단발
    //public enum GunMode { LINEAR, ENERGYSPHERE, SHOTGUN};
    public enum FireMode { AUTO, SINGLE, BURST};
    public enum SkillMode { GENERAL, SPECIAL};

    //public GunMode gunMode;
    public FireMode fireMode;
    public SkillMode skillMode;

    //총구
    public Transform muzzle;
    //발사체
    public string projectilePrefabName;
    //발사시간간격(ms)
    public float msBetweenShots = 500.0f;
    //발사체의 속도
    public float muzzleVelocity = 20.0f;
    //총 사정거리
    public float maxRange=10.0f;
    //총 데미지
    public float damage=1.0f;
    //burst모드일때 한번에 최대 몇개쏠수 있는지
    public int burstCount;

    //다음 발사 시간 계산
    protected float nextShotTime=0;

    protected bool triggerReleasedSinceLastShot=true;
    protected int shotsRemainingInBurst=0;

    public abstract void Shoot();   

    public void OnTriggerHold()
    {       
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }

    protected void SetProjectilePrefabName(string name)
    {
        projectilePrefabName = name;
    }

}
