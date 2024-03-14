using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : FiniteState
{
    public static string TITLE = "Idle";

    private EnemyCntrl enemyCntrl = null;

    public SkeletonIdleState(EnemyCntrl enemyCntrl) : base(TITLE)
    {
        this.enemyCntrl = enemyCntrl;
    }

    public override void OnEnter()
    {
        enemyCntrl.SetSpeed(0.0f);
    }

    public override void OnExit()
    {

    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        if (enemyCntrl.IsDead())
        {
            nextState = SkeletonDieState.TITLE;
        } 
        else if (enemyCntrl.WithinChaseArea())
        {
            nextState = SkeletonChaseState.TITLE;
        }

        return (nextState);
    }
}
