using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public bool TriggerCancel()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public bool TriggerClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool TriggerGrab()
    {
        return Input.GetKeyDown(KeyCode.H);
    }

    public bool TriggerDelete()
    {
        return Input.GetKeyDown(KeyCode.Delete);
    }

    public bool ReleaseTriggerClick()
    {
        return Input.GetMouseButtonUp(0);
    }

    public bool PressingRotateObjectLeft()
    {
        return Input.GetKey(KeyCode.Q);
    }

    public bool PressingRotateObjectRight()
    {
        return Input.GetKey(KeyCode.E);
    }
}
