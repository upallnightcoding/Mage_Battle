using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : FiniteState
{
    public static string TITLE = "Attack";

    private HeroCntrl heroCntrl = null;

    public PlayerAttackState(HeroCntrl heroCntrl) : base(TITLE)
    {
        this.heroCntrl = heroCntrl;
    }

    public override void OnEnter()
    {
        heroCntrl.StartAttack();
    }

    public override void OnExit()
    {
        heroCntrl.EndAttack();
    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        heroCntrl.PlayerAttack(dt);

        return (nextState);
    }
}
