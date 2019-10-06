using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    //샷건 KnockBack Force
    public float knockBackforce = 10.0f;   
    //샷건에서 한번에 나가는 총알 갯수
    public int projectileCount=3;
    //샷건에서 총알이 퍼지는 최대 각도
    public float spreadAngle=45.0f;   
   
    //projectile들의 회전값을 담을 변수
    List<Quaternion> projectiles;    

    // Start is called before the first frame update
    private void Awake()
    {         
        projectiles = new List<Quaternion>(projectileCount);

        for(int i=0; i<projectileCount; ++i)
        {
            projectiles.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    private void Start()
    {
       
    }

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
            
            for (int i = 0; i < projectiles.Count; ++i)
            {
                projectiles[i] = Random.rotation;
                Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;               
                newProjectile.transform.rotation = Quaternion.RotateTowards(newProjectile.transform.rotation, projectiles[i], spreadAngle);

                if (skillMode == SkillMode.GENERAL)
                {
                    newProjectile.SetDamage(damage);
                    newProjectile.SetMaxRange(maxRange);
                    newProjectile.SetSpeed(muzzleVelocity);
                    newProjectile.SetKnockBackMode(true);
                    newProjectile.SetKnockBackForce(knockBackforce);
                }
                else if (skillMode == SkillMode.SPECIAL)
                {
                    newProjectile.SetDamage(damage);
                    newProjectile.SetMaxRange(maxRange);
                    newProjectile.SetSpeed(muzzleVelocity);
                    //p.SetKnockBackMode(true);
                    //p.SetKnockBackForce(knockBackforce);
                    newProjectile.SetPentratingActive(true);
                }
            }

        }
    }

    //아직 지우지 마세요~
    //private bool CheckFanShapedRange(Transform target)
    //{
    //    //총구 위치
    //    Transform tmp = projectileSpawnTransform;
    //    //총구 벡터
    //    Vector3 sourceVec = tmp.position;
    //    sourceVec.y = 0;
    //    //총구 ~ Target의 방향 벡터
    //    Vector3 targetVec = target.transform.position - sourceVec;
    //    targetVec.y = 0;

    //    //총구의 로컬벡터
    //    Vector3 tmpLocalPosition = tmp.localPosition;
    //    //Debug.Log("localPosition" + tmpLocalPosition);

    //    //총구의 로컬벡터를 월드벡터로 변환(방향)
    //    Vector3 convertTmpLocalDirWorldDir = transform.TransformDirection(tmpLocalPosition);
    //    convertTmpLocalDirWorldDir.y = 0;

    //    //변환한 총구 방향 벡터와 (총구~Target의 방향 벡터) 간의 각도
    //    float angle = Vector3.Angle(targetVec, convertTmpLocalDirWorldDir);        
    //    Debug.Log("각도:"+angle);

    //    Vector3 debugVectorLeft = Quaternion.Euler(0, -shotGunRange, 0) * convertTmpLocalDirWorldDir*100;
    //    Vector3 debugVectorRight = Quaternion.Euler(0, shotGunRange, 0) * convertTmpLocalDirWorldDir*100;

    //    Debug.DrawLine(transform.TransformPoint(tmpLocalPosition), debugVectorLeft*100, Color.red,1.0f);
    //    Debug.DrawLine(transform.TransformPoint(tmpLocalPosition), debugVectorRight* 100, Color.red, 1.0f);        

    //    //샷건 범위각도안에 들어오면 
    //    if (angle<=shotGunRange)
    //    {
    //        return true;
    //    }

    //    return false;
    //}   

}
