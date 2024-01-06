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

    // Player Movement & Aim Functions
    //--------------------------------
    public Vector2 GetPlayerMovement() => playerMovement;
    public Vector2 GetPlayerAim() => playerAim;

    // Mouse Device Functions
    //-----------------------
    public Vector2 GetMousePosition() => Mouse.current.position.ReadValue();
    public bool IsLeftMousePressed() => Mouse.current.leftButton.isPressed;
    public bool IsLeftMouseReleased() => Mouse.current.leftButton.wasReleasedThisFrame;
    public bool IsRightMousePressed() => Mouse.current.rightButton.isPressed;
    public bool IsRightMouseReleased() => Mouse.current.rightButton.wasReleasedThisFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * HasSelectedSpell() - 
     */
    public bool HasSelectedSpell()
    {
        return (SelectSpell != -1);
    }

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
}
