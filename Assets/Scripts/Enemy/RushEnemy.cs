using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushEnemy : Enemy
{
    private enum RushEnemyState
    {
        IDLE,
        SEARCHING,
        CHASING,
        RUSHING_START,
        RUSHING,
        RUSHING_MISS,
        RUSHING_HIT,        
        DEAD
    };

    //Enemy 상태
    private RushEnemyState rushEnemyState;

    //공격할 범위
    public float attackDistance = 5.0f;
    //추적할 범위
    public float chasingDistance = 8.0f;
    //공격간 간격
    public float timeBetweenAttack = 2.0f;
    //발사공격 코루틴
    private Coroutine rushTimerCoroutine;
    //Searching할 때 가속력
    public float acceleration = 10.0f;   
    //Rushing할 때 스피드
    public float rushSpeed;

    //Rush중인가 아닌가 상태
    private bool isRushAvailable = true;
    private bool isOnRush = false;
    private bool isRushHit = false;

    //Rushing할때 목표지점
    Vector3 rushDestination;    

    //맵 사이즈 캐싱
    private Vector2 mapSize;
    //search관련 여부
    private bool isSearchRestart = false;
    //Searching할 때 목표지점
    private Vector3 searchingDestination;
    //Search할 때 너무 구석으로 안가기 위한 offset
    public float searchingOffset;
    //Search 발동할때 움직일 범위(현재 위치 기준으로 얼마나 Search하러갈지)
    public int searchRangeMin;
    public int searchRangeMax;
    //Search할때 범위
    private float searchingRangeMaxX;
    private float searchingRangeMinX;
    private float searchingRangeMaxZ;
    private float searchingRangeMinZ; 

    public NavMeshAgent navMeshAgent;

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
            navMeshAgent = GetComponent<NavMeshAgent>();

            SetHealth();
            isDead = false;
            rushEnemyState = RushEnemyState.IDLE;

            navMeshAgent.speed = moveSpeed;
            navMeshAgent.acceleration = acceleration;
            navMeshAgent.stoppingDistance = 0;

            //영준수정- 매니저에서 Map Size 받아오는걸로 변경
            mapSize = new Vector2(20, 20);
            //처음에 Search활성화 해주기위한 것
            searchingDestination = transform.position;
            //searching관련 값 미리 계산
            searchingRangeMaxX = mapSize.x * 0.5f - searchingOffset;
            searchingRangeMinX = mapSize.x * -0.5f + searchingOffset;
            searchingRangeMaxZ = mapSize.y * 0.5f - searchingOffset;
            searchingRangeMinZ = mapSize.y * -0.5f + searchingOffset;

            StartCoroutine(EnemyAction());
            StartCoroutine(CheckEnemyState());
        }               

    }

    private void OnDisable()
    {
        ResetValue();
    }

    protected override void Move()
    {
        //Debug.Log("mvoe중");
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }

    private void Searching()
    {        
       
        Vector3 toSearchPosition = transform.position;
        toSearchPosition.y = 0;
        float randomX = Random.Range(searchRangeMin, searchRangeMax);
        float randomZ = Random.Range(searchRangeMin, searchRangeMax);

        if(toSearchPosition.x+randomX>searchingRangeMaxX || toSearchPosition.x+randomX<searchingRangeMinX)
        {
            randomX = 0;
        }
        toSearchPosition.x += randomX;
        if (toSearchPosition.z + randomZ > searchingRangeMaxZ || toSearchPosition.z + randomZ <searchingRangeMinZ)
        {
            randomZ = 0;
        }
        toSearchPosition.z += randomZ;

        searchingDestination = toSearchPosition;
        navMeshAgent.SetDestination(searchingDestination);
        
    }

    IEnumerator CheckEnemyState()
    {
        yield return new WaitForSeconds(3.0f);
        while (!isDead)
        {
            
            if (isRushHit==true || health<=0)
            {
                rushEnemyState = RushEnemyState.DEAD;
            }
            else
            {
                //Debug.Log("상태" + linearShootingEnemyState);
                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance <= attackDistance && isRushAvailable == true && isOnRush == false)
                {
                    isSearchRestart = true;
                    navMeshAgent.speed = rushSpeed;
                    //navMeshAgent.acceleration = acc;               

                    //Debug.Log("적과의거리" + Vector3.Distance(transform.position, target.transform.position));    
                    isRushAvailable = false;
                    isOnRush = true;
                    rushEnemyState = RushEnemyState.RUSHING_START;


                }
                else if (distance <= attackDistance && isRushAvailable == false)
                {
                    if (isOnRush == true)
                    {
                        rushEnemyState = RushEnemyState.RUSHING;
                        //if (isRushHit == true)
                        //{
                        //    isOnRush = false;
                        //    rushEnemyState = RushEnemyState.RUSHING_HIT;
                        //}
                    }
                    else
                    {
                        if (isRushHit == false)
                        {
                            rushEnemyState = RushEnemyState.RUSHING_MISS;
                        }

                    }
                }                
                else
                {
                    navMeshAgent.speed = moveSpeed;
                    //searchingDestination = transform.position;
                    navMeshAgent.isStopped = false;
                    rushEnemyState = RushEnemyState.SEARCHING;
                    //navMeshAgent.stoppingDistance = 0;
                }
            }
            
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator EnemyAction()
    {
        //영준 수정 - 나중에 코드 합쳤을때는 이거 없애도됨
        yield return new WaitForSeconds(3.0f);

        while (!isDead)
        {
            //Debug.Log("상태" + rushEnemyState);
            if (rushEnemyState == RushEnemyState.IDLE)
            {
                
            }
            else if (rushEnemyState == RushEnemyState.SEARCHING)
            {
                Vector3 curPosition = transform.position;
                curPosition.y = 0;

                Vector3 searchPosition;
                if (isSearchRestart == true)
                {
                    isSearchRestart = false;
                    searchPosition = curPosition;
                }
                else
                {
                    searchPosition = searchingDestination;
                    searchPosition.y = 0;
                }


                if (curPosition == searchPosition)
                {                   
                    Searching();
                }
            }
            else if (rushEnemyState == RushEnemyState.CHASING)
            {
                Move();
                LockOnTarget();

            }
            else if (rushEnemyState == RushEnemyState.RUSHING_START) //Rush시작할때
            {
                LockOnTarget();
                Rush();
            }
            else if (rushEnemyState == RushEnemyState.RUSHING) //Rush중
            {
                Vector3 enemyPosition = transform.position;
                enemyPosition.y = 0;

                //만약 적이 Rush목표지점에 도착했다면                
                if (enemyPosition == rushDestination)
                { 
                    //Rush쿨타임 기다리는 동안 타겟(플레이어)방향으로 회전하기          
                    isOnRush = false;                  
                      
                }
                
            }
            else if (rushEnemyState == RushEnemyState.RUSHING_MISS) //Rush했는데 아무것도 안 부딪혔을때
            {
                LockOnTarget();
                if (rushTimerCoroutine == null)
                {
                    isOnRush = false;
                    rushTimerCoroutine = StartCoroutine(RushTimer());
                }                
            }
            else if(rushEnemyState==RushEnemyState.RUSHING_HIT) //Rush했는데 타겟에 맞았을때
            {
                //LockOnTarget();
                //if (rushTimerCoroutine==null)
                //{                    
                //    rushTimerCoroutine = StartCoroutine(RushTimer());
                //}
                
            }
            else if (rushEnemyState == RushEnemyState.DEAD)
            {
                isDead = true;
                //영준수정- 나중에 Destroy되는거 ObjectPool로 바꿔야함
                //ObjectPoolManager.Instance.Free(gameObject);   
                Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void Rush()
    {        
        rushDestination = target.transform.position;
        rushDestination.y = 0;
        enemyAttack.Rushing(navMeshAgent, rushDestination);        
    }

    public void RushHit()
    {        
        IDamageable damageableObject = target.GetComponent<IDamageable>();
        //이거 나중에 인자 없애줘야함
        damageableObject.TakeHit(2);
        enemyAttack.KnockBack(target);
        isRushHit = true;
        
    }

    IEnumerator RushTimer()
    {
        yield return new WaitForSeconds(timeBetweenAttack);        
        isRushAvailable = true;
        //isRushHit = false;
        isOnRush = false;
        rushTimerCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(isOnRush==true)
        {
            isOnRush = false;
            //부딪힌게 Player면
            if (other.CompareTag("Player"))
            {                
                RushHit();
            }
        }        
    }

    private void ResetValue()
    {
        isRushAvailable = true;
        isOnRush = false;
        isRushHit = false;
        rushTimerCoroutine = null;
    }

}
