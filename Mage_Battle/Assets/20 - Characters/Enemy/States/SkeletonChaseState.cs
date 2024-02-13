using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseState : FiniteState
{
    public static string TITLE = "Chase";

    private EnemyCntrl enemyCntrl = null;

    public SkeletonChaseState(EnemyCntrl enemyCntrl) : base(TITLE)
    {
        this.enemyCntrl = enemyCntrl;
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

        if (enemyCntrl.WithinAttackArea())
        {
            nextState = SkeletonAttackState.TITLE;
        } 
        else if (enemyCntrl.WithinChaseArea())
        {
            enemyCntrl.MovesTowardPlayer();
        } 
        else
        {
            nextState = SkeletonIdleState.TITLE;
        }

        return (nextState);
    }
}
