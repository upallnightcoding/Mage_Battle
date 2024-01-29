using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : FiniteState
{
    public static string TITLE = "Move";

    private HeroCntrl heroCntrl = null;
    private bool playerMoving = true;

    public PlayerMoveState(HeroCntrl heroCntrl) : base(TITLE)
    {
        this.heroCntrl = heroCntrl;
    }

    public override void OnEnter()
    {
        playerMoving = true;
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        if (heroCntrl.IsLeftMousePressed())
        {
            //nextState = heroCntrl.PlayerClickAndMove();
        }

        heroCntrl.UpdateAnimation();

        return (nextState);
    }

    /*public string xxxOnUpdate(float dt)
    {
        string nextState = null;

        if (playerMoving)
        {
            heroCntrl.PlayerClickAndMove();
        }

        if (heroCntrl.IsLeftMouseReleased())
        {
            playerMoving = false;
        } 

        if (!playerMoving && heroCntrl.HasReachedTarget())
        {
            heroCntrl.StopPlayer();
            nextState = PlayerIdleState.TITLE;
        }

        if (heroCntrl.GoOnAttack())
        {
            nextState = PlayerAttackState.TITLE;
        }

        return (nextState);
    }*/
}
