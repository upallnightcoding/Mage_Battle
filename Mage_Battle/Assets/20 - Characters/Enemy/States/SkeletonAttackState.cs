using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : FiniteState
{
    public static string TITLE = "Attack";

    private EnemyCntrl enemyCntrl = null;

    public SkeletonAttackState(EnemyCntrl enemyCntrl) : base(TITLE)
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

        if (enemyCntrl.IsDead())
        {
            nextState = SkeletonDieState.TITLE;
        }
        else if (enemyCntrl.WithinAttackArea())
        {
            enemyCntrl.TriggerAttack(true);
            //skeletonCntrl.MovesTowardPlayer(dt);
        }
        else if (enemyCntrl.WithinChaseArea())
        {
            enemyCntrl.TriggerAttack(false);
            //skeletonCntrl.MovesTowardPlayer(dt);
            nextState = SkeletonChaseState.TITLE;
        }
        else
        {
            nextState = SkeletonIdleState.TITLE;
        }

        return (nextState);
    }
}
