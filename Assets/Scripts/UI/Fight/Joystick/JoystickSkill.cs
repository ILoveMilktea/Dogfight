﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickSkill : JoystickBase
{
    public Sprite defaultSkillImage;
    public bool isSkillEquiped = false;

    public override void OnPointerUp(PointerEventData data)
    {
        state = TouchState.End;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case TouchState.Begin:
                state = TouchState.Stay;
                break;
            case TouchState.Stay:
                break;
            case TouchState.Drag:
                state = TouchState.Stay;
                break;
            case TouchState.End:
                UseSkill();
                Standby();
                state = TouchState.None;
                break;
            default:
                break;
        } 
    }

    private void UseSkill()
    {
        Vector2 attackDirection = handle.position - border.position;
        attackDirection.Normalize();
        Vector3 moveDirection3D = new Vector3(attackDirection.x, 0, attackDirection.y);

        float moveAmount = Vector2.Distance(border.position, handle.position);
        moveAmount = moveAmount / handleMoveRange;

        if (moveAmount > 0.5f && isSkillEquiped)
        {
            // joystick handle의 이동 범위가 반을 넘어가야 움직이는거
            FightSceneController.Instance.PlayerSkill(moveDirection3D);
        }

    }

    public void SkillOn(Sprite sprite)
    {
        isSkillEquiped = true;
        handle.GetComponent<Image>().sprite = sprite;
    }
    public void SkillOff()
    {
        isSkillEquiped = false;
        handle.GetComponent<Image>().sprite = defaultSkillImage;
    }


    private void Standby()
    {
        touchPos = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        FightSceneController.Instance.PlayerStandby();
    }

}
