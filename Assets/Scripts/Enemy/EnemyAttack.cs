﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//적의 공격하는 패턴 클래스
public class EnemyAttack : MonoBehaviour
{
    //발사체를 발사한 대상
    private GameObject source;
    //발사체 사정거리
    public float maxRange;
    //발사체 데미지
    public float damage;
    //발사체 속도
    public float muzzleVelocity;
    //발사체 날라가는 방향 개수
    public int directionNumber = 3;
    //발사체 나가는 최대각도
    public float projectileMaxAngle = 120.0f; 
    //발사체 중간에 speed 바뀌는 모드 적용할지
    public bool speedChangeMode=false;
    //발사체 전체 사정 거리중 speed가 바뀌는 지점 비율(예시)30퍼센트)
    public float speedChangeRatio = 0.2f;
    //발사체 Prefab 이름
    public string projectilePrefabName;

    //Knock관련 변수
    public float knockBackForce;
    public float knockBackDuration;

    //SpereadShooting관련 변수
    //발사체들의 초기 발사각도값을 담을 변수
    private List<Quaternion> projectiles_rotations;
    //한번에 나갈 발사체 갯수
    public int projectileCount = 10;
    //발사체가 퍼지는 최대 각도
    public float projectileSpreadAngle = 45.0f;

    private void Awake()
    {
        source = gameObject;

        projectiles_rotations = new List<Quaternion>(projectileCount);

        for (int i = 0; i < projectileCount; ++i)
        {
            projectiles_rotations.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        //ResetValue();
    }

    //일반 직선으로 날아가는 발사체 공격
    public void LinearShooting(Transform muzzle)
    {
        float tmpAngle = 0f;
        if(directionNumber!=1)
        {
            tmpAngle = -projectileMaxAngle * 0.5f;
        }      
           
        float variation = projectileMaxAngle / (directionNumber - 1);
        //날라갈 방향만큼 총알 각도 조절
        for(int i=0; i<directionNumber; ++i)
        {
            GameObject newProjectileObject = ObjectPoolManager.Instance.Get(projectilePrefabName);            
            Transform projectileTransform = newProjectileObject.transform;
            projectileTransform.position = muzzle.position;
            projectileTransform.rotation = muzzle.rotation;
            Projectile newProjectile = newProjectileObject.GetComponent<Projectile>();

            //GameObject source = FindObjectOfType<Player>().gameObject;
            newProjectile.SetSource(source);
            newProjectile.SetMaxRange(maxRange);
            newProjectile.SetDamage(damage);
            newProjectile.SetSpeed(muzzleVelocity);
            newProjectile.SetSpeedChangeMode(speedChangeMode);
            if(speedChangeMode==true)
            {
                newProjectile.SetSpeedChangingRatio(speedChangeRatio);
            }
            
            newProjectile.SetRotation(Quaternion.Euler(newProjectile.transform.eulerAngles + new Vector3(0,tmpAngle,0)));            
            tmpAngle += variation;

            newProjectileObject.SetActive(true);
        }        
    }

    public void Rushing(NavMeshAgent navMeshAgent, Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    public void KnockBack(GameObject target)
    {
        //이거 나중에 인자빼기
        IDamageable damageableObject = target.GetComponent<IDamageable>();

        //넉백 주기
        
        Vector3 knockBackDir = (target.transform.position-transform.position).normalized;
        knockBackDir.y = 0;
        
        damageableObject.TakeKnockBack(knockBackDir,knockBackForce,knockBackDuration);
    }

    public void SpreadShooting(Transform muzzle)
    {
       

        projectilePrefabName = "Bullet_Enemy";
        for (int i = 0; i < projectiles_rotations.Count; ++i)
        {
            projectiles_rotations[i] = Random.rotation;            

            GameObject newProjectileObject = ObjectPoolManager.Instance.Get(projectilePrefabName);
            Transform projectileTransform = newProjectileObject.transform;
            projectileTransform.position = muzzle.position;
            projectileTransform.rotation = muzzle.rotation;
            Projectile newProjectile = newProjectileObject.GetComponent<Projectile>();

            Quaternion tmp = Quaternion.RotateTowards(newProjectile.transform.rotation, projectiles_rotations[i], projectileSpreadAngle);
            Vector3 randomRotation = tmp.eulerAngles;
            randomRotation.x = 0;
            tmp = Quaternion.Euler(randomRotation);
            newProjectile.transform.rotation = tmp;

            newProjectile.SetDamage(damage);
            newProjectile.SetMaxRange(maxRange);
            newProjectile.SetSpeed(muzzleVelocity);              
            newProjectileObject.SetActive(true);
        }
    }    

    //적 종류에 따라 발사체관련 값 지정
    public void SetProjectileValue(string projectilePrefabName, float maxRange, float damage, float muzzleVelocity)
    {
        this.projectilePrefabName = projectilePrefabName;
        this.maxRange = maxRange;
        this.damage = damage;
        this.muzzleVelocity = muzzleVelocity;       
    }

    public void SetSpeedChangeMode(bool mode)
    {
        this.speedChangeMode = mode;
    }   

    public void SetProjectileMaxAngle(float maxAngle)
    {
        this.projectileMaxAngle = maxAngle;
    }

    //비활성화 될때 값 초기화
    public void ResetValue()
    {
        //speedChangeMode = false;
    }

}