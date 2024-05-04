using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDieState : FiniteState
{
    public static string TITLE = "Die";

    private EnemyCntrl enemyCntrl = null;

    public SkeletonDieState(EnemyCntrl enemyCntrl) : base(TITLE)
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
        
        if(enemyCntrl.IsDead())
        {
            enemyCntrl.KillEnemy();
        }

        return (nextState);
    }
}
