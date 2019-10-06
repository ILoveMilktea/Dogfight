using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    //총알 속도
    protected float speed;
    //총알 데미지
    protected float damage;
    //총알 최대 거리
    protected float maxRange;
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

    protected Vector3 startPosition;
    
    void Start()
    {
        //isPenetratingActive = false;        
        startPosition = gameObject.transform.position;        
    }

    // Update is called once per frame
    void Update()
    {       
        float moveDistance = speed * Time.deltaTime;
              
        CheckCollision(moveDistance);        
        transform.Translate(Vector3.forward * moveDistance);
        CheckMoveDistance();

    }

    //Set함수
    public void SetSpeed(float _speed)
    {       
        speed = _speed;        
    }

    public void SetMaxRange(float _maxRange)
    {
        maxRange = _maxRange;
    }
    
    public void SetKnockBackMode(bool mode)
    {
        isKnockBackMode = mode;
    }

    public void SetKnockBackForce(float force)
    {
        knockBackForce = force;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
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

    protected void OnHitObject(RaycastHit hit)
    {
        Collider collider = hit.collider;
        IDamageable damageableObject=collider.GetComponent<IDamageable>();
        if(damageableObject!=null)
        {
            damageableObject.TakeHit(damage);
            if(isKnockBackMode==true)
            {
                KnockBack(collider);
            }
        }

        if (!isPenetratingActive)
        {            
            GameObject.Destroy(gameObject);
        }
        
    }

    public void SetPentratingActive(bool mode)
    {        
        this.isPenetratingActive = mode;
       
    }

    //총알 거리 계산
    virtual protected void CheckMoveDistance()
    {        
        if (Vector3.Distance(startPosition,gameObject.transform.position)>=maxRange)
        {             
            GameObject.Destroy(gameObject);            
        }        
    }

    public void KnockBack(Collider collider)
    {
        Debug.Log("KnockBack");
        Vector3 dir = (collider.transform.position - transform.position).normalized;
        dir.y = 0;

        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeKnockBack(dir, knockBackForce, knockBackDuration);
        }
    }
}
