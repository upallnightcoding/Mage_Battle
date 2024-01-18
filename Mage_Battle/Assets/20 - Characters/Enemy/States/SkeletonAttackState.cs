using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : FiniteState
{
    public static string TITLE = "Attack";

    private SkeletonCntrl skeletonCntrl = null;

    public SkeletonAttackState(SkeletonCntrl skeletonCntrl) : base(TITLE)
    {
        this.skeletonCntrl = skeletonCntrl;
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

        if (skeletonCntrl.WithinAttackArea())
        {
            skeletonCntrl.TriggerAttack(true);
            //skeletonCntrl.MovesTowardPlayer(dt);
        }
        else if (skeletonCntrl.WithinFollowArea())
        {
            skeletonCntrl.TriggerAttack(false);
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
