using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereGun : Gun
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    override public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            //Time.time : 게임이 시작되고 지난 시간(초)
            nextShotTime = Time.time + msBetweenShots / 1000;
           
            if (fireMode == FireMode.AUTO)
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

            if (skillMode == SkillMode.GENERAL)
            {
                newProjectile.SetDamage(damage);
                newProjectile.SetMaxRange(maxRange);                
                newProjectile.SetSpeed(muzzleVelocity);
                
            }
            else if (skillMode == SkillMode.SPECIAL)
            {
                if(newProjectile is EnergySphere)
                {
                    newProjectile.SetDamage(damage);
                    newProjectile.SetMaxRange(maxRange);
                    newProjectile.SetSpeed(muzzleVelocity);
                    (newProjectile as EnergySphere).SetSpecialMode(true);
                }
                
            }

        }

    }
}
