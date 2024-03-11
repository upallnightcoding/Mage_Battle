using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputCntrl : MonoBehaviour
{
    public static event Action OnDesengageEvent;

    [SerializeField] private float singleClickTimer = 0.1f;
    [SerializeField] private float doubleClickTimer = 0.2f;

    private SingleClickState currentSingleClickState = SingleClickState.IDLE_STATE;
    private SingleClickState prevCurrentSingleClickState = SingleClickState.IDLE_STATE;

    private Vector2 playerMovement;
    private Vector2 playerAim;

    //private InputCntrlClickType doubleClick = InputCntrlClickType.NO_CLICK;

    //public bool HasRequestToCast { set; get; } = false;
    public int SelectSpell { set; get; } = -1;
    public bool GoOnAttack { set; get; } = false;

    private float clickTimeStamp;
    private bool firstDoubleClick = true;

    // Player Movement & Aim Functions
    //--------------------------------
    public Vector2 GetPlayerMovement() => playerMovement;
    public Vector2 GetPlayerAim() => playerAim;

    // Mouse Device Functions
    //-----------------------
    public Vector2 GetMousePosition() => Mouse.current.position.ReadValue();
    private int GetClickCount() => Mouse.current.clickCount.ReadValue();
    public bool IsLeftMousePressed() => Mouse.current.leftButton.wasPressedThisFrame;
    public bool IsLeftMouseReleased() => Mouse.current.leftButton.wasReleasedThisFrame;
    //public bool IsRightMousePressed() => Mouse.current.rightButton.isPressed;
    //public bool IsRightMouseReleased() => Mouse.current.rightButton.wasReleasedThisFrame;

    public bool HasSelectedSpell() => SelectSpell != -1;

    public void ReSetSelectedSpell() => SelectSpell = -1;

    public void OnMove(InputAction.CallbackContext context)
    {
        /*if (context.performed)
        {
            playerMovement = context.ReadValue<Vector2>();
        }*/
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        /*if (context.performed)
        {
            playerAim = context.ReadValue<Vector2>();
        }*/
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //HasRequestToCast = true;
        }
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //HasRequestToCast = true;
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

    public void OnCast3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectSpell = 2;
        }
    }

    public void OnCast4(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectSpell = 3;
        }
    }

    public void GoOnAttck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GoOnAttack = true;
        }
    }

    public void OnDesengage(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDesengageEvent.Invoke();
        }
    }

    public InputCntrlClickType GetClick()
    {
        InputCntrlClickType click = InputCntrlClickType.NO_CLICK;
        
        if (SelectSpell != -1)
        {
            currentSingleClickState = SingleClickState.FIRE_STATE;
        }

        if (currentSingleClickState != prevCurrentSingleClickState)
        {
            Debug.Log($"Current Click: {currentSingleClickState}");
            prevCurrentSingleClickState = currentSingleClickState;
        }

        switch (currentSingleClickState)
        {
            case SingleClickState.IDLE_STATE:
                currentSingleClickState = IdleSingleClickState();
                click = InputCntrlClickType.NO_CLICK;
                break;
            case SingleClickState.DOUBLE_CLICK_STATE:
                currentSingleClickState = SingleClickState.IDLE_STATE;
                click = InputCntrlClickType.DOUBLE_CLICK;
                break;
            case SingleClickState.FIRE_STATE:
                click = InputCntrlClickType.FIRE_CLICK;
                currentSingleClickState = SingleClickState.IDLE_STATE;
                break;
            case SingleClickState.TIME_RUNNING_STATE:
                break;
            case SingleClickState.SINGLE_CLICK_STATE:
                click = InputCntrlClickType.SINGLE_CLICK;
                currentSingleClickState = SingleClickState.IDLE_STATE;
                break;
            case SingleClickState.START_DRAG_STATE:
                click = InputCntrlClickType.START_DRAG_CLICK;
                currentSingleClickState = SingleClickState.DRAGGING_STATE;
                break;
            case SingleClickState.DRAGGING_STATE:
                currentSingleClickState = DraggingClick();
                click = InputCntrlClickType.DRAGGING_CLICK;
                break;
            case SingleClickState.END_DRAG_STATE:
                currentSingleClickState = SingleClickState.IDLE_STATE;
                click = InputCntrlClickType.END_DRAG_CLICK;
                break;
        }

        return (click);
    }

    private SingleClickState DraggingClick()
    {
        SingleClickState nextState = SingleClickState.DRAGGING_STATE;

        if (IsLeftMouseReleased())
        {
            nextState = SingleClickState.END_DRAG_STATE;
        }

        return (nextState);
    }

    private SingleClickState IdleSingleClickState()
    {
        SingleClickState nextState = SingleClickState.IDLE_STATE;

        if (IsLeftMousePressed())
        {
            StartCoroutine(StartSingleClickTimer());
            nextState = SingleClickState.TIME_RUNNING_STATE;
        } 

        return (nextState);
    }

    /**
     * StartSingleClickTimer() -
     */
    private IEnumerator StartSingleClickTimer()
    {
        float startTime = Time.time;
        bool mouseReleased = false;

        while (((Time.time - startTime) < singleClickTimer) && (!mouseReleased))
        {
            mouseReleased = IsLeftMouseReleased();

            yield return null;
        }

        if (mouseReleased)
        {
            startTime = Time.time;
            bool mousePressed = false;
            while (((Time.time - startTime) < singleClickTimer) && (!mousePressed))
            {
                mousePressed = IsLeftMousePressed();

                yield return null;
            }

            currentSingleClickState = 
                (mousePressed) ? SingleClickState.DOUBLE_CLICK_STATE : SingleClickState.SINGLE_CLICK_STATE;
        } 
        else
        {
            currentSingleClickState = SingleClickState.START_DRAG_STATE;
        }

        //currentSingleClickState = 
          //  (mouseReleased) ? SingleClickState.SINGLE_CLICK_STATE : SingleClickState.START_DRAG_STATE;
    }

  
}

public enum InputCntrlClickType
{
    NO_CLICK,
    SINGLE_CLICK,
    DOUBLE_CLICK,
    START_DRAG_CLICK,
    DRAGGING_CLICK,
    END_DRAG_CLICK,
    FIRE_CLICK
}

public enum SingleClickState
{
    IDLE_STATE,
    TIME_RUNNING_STATE,
    SINGLE_CLICK_STATE,
    START_DRAG_STATE,
    DRAGGING_STATE,
    END_DRAG_STATE,
    DOUBLE_CLICK_STATE,
    FIRE_STATE
}