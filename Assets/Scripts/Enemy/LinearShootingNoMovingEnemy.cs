using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//고정된 자리에서 움직이지않고 회전하면서 쏘는 적
public class LinearShootingNoMovingEnemy : Enemy
{
    private enum LinearShootingNoMovingEnemyState
    {
        IDLE,
        SEARCHING,
        CHASING,
        ATTACKING_SHOOTING,
        DEAD
    };

    //Enemy 상태
    private LinearShootingNoMovingEnemyState linearShootingNoMovingEnemyState;

    //공격할 범위
    public float attackDistance = 5.0f;
    //공격간 간격
    public float timeBetweenAttack = 2.0f;
    //발사공격 코루틴
    private Coroutine shootingCoroutine;

    //[Animator관련 변수]
    //Animator 변수
    private Animator animator;

    //게임시작할때 몇초동안 기다렸다 Enemy상태 갱신하기
    private float waitTimeForStart = 2.0f;

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
            animator = this.gameObject.GetComponent<Animator>();
            target = FindObjectOfType<Player>().gameObject;
            SetHealth();
            isDead = false;
            linearShootingNoMovingEnemyState = LinearShootingNoMovingEnemyState.IDLE;

            animator.SetBool("IsDead1", false);

            StartCoroutine(EnemyAction());
            StartCoroutine(CheckEnemyState());
        }
    }   

    protected override void Move()
    {      

    }

    IEnumerator CheckEnemyState()
    {
        //맵 완전히 켜질때까지 기다리기
        yield return new WaitForSeconds(waitTimeForStart);

        while (!isDead)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (distance <= attackDistance)
            {
               
                //Debug.Log("적과의거리" + Vector3.Distance(transform.position, target.transform.position));
                transform.position = transform.position;
                linearShootingNoMovingEnemyState = LinearShootingNoMovingEnemyState.ATTACKING_SHOOTING;
            }
            else
            {                
                linearShootingNoMovingEnemyState = LinearShootingNoMovingEnemyState.CHASING;
            }
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator EnemyAction()
    {
        //맵 완전히 켜질때까지 기다리기
        yield return new WaitForSeconds(waitTimeForStart);

        Debug.Log("상태" + linearShootingNoMovingEnemyState);
        while (!isDead)
        {
            if (linearShootingNoMovingEnemyState == LinearShootingNoMovingEnemyState.IDLE)
            {
                //Debug.Log("상태" + linearShootingEnemyState);
            }
            else if (linearShootingNoMovingEnemyState == LinearShootingNoMovingEnemyState.SEARCHING)
            {
                if (shootingCoroutine != null)
                {
                    StopCoroutine(shootingCoroutine);
                    shootingCoroutine = null;
                }
                LockOnTarget();
            }           
            else if (linearShootingNoMovingEnemyState == LinearShootingNoMovingEnemyState.ATTACKING_SHOOTING)
            {
                SetAllAnimationFalse();
                animator.SetBool("isAttacking", true);

                LockOnTarget();
                if (shootingCoroutine == null)
                {
                    shootingCoroutine = StartCoroutine(Shooting());
                }
                //Debug.Log("상태" + linearShootingEnemyState);
            }
            else if (linearShootingNoMovingEnemyState == LinearShootingNoMovingEnemyState.DEAD)
            {
                isDead = true;

                SetAllAnimationFalse();
                animator.SetBool("isDead1", true);

                //죽는 애니메이션 5초동안 유지하고 없어짐
                yield return new WaitForSeconds(3.0f);
                ObjectPoolManager.Instance.Free(gameObject);

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

    //모든 Animator 변수 false하기
    private void SetAllAnimationFalse()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);        
    }
}
