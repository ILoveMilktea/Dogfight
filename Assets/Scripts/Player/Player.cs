using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RequireComponent : 요구되는 의존 컴포넌트 자동으로 이 스크립트를 추가한 게임오브젝트에 추가
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    //플레이어 상태
    public enum State { Idle, Attacking, Attacked, KnockBack };
    public float moveSpeed = 8;
    public bool isAttacking = false;
    public Animator animator;

    PlayerController controller;
    GunController gunController;
    Camera viewCamera;
    CooldownTimer cooldownTimer; // 스킬 쿨타임


    int count = 0;

    // Start is called before the first frame update
    public void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
        cooldownTimer = FindObjectOfType<CooldownTimer>(); // 스킬 쿨타임
        SetCooldownTimer();
    }


    // Update is called once per frame
    void Update()
    {
        // move 지워버림 ㅈㅅ

        //Look Input
        //카메라 시점에서 마우스의 위치 얻기
        //카메라에서 마우스 포인터위치로 Ray쏘기
        //Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayDistance;

        //out : Call by reference가능하게 하는 키워드(참조자 같은 느낌)
        //if(groundPlane.Raycast(ray,out rayDistance))
        //{
        //    Vector3 point = ray.GetPoint(rayDistance);
        //    //Debug.DrawLine(ray.origin, point, Color.red,3000f);
        //    controller.LookAt(point);
        //}

        //Weapon Input
        //if(Input.GetMouseButton(0))
        //{
        //    gunController.OnTirggerHold();
        //}
        //if(Input.GetMouseButtonUp(0))
        //{
        //    gunController.OnTriggerRelease();
        //}

    }

    private void SetCooldownTimer()
    {
        cooldownTimer.SetCooldownTime(5); // 임시 5초
        cooldownTimer.StartFight();
    }

    public void Move(Vector3 direction, float amount)
    {
        Vector3 moveVelocity = direction.normalized * moveSpeed;
        controller.Move(moveVelocity);
        if(!isAttacking)
        {
            controller.LookAt(transform.position + direction);
        }
        
        //Debug.Log("move" + direction);

        animator.SetBool("isMove", true); 
    }

    public void StopMove()
    {
        controller.Move(Vector3.zero);

        animator.SetBool("isMove", false); 
    }

    public void Attack(Vector3 direction)
    {
        //Debug.Log("attack" + direction);
        controller.LookAt(transform.position + direction);
        gunController.OnTirggerHold();
        //gunController.OnTriggerRelease(); // 연사를 위해 임시추가
    }

    public void SkillAttack(Vector3 direction)
    {
        controller.LookAt(transform.position + direction);
        // 여기서 guncontroller로 접속해서 스킬모드로 한방 쏴야하는데~~~
        if (cooldownTimer.IsSkillReady())
        {
            gunController.OnSkillTriggerHold();
            cooldownTimer.SkillUse();
        }
    }

    public void Standby()
    {
        gunController.OnTriggerRelease();
    }

    public WeaponType SwapWeapon()
    {
        return gunController.SwapWeapon();
    }


    public void PlayerDead()
    {
        StopMove();
        Standby();

        animator.SetTrigger("isDead");
    }
}
