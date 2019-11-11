using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearGun : Gun
{   
    override public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            //Time.time : 게임이 시작되고 지난 시간(초)
            nextShotTime = Time.time + msBetweenShots / 1000;

            //일직선 총

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

            GameObject newProjectileObject = ObjectPoolManager.Instance.Get("Bullet");
            Transform projectileTransform = newProjectileObject.transform;
            //Debug.Log("muzzle position: " + muzzle.position);
            projectileTransform.position = muzzle.position;
            projectileTransform.rotation = muzzle.rotation;
            Projectile newProjectile = newProjectileObject.GetComponent<Projectile>();
            
            //Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;

            if (skillMode == SkillMode.GENERAL)
            {

            }
            else if (skillMode == SkillMode.SPECIAL)
            {

                //발사체 관통 활성화
                newProjectile.SetPentratingActive(true);
            }

            GameObject source = FindObjectOfType<Player>().gameObject;
            newProjectile.SetSource(source);
            newProjectile.SetMaxRange(maxRange);
            newProjectile.SetDamage(damage);
            newProjectile.SetSpeed(muzzleVelocity);

            //Debug.Log("projectile position: " + newProjectileObject.transform.position);

            newProjectileObject.SetActive(true);
           
        }

    }
}
