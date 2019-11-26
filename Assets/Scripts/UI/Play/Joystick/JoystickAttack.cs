using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAttack : JoystickBase
{
    //임시 공격속도
    private float attackRate = 1;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (attackRate < 0.5)
        //{
        //    attackRate += Time.fixedDeltaTime;
        //    return;
        //}

        switch (state)
        {
            case TouchState.Begin:
                state = TouchState.Stay;
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
        
        GameManager.Instance.PlayerAttack(moveDirection3D);
    }

    private void Standby()
    {
        GameManager.Instance.PlayerStandby();
    }
}
