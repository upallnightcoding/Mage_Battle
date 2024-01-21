using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : FiniteState
{
    public static string TITLE = "Idle";

    private SkeletonCntrl skeletonCntrl = null;

    public SkeletonIdleState(SkeletonCntrl skeletonCntrl) : base(TITLE)
    {
        this.skeletonCntrl = skeletonCntrl;
    }

    public override void OnEnter()
    {
        skeletonCntrl.SetSpeed(0.0f);
    }

    public override void OnExit()
    {

    }

    public override string OnUpdate(float dt)
    {
        string nextState = null;

        if (skeletonCntrl.WithinChaseArea())
        {
            nextState = SkeletonChaseState.TITLE;
        }

        return (nextState);
    }
}
