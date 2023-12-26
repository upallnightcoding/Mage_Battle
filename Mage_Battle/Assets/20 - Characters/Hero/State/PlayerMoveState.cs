using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : FiniteState
{
    public static string TITLE = "Move";

    private HeroCntrl heroCntrl = null;

    public PlayerMoveState(HeroCntrl heroCntrl) : base(TITLE)
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

        heroCntrl.PlayerMovement(dt);

        if (heroCntrl.GoOnAttack())
        {
            nextState = PlayerAttackState.TITLE;
        }

        return (nextState);
    }
}
