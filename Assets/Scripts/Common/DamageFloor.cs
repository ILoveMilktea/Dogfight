using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFloor : MonoBehaviour
{
    //장판 데미지
    public int damage = 1;
    //장판 지속 시간
    public float lastingTime = 100.0f;
    //장판 크기
    public float floorRange = 5.0f;
    //장판 공격시간간격
    public float timeBetweenAttack = 1.0f;
    //공격할 수 있는지 여부
    private bool isAttackAvailable=true;   
    
    //장판 지속 시간 끝났는지 여부
    private bool isFloorFinished=false;
    //장판 공격 시간
    private IEnumerator attackCoroutine;
    

    // Start is called before the first frame update
    private void Start()
    {        
        //장판 크기 설정
        gameObject.transform.localScale = new Vector3(floorRange, 0.1f, floorRange);
        //장판 지속 시간 체크 코루틴 시작
        StartCoroutine(FloorLastingTimer());
    }

    // Update is called once per frame
    private void Update()
    {
        if(isFloorFinished==true)
        {            
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();        
    }

    //장판 공격
    private void Attack(Collider other)
    {
        
        IDamageable damageableObject = other.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            
            damageableObject.TakeHit(damage);
        }
    }  

    private void OnTriggerStay(Collider other)
    {       
        if (isAttackAvailable==true)
        {
            if (other.tag == "Enemy")
            {                
                attackCoroutine = AttackTimer();
                StartCoroutine(attackCoroutine);
                Attack(other);               
            }
        }        
    }

    IEnumerator AttackTimer()
    {        
        isAttackAvailable = false;
        yield return new WaitForSeconds(timeBetweenAttack);
        isAttackAvailable = true;
    }

    IEnumerator FloorLastingTimer()
    {
        yield return new WaitForSeconds(lastingTime);
        isFloorFinished = true;
    }
}
