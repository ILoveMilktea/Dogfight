using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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
    public Projectile projectile;
    //발사시간간격(ms)
    public float msBetweenShots = 500.0f;
    //발사체의 속도
    public float muzzleVelocity = 20.0f;
    //총 사정거리
    public float maxRange=10.0f;
    //총 데미지
    public float damage=1.0f;

    public int burstCount;

    //다음 발사 시간 계산
    protected float nextShotTime=0;

    protected bool triggerReleasedSinceLastShot=true;
    protected int shotsRemainingInBurst;

    virtual public void Shoot()
    {
        if(Time.time>nextShotTime)
        {
            //Time.time : 게임이 시작되고 지난 시간(초)
            nextShotTime = Time.time + msBetweenShots / 1000;

            //일직선 총
            
                if(fireMode==FireMode.AUTO)
                {
                    
                }                
                else if (fireMode == FireMode.SINGLE)
                {
                    if (!triggerReleasedSinceLastShot)
                    {
                        return;
                    }
                }
                else if (fireMode == FireMode.BURST)
                {
                    if (shotsRemainingInBurst == 0)
                    {
                        return;
                    }
                    --shotsRemainingInBurst;
                }
                
                Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;    

                if(skillMode==SkillMode.GENERAL)
                {

                }
                else if(skillMode==SkillMode.SPECIAL)
                {
                    
                    //발사체 관통 활성화
                    newProjectile.SetPentratingActive(true);
                }
                
                newProjectile.SetMaxRange(maxRange);
                newProjectile.SetDamage(damage);                
                newProjectile.SetSpeed(muzzleVelocity);            
            

        }
        
    }

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

}
