using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAttack : JoystickBase
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(state);
        switch (state)
        {
            case TouchState.Begin:
                state = TouchState.Stay;
                break;
            case TouchState.Stay:
                // 현재 handle 이동만큼 움직임전달
                break;
            case TouchState.Drag:
                state = TouchState.Stay;
                // 여기도 전달
                break;
            case TouchState.End:
                //손을 뗌
                state = TouchState.None;
                break;
            default:
                break;
        }
    }
}
