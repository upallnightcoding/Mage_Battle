using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalkState : FiniteState
{
    public static string TITLE = "Walk";

    private SkeletonCntrl skeletonCntrl = null;

    public SkeletonWalkState(SkeletonCntrl skeletonCntrl) : base(TITLE)
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

        if (skeletonCntrl.WithinFollowArea())
        {
            skeletonCntrl.MovesTowardPlayer(dt);
        } else
        {
            nextState = SkeletonIdleState.TITLE;
        }

        return (nextState);
    }
}
