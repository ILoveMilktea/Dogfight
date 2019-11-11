using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발사체 클래스
public abstract class Projectile : MonoBehaviour
{
    protected GameObject source;

    public LayerMask collisionMask;
    //총알 속도
    protected float speed;
    //총알 데미지
    protected float damage;
    //총알 최대 거리
    protected float maxRange=10.0f;
    //총알 관통 여부
    protected bool isPenetratingActive=false;
    //필살기 모드 여부
    protected bool isSpecialMode = false;
    //KnockBack 모드 여부
    protected bool isKnockBackMode = false;
    //KnockBack 힘
    protected float knockBackForce = 5.0f;
    //KnockBack 시간
    protected float knockBackDuration = 0.3f;   

    //지금까지 간 거리 합
    protected float distanceTotal = 0.0f;     

    //Set함수
    public void SetSource(GameObject source)
    {
        this.source = source;
    }

    public void SetSpeed(float speed)
    {       
        this.speed = speed;        
    }

    public void SetMaxRange(float maxRange)
    {
        this.maxRange = maxRange;
    }
    
    public void SetKnockBackMode(bool mode)
    {
        isKnockBackMode = mode;
    }

    public void SetKnockBackForce(float force)
    {
        knockBackForce = force;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetSpecialMode(bool mode)
    {
        isSpecialMode = mode;
    }

    public void SetPentratingActive(bool mode)
    {
        this.isPenetratingActive = mode;

    }

    protected float Move()
    {
        float moveDistance = speed * Time.deltaTime;

        if (distanceTotal + moveDistance > maxRange)
        {
            moveDistance = maxRange - distanceTotal;
            distanceTotal = maxRange;
        }
        else
        {
            distanceTotal += moveDistance;
        }

        return moveDistance;
    }   

    //충돌 검사
    virtual protected void CheckCollision(float moveDistance)
    {                
        //Ray 생성
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Ray 발사
        if(Physics.Raycast(ray,out hit,moveDistance,collisionMask,QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
       
    }

    //물체와 부딪혔을 때 작동하는 함수
    protected void OnHitObject(RaycastHit hit)
    {
        Collider collider = hit.collider;
        IDamageable damageableObject=collider.GetComponent<IDamageable>();
        if(damageableObject!=null)
        {
            //damageableObject.TakeHit(damage);
            GameObject target = hit.transform.gameObject;
            FightSceneController.Instance.DamageToCharacter(source, target);
        } 
    }   

    //발사체가 최대 거리까지가 움직였는지 체크
    virtual protected void CheckMoveDistance()
    {            
        if(distanceTotal==maxRange)
        {           
            distanceTotal = 0;
            ObjectPoolManager.Instance.Free(gameObject);
        }
    }

    //넉백 효과
    public void KnockBack(Collider collider)
    {        
        Vector3 dir = (collider.transform.position - transform.position).normalized;
        dir.y = 0;

        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeKnockBack(dir, knockBackForce, knockBackDuration);
        }
    }
}
