using UnityEngine;
using UnityEngine.UI;

public enum InputAxis
{
    X,
    Y,
    Z
}

public interface IDFInput
{
    bool GetTouchDown(int pointer);
    bool GetTouchUp(int pointer);
    bool GetTouch(int pointer);


    float GetAxis(InputAxis axis);

    Vector2 GetPointerXY(int pointer);

    bool IsAnyKeyDown();
}

public class DisabledInput : IDFInput
{
    public bool GetTouchDown(int pointer)
    {
        return false;
    }

    public bool GetTouchUp(int pointer)
    {
        return false;
    }

    public bool GetTouch(int pointer)
    {
        return false;
    }

    public float GetAxis(InputAxis axis)
    {
        return 0;
    }

    public Vector2 GetPointerXY(int pointer)
    {
        Touch touch = Input.GetTouch(pointer);
        return touch.position;
    }

    public bool IsAnyKeyDown()
    {
        return false;
    }
}

public class DFInput : IDFInput
{
    public virtual bool IsAnyKeyDown()
    {
        return Input.anyKeyDown;
    }
    public virtual float GetAxis(InputAxis axis)
    {
        switch (axis)
        {
            case InputAxis.X:
                return Input.GetAxis("Vertical");
            case InputAxis.Y:
                return Input.GetAxis("Horizontal");
            case InputAxis.Z:
                if (Input.touchCount == 2)
                {
                    Vector2 cur = Input.GetTouch(0).position - Input.GetTouch(1).position;
                    Vector2 prev = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
                    return cur.magnitude - prev.magnitude;
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }
    }

    public virtual Vector2 GetPointerXY(int pointer)
    {
        Touch touch = Input.GetTouch(pointer);
        return touch.position;
    }

    public virtual bool GetTouchDown(int pointer)
    {
        if (Input.GetTouch(pointer).phase == TouchPhase.Began)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool GetTouchUp(int pointer)
    {
        if (Input.GetTouch(pointer).phase == TouchPhase.Ended)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool GetTouch(int pointer)
    {
        if (Input.GetTouch(pointer).phase == TouchPhase.Stationary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}