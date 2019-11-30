using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickAttack : JoystickBase
{
    public Image equipingWeapon;
    private bool isSwap;

    protected override void Start()
    {
        base.Start();
        isSwap = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case TouchState.Begin:
                state = TouchState.Stay;
                isSwap = true;
                break;
            case TouchState.Stay:
                Attack();
                break;
            case TouchState.Drag:
                state = TouchState.Stay;
                Attack();
                break;
            case TouchState.End:
                Standby();
                CheckSwap();
                state = TouchState.None;
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        Vector2 attackDirection = handle.position - border.position;
        attackDirection.Normalize();
        Vector3 moveDirection3D = new Vector3(attackDirection.x, 0, attackDirection.y);

        float moveAmount = Vector2.Distance(border.position, handle.position);
        moveAmount = moveAmount / handleMoveRange;

        if (moveAmount > 0.5f)
        {
            isSwap = false;
            // joystick handle의 이동 범위가 반을 넘어가야 움직이는거
            FightSceneController.Instance.LookMoveRotate();
            FightSceneController.Instance.PlayerAttack(moveDirection3D);
        }
        else
        {
            Standby();
        }
    }

    private void Standby()
    {
        FightSceneController.Instance.PlayerStandby();
        FightSceneController.Instance.UnLookMoveRotate();
    }

    private void CheckSwap()
    {
        if (isSwap)
        {
            FightSceneController.Instance.SwapWeapon();
        }
        isSwap = false;
    }

    public void WeaponImageSwap(WeaponType weapon)
    {
        equipingWeapon.sprite = Resources.Load<Sprite>("Image/Weapon/" + weapon.ToString());
    }
}
