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
        
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        if (heroCntrl.IsLeftMousePressed())
        {
            nextState = PlayerMoveState.TITLE;
        } 

        return (nextState);
    }
}
