using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCntrl : MonoBehaviour
{
    private Vector2 playerMovement;
    private Vector2 playerAim;

    public bool HasCast { set; get; } = false;
    public int SelectSpell { set; get; } = -1;
    public bool GoOnAttack { set; get; } = false;

    private float clickTimeStamp;

    // Player Movement & Aim Functions
    //--------------------------------
    public Vector2 GetPlayerMovement() => playerMovement;
    public Vector2 GetPlayerAim() => playerAim;

    // Mouse Device Functions
    //-----------------------
    public Vector2 GetMousePosition() => Mouse.current.position.ReadValue();
    //public bool IsLeftMousePressed() => Mouse.current.leftButton.isPressed;
    public bool IsLeftMousePressed() => Mouse.current.leftButton.wasPressedThisFrame;
    public bool IsLeftMouseReleased() => Mouse.current.leftButton.wasReleasedThisFrame;
    public bool IsRightMousePressed() => Mouse.current.rightButton.isPressed;
    public bool IsRightMouseReleased() => Mouse.current.rightButton.wasReleasedThisFrame;
    public int GetClickCount() => Mouse.current.clickCount.ReadValue();

    /*public bool IsLeftMouseClick()
    {
        bool click = true;

        return (click);
    }*/

    /**
     * HasSelectedSpell() - 
     */
    /*public bool HasSelectedSpell()
    {
        return (SelectSpell != -1);
    }*/

   
    /**
     * SetReadyForNextSpell() - Sets the input system ready to receive the
     * next spell cast.
     */
    public void SetReadyForNextSpell()
    {
        SelectSpell = -1;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMovement = context.ReadValue<Vector2>();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerAim = context.ReadValue<Vector2>();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HasCast = true;
        }
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnCast ...");
        }
    }

    public void OnCast1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectSpell = 0;
        }
    }

    public void OnCast2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectSpell = 1;
        }
    }

    public void GoOnAttck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GoOnAttack = true;
        }
    }

    private InputCntrlClickState currentClickState = InputCntrlClickState.IDLE_CLICK;

    public InputCntrlClickType GetClick()
    {
        InputCntrlClickType click = InputCntrlClickType.NO_CLICK;

        switch(currentClickState)
        {
            case InputCntrlClickState.IDLE_CLICK:
                currentClickState = IdleClickState();
                click = InputCntrlClickType.NO_CLICK;
                break;
            case InputCntrlClickState.TIMER_RUNNING:
                break;
            case InputCntrlClickState.SINGLE_CLICK:
                click = InputCntrlClickType.SINGLE_CLICK;
                currentClickState = InputCntrlClickState.IDLE_CLICK;
                break;
            case InputCntrlClickState.START_DRAG:
                click = InputCntrlClickType.START_DRAG_CLICK;
                currentClickState = InputCntrlClickState.DRAGING;
                break;
            case InputCntrlClickState.DRAGING:
                currentClickState = DraggingClick();
                click = InputCntrlClickType.DRAGGING_CLICK;
                break;
            case InputCntrlClickState.END_DRAG:
                currentClickState = InputCntrlClickState.IDLE_CLICK;
                click = InputCntrlClickType.END_DRAG_CLICK;
                break;
        }

        return (click);
    }

    private InputCntrlClickState DraggingClick()
    {
        InputCntrlClickState nextState = InputCntrlClickState.DRAGING;

        if (IsLeftMouseReleased())
        {
            nextState = InputCntrlClickState.END_DRAG;
        }

        return (nextState);
    }

    private InputCntrlClickState IdleClickState()
    {
        InputCntrlClickState nextState = InputCntrlClickState.IDLE_CLICK;

        if (IsLeftMousePressed())
        {
            StartCoroutine(StartClickTimer());
            nextState = InputCntrlClickState.TIMER_RUNNING;
        } 

        return (nextState);
    }

    private IEnumerator StartClickTimer()
    {
        float startTime = Time.time;
        bool mouseReleased = false;

        while (((Time.time - startTime) < 0.2f) && (!mouseReleased))
        {
            mouseReleased = IsLeftMouseReleased();

            yield return null;
        }

        if (mouseReleased)
        {
            currentClickState = InputCntrlClickState.SINGLE_CLICK;
        } 
        else
        {
            currentClickState = InputCntrlClickState.START_DRAG;
        }
    }

}

public enum InputCntrlClickType
{
    NO_CLICK,
    SINGLE_CLICK,
    START_DRAG_CLICK,
    DRAGGING_CLICK,
    END_DRAG_CLICK
}

public enum InputCntrlClickState
{
    IDLE_CLICK,
    TIMER_RUNNING,
    SINGLE_CLICK,
    START_DRAG,
    DRAGING,
    END_DRAG
}