using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : FiniteState
{
    public static string TITLE = "Move";

    private HeroCntrl heroCntrl = null;
    private Vector3 position = Vector3.zero;
    private bool playerMoving = true;

    public PlayerMoveState(HeroCntrl heroCntrl) : base(TITLE)
    {
        this.heroCntrl = heroCntrl;
    }

    public override void OnEnter()
    {
        //position = heroCntrl.GetMousePosition();
        playerMoving = true;
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        if (playerMoving)
        {
            Debug.Log("Left Mouse Pressed ...");
            position = heroCntrl.GetMousePosition();
            heroCntrl.PlayerMovement(position);
        }

        if (heroCntrl.IsLeftMouseReleased())
        {
            Debug.Log("Left Mouse Released ...");
            playerMoving = false;
        } 

        if (!playerMoving && heroCntrl.HasReachedTarget())
        {
            Debug.Log("Has reached target ...");
            heroCntrl.StopPlayer();
            nextState = PlayerIdleState.TITLE;
        }

        /*if (heroCntrl.GoOnAttack())
        {
            nextState = PlayerAttackState.TITLE;
        }*/

        return (nextState);
    }
}
