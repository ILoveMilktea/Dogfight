using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnerySphere(에너지구) 클래스
public class EnergySphere : Projectile
{
    //상태
    public enum EnerySphereState { MOVING, STOP, DESTROY };
    public EnerySphereState state;

    //Special모드일때 구 지속시간
    public float lastingTime = 10.0f;
    
    private bool isArrived=false;
    //필살기 모드인지   
    private bool isChildrenDestroyed = false;

    //자식 붙어있는거 관리
    public GameObject[] childAttached;  

    void Update()
    {       
        if(state==EnerySphereState.MOVING)
        {
            float moveDistance = Move();

            if (isArrived==false)
            {
                CheckCollision(moveDistance);
                transform.Translate(Vector3.forward * moveDistance);
                CheckMoveDistance();
            }            
            else
            {                
                if(isSpecialMode==false)
                {
                    state = EnerySphereState.DESTROY;
                }
                else
                {
                    
                    state = EnerySphereState.STOP;
                    ////DamageFloor 활성화
                    //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    ////AttractFloor 활성화                                
                    //gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    for(int i=0; i<childAttached.Length; ++i)
                    {
                        GameObject child = Instantiate(childAttached[i], transform);
                        child.transform.parent = this.gameObject.transform;
                    }
                    
                }

            }
        }        
        else if(state==EnerySphereState.STOP)
        {           
            StartCoroutine(DestroyTimer());
        }
        else if (state == EnerySphereState.DESTROY)
        {
            if(isSpecialMode==false)
            {
               
                ResetFreeValue();
                ObjectPoolManager.Instance.Free(gameObject);
            }
            else
            {
               
                if (isChildrenDestroyed == false)
                {
                    isChildrenDestroyed = true;
                    for (int i = 0; i < gameObject.transform.childCount; ++i)
                    {
                        if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
                        {
                            //Debug.Log("자식 destroy" + gameObject.transform.GetChild(i).gameObject);                            
                            Destroy(gameObject.transform.GetChild(i).gameObject);
                        }
                    }
                }

                if (transform.childCount == 0)
                {
                    ResetFreeValue();
                    ObjectPoolManager.Instance.Free(gameObject);

                }
            }
                
        }
       
    }

    private void OnDestroy()
    {        
        StopAllCoroutines();        
    }  

    private void ResetFreeValue()
    {
        distanceTotal = 0;
        state = EnerySphereState.MOVING;
        isArrived = false;
        isChildrenDestroyed = false;
    }  

    protected override void CheckCollision(float moveDistance)
    {        
        //Ray 생성
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Ray 발사
        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            //Debug.Log("enemy");
            isArrived = true;
            if(isSpecialMode==false)
            {
                OnHitObject(hit);
            }                      
        }
    }    

    //일정 거리까지 가서 멈추기
    override protected void CheckMoveDistance()
    { 
        if (distanceTotal == maxRange)
        {                    
            isArrived = true;
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(lastingTime);
        state = EnerySphereState.DESTROY;
    }   
    
}
