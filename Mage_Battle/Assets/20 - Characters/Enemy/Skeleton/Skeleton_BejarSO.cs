using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Mage Battle/Enemy/Skeleton Bejar")]
public class Skeleton_BejarSO : EnemySO
{
    public override FiniteStateMachine CreateFsm(EnemyCntrl enemyCntrl)
    {
        FiniteStateMachine fsm = new();

        fsm.Add(new SkeletonIdleState(enemyCntrl));
        fsm.Add(new SkeletonChaseState(enemyCntrl));
        fsm.Add(new SkeletonAttackState(enemyCntrl));
        fsm.Add(new SkeletonDieState(enemyCntrl));

        return (fsm);
    }

    public override void CastSpell(EnemyCntrl enemyCntrl, Vector3 release)
    {
        GameObject go = Instantiate(spellFXPreFab, release, Quaternion.identity);
        Vector3 direction = enemyCntrl.DirectionToPlayer();
        go.GetComponent<Rigidbody>().AddForce(direction * attackForce);
    }
}
