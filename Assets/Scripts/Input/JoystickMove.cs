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
        switch (state)
        {
            case TouchState.Begin:
                state = TouchState.Stay;
                break;
            case TouchState.Stay:
                // 현재 handle 이동만큼 움직임전달
                PlayerMove();
                break;
            case TouchState.Drag:
                state = TouchState.Stay;
                // 여기도 전달
                PlayerMove();
                break;
            case TouchState.End:
                //손을 뗌
                state = TouchState.None;
                //GameManager.Instance.StopPlayer();
                break;
            default:
                break;
        }
    }

    private void PlayerMove()
    {
        Vector2 moveDirection = handle.position - border.position;
        moveDirection.Normalize();
        Vector3 moveDirection3D = new Vector3(moveDirection.x, 0, moveDirection.y);

        float moveAmount = Vector2.Distance(border.position, handle.position);
        moveAmount = moveAmount / handleMoveRange;
        GameManager.Instance.MovePlayer(moveDirection3D, moveAmount);
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
