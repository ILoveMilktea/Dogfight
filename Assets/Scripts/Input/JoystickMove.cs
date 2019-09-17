using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMove : JoystickBase
{
    // Start is called before the first frame update
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

    //public override void OnPointerDown(PointerEventData data)
    //{
    //    base.OnPointerDown(data);
    //}

    //// Gameobject drag
    //public override void OnDrag(PointerEventData data)
    //{
    //    base.OnDrag(data);
    //}

    //// End touch on this gameobject
    //public override void OnPointerUp(PointerEventData data)
    //{
    //    base.OnPointerUp(data);
    //}

    
}
