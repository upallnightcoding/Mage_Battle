using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : FiniteState
{
    public static string TITLE = "Idle";

    private HeroCntrl heroCntrl = null;

    public PlayerIdleState(HeroCntrl heroCntrl) : base(TITLE)
    {
        this.heroCntrl = heroCntrl;
    }

    public override void OnEnter()
    {
        heroCntrl.StopAnimation();
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        if (heroCntrl.IsLeftMousePressed())
        {
            Debug.Log("Goto Move State");
            nextState = PlayerMoveState.TITLE;
        } else
        {
            //heroCntrl.PlayerMovement();
        }

        return (nextState);
    }
}
