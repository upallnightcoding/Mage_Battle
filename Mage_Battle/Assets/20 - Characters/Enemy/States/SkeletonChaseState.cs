using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseState : FiniteState
{
    public static string TITLE = "Chase";

    private SkeletonCntrl skeletonCntrl = null;

    public SkeletonChaseState(SkeletonCntrl skeletonCntrl) : base(TITLE)
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
            nextState = SkeletonAttackState.TITLE;
        } 
        else if (skeletonCntrl.WithinFollowArea())
        {
            skeletonCntrl.MovesTowardPlayer(dt);
        } 
        else
        {
            nextState = SkeletonIdleState.TITLE;
        }

        return (nextState);
    }
}
