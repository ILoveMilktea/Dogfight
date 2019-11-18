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
    public float moveSpeed = 5;

    PlayerController controller;
    GunController gunController;
    Camera viewCamera;

    int count = 0;
    private WeaponType equipingWeapon;

    // Start is called before the first frame update
    public void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;

        equipingWeapon = WeaponType.LinearGun;
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

    public void Move(Vector3 direction, float amount)
    {
        Vector3 moveVelocity = direction.normalized * moveSpeed;
        controller.Move(moveVelocity);
        controller.LookAt(transform.position + direction);
        //Debug.Log("move" + direction);

        GetComponent<Animator>().SetBool("isMove", true); //임시 애니메이팅, 나중에 지움
    }

    public void StopMove()
    {
        controller.Move(Vector3.zero);

        GetComponent<Animator>().SetBool("isMove", false); //임시 애니메이팅, 나중에 지움
    }

    public void Attack(Vector3 direction)
    {
        //Debug.Log("attack" + direction);
        controller.LookAt(transform.position + direction);
        gunController.OnTirggerHold();
        //gunController.OnTriggerRelease(); // 연사를 위해 임시추가
    }

    public void Standby()
    {
        gunController.OnTriggerRelease();
    }

    // swappppp
    public WeaponType SwapWeapon()
    {
        if (equipingWeapon == WeaponType.ShotGun)
        {
            equipingWeapon = WeaponType.EnergySphereGun;
        }
        else
        {
            equipingWeapon++;
        }

        gunController.EquipGun(Resources.Load<Gun>("Prefab/Weapon/" + equipingWeapon.ToString()));

        return equipingWeapon;
    }
}
