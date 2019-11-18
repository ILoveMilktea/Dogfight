using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//고정된 자리에서 움직이지않고 회전하면서 쏘는 적
public class LinearShootingNoMovingEnemy : Enemy
{
    private enum LinearShootingEnemyState
    {
        IDLE,
        SEARCHING,
        CHASING,
        ATTACKING_SHOOTING,
        DEAD
    };

    //Enemy 상태
    private LinearShootingEnemyState linearShootingEnemyState;

    //공격할 범위
    public float attackDistance = 5.0f;
    //공격간 간격
    public float timeBetweenAttack = 2.0f;
    //발사공격 코루틴
    private Coroutine shootingCoroutine; 

    private void OnEnable()
    {
        //ObjectPooling하기위해 활성화된 경우
        if (isOnObjectPooling == true)
        {
            isOnObjectPooling = false;
        }
        //실제 플레이안에서 활성화된 경우
        else
        {
            target = FindObjectOfType<Player>().gameObject;
            SetHealth();
            isDead = false;
            linearShootingEnemyState = LinearShootingEnemyState.IDLE;

            StartCoroutine(EnemyAction());
            StartCoroutine(CheckEnemyState());
        }
    }

    protected override void Move()
    {      

    }

    IEnumerator CheckEnemyState()
    {
        while (!isDead)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (distance <= attackDistance)
            {
               
                //Debug.Log("적과의거리" + Vector3.Distance(transform.position, target.transform.position));
                transform.position = transform.position;
                linearShootingEnemyState = LinearShootingEnemyState.ATTACKING_SHOOTING;
            }
            else
            {                
                linearShootingEnemyState = LinearShootingEnemyState.CHASING;
            }
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator EnemyAction()
    {
        while (!isDead)
        {
            if (linearShootingEnemyState == LinearShootingEnemyState.IDLE)
            {
                //Debug.Log("상태" + linearShootingEnemyState);
            }
            else if (linearShootingEnemyState == LinearShootingEnemyState.SEARCHING)
            {
                if (shootingCoroutine != null)
                {
                    StopCoroutine(shootingCoroutine);
                    shootingCoroutine = null;
                }
                LockOnTarget();
            }           
            else if (linearShootingEnemyState == LinearShootingEnemyState.ATTACKING_SHOOTING)
            {
                LockOnTarget();
                if (shootingCoroutine == null)
                {
                    shootingCoroutine = StartCoroutine(Shooting());
                }
                //Debug.Log("상태" + linearShootingEnemyState);
            }
            else if (linearShootingEnemyState == LinearShootingEnemyState.DEAD)
            {
                linearShootingEnemyState = LinearShootingEnemyState.IDLE;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Shooting()
    {
        //Debug.Log("shooting코루틴");
        while (!isDead)
        {
            enemyAttack.LinearShooting(muzzle);
            yield return new WaitForSeconds(timeBetweenAttack);
        }
    }
}
