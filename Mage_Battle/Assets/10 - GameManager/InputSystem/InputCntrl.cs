using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputCntrl : MonoBehaviour
{
    public static event Action OnDesengageEvent;

    [SerializeField] private float singleLeftClickTimer = 0.1f;
    [SerializeField] private float singleRightClickTimer = 0.1f;
    [SerializeField] private float doubleClickTimer = 0.2f;

    private SingleClickState currentSingleClickState = SingleClickState.IDLE_STATE;
    private SingleClickState prevCurrentSingleClickState = SingleClickState.IDLE_STATE;

    public int SelectSpell { set; get; } = -1;
    public bool GoOnAttack { set; get; } = false;

    // Mouse Device Functions
    //-----------------------
    public Vector2 GetMousePosition() => Mouse.current.position.ReadValue();
    public bool IsLeftMousePressed() => Mouse.current.leftButton.wasPressedThisFrame;
    public bool IsLeftMouseReleased() => Mouse.current.leftButton.wasReleasedThisFrame;
    public bool IsRightMousePressed() => Mouse.current.rightButton.wasPressedThisFrame;
    public bool IsRightMouseReleased() => Mouse.current.rightButton.wasReleasedThisFrame;

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
                currentSingleClickState =
                    (prevCurrentSingleClickState == SingleClickState.DRAGGING_LEFT_STATE) 
                    ? SingleClickState.DRAGGING_LEFT_STATE : SingleClickState.IDLE_STATE;
                break;

            case SingleClickState.TIME_RUNNING_STATE:
                break;

            case SingleClickState.SINGLE_LEFT_CLICK_STATE:
                click = InputCntrlClickType.SINGLE_LEFT_CLICK;
                currentSingleClickState = SingleClickState.IDLE_STATE;
                break;
            case SingleClickState.START_LEFT_DRAG_STATE:
                click = InputCntrlClickType.START_LEFT_DRAG_CLICK;
                currentSingleClickState = SingleClickState.DRAGGING_LEFT_STATE;
                break;
            case SingleClickState.DRAGGING_LEFT_STATE:
                currentSingleClickState = DraggingLeftClick();
                click = InputCntrlClickType.DRAGGING_LEFT_CLICK;
                break;
            case SingleClickState.END_LEFT_DRAG_STATE:
                currentSingleClickState = SingleClickState.IDLE_STATE;
                click = InputCntrlClickType.END_DRAG_LEFT_CLICK;
                break;

            case SingleClickState.SINGLE_RIGHT_CLICK_STATE:
                click = InputCntrlClickType.SINGLE_RIGHT_CLICK;
                currentSingleClickState = SingleClickState.IDLE_STATE;
                break;
            case SingleClickState.START_RIGHT_DRAG_STATE:
                click = InputCntrlClickType.START_RIGHT_DRAG_CLICK;
                currentSingleClickState = SingleClickState.DRAGGING_RIGHT_STATE;
                break;
            case SingleClickState.DRAGGING_RIGHT_STATE:
                currentSingleClickState = DraggingRightClick();
                click = InputCntrlClickType.DRAGGING_RIGHT_CLICK;
                break;
            case SingleClickState.END_RIGHT_DRAG_STATE:
                currentSingleClickState = SingleClickState.IDLE_STATE;
                click = InputCntrlClickType.END_DRAG_RIGHT_CLICK;
                break;
        }

        prevCurrentSingleClickState = currentSingleClickState;

        return (click);
    }

    private SingleClickState DraggingLeftClick()
    {
        SingleClickState nextState = SingleClickState.DRAGGING_LEFT_STATE;

        if (IsLeftMouseReleased())
        {
            nextState = SingleClickState.END_LEFT_DRAG_STATE;
        }

        return (nextState);
    }

    private SingleClickState DraggingRightClick()
    {
        SingleClickState nextState = SingleClickState.DRAGGING_RIGHT_STATE;

        if (IsRightMouseReleased())
        {
            nextState = SingleClickState.END_RIGHT_DRAG_STATE;
        }

        return (nextState);
    }

    private SingleClickState IdleSingleClickState()
    {
        SingleClickState nextState = SingleClickState.IDLE_STATE;

        if (IsLeftMousePressed())
        {
            StartCoroutine(StartSingleLeftClickTimer());
            nextState = SingleClickState.TIME_RUNNING_STATE;
        }

        if (IsRightMousePressed())
        {
            StartCoroutine(StartSingleRightClickTimer());
            nextState = SingleClickState.TIME_RUNNING_STATE;
        }

        return (nextState);
    }

    /**
     * StartSingleClickTimer() -
     */
    private IEnumerator StartSingleLeftClickTimer()
    {
        float startTime = Time.time;
        bool mouseReleased = false;

        while (((Time.time - startTime) < singleLeftClickTimer) && (!mouseReleased))
        {
            mouseReleased = IsLeftMouseReleased();

            yield return null;
        }

        if (mouseReleased)
        {
            startTime = Time.time;
            bool mousePressed = false;
            while (((Time.time - startTime) < singleLeftClickTimer) && (!mousePressed))
            {
                mousePressed = IsLeftMousePressed();

                yield return null;
            }

            currentSingleClickState = 
                (mousePressed) ? SingleClickState.DOUBLE_CLICK_STATE : SingleClickState.SINGLE_LEFT_CLICK_STATE;
        } 
        else
        {
            currentSingleClickState = SingleClickState.START_LEFT_DRAG_STATE;
        }
    }

    private IEnumerator StartSingleRightClickTimer()
    {
        float startTime = Time.time;
        bool mouseReleased = false;

        while (((Time.time - startTime) < singleRightClickTimer) && (!mouseReleased))
        {
            mouseReleased = IsRightMouseReleased();

            yield return null;
        }

        if (mouseReleased)
        {
            startTime = Time.time;
            bool mousePressed = false;
            while (((Time.time - startTime) < singleRightClickTimer) && (!mousePressed))
            {
                mousePressed = IsRightMousePressed();

                yield return null;
            }

            currentSingleClickState =
                (mousePressed) ? SingleClickState.DOUBLE_CLICK_STATE : SingleClickState.SINGLE_RIGHT_CLICK_STATE;
        }
        else
        {
            currentSingleClickState = SingleClickState.START_RIGHT_DRAG_STATE;
        }
    }
}

public enum InputCntrlClickType
{
    NO_CLICK,

    SINGLE_LEFT_CLICK,
    START_LEFT_DRAG_CLICK,
    DRAGGING_LEFT_CLICK,
    END_DRAG_LEFT_CLICK,

    SINGLE_RIGHT_CLICK,
    START_RIGHT_DRAG_CLICK,
    DRAGGING_RIGHT_CLICK,
    END_DRAG_RIGHT_CLICK,

    DOUBLE_CLICK,

    FIRE_CLICK
}

public enum SingleClickState
{
    IDLE_STATE,
    DOUBLE_CLICK_STATE,
    FIRE_STATE,

    TIME_RUNNING_STATE,
    SINGLE_LEFT_CLICK_STATE,
    START_LEFT_DRAG_STATE,
    DRAGGING_LEFT_STATE,
    END_LEFT_DRAG_STATE,

    SINGLE_RIGHT_CLICK_STATE,
    START_RIGHT_DRAG_STATE,
    DRAGGING_RIGHT_STATE,
    END_RIGHT_DRAG_STATE
}