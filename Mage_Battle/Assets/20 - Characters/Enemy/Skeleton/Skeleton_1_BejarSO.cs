using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Mage Battle/Enemy/Skeleton Bejar (1)")]
public class Skeleton_1_BejarSO : EnemySO
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

    public override GameObject Spawn(Transform player, Vector3 position)
    {
        //Debug.Log($"Player: {player}/{position}");
        GameObject fx = Instantiate(spawnFXPreFab, position, Quaternion.identity);
        Destroy(fx, 3.0f);

        GameObject skeleton = Instantiate(enemyPreFab, position, Quaternion.identity);
        skeleton.GetComponent<EnemyCntrl>().Player = player;

        return (skeleton);
    }

    public override void CastSpell(EnemyCntrl enemyCntrl, Vector3 release)
    {
        GameObject go = Instantiate(spellFXPreFab, release, Quaternion.identity);
        Vector3 direction = enemyCntrl.DirectionToPlayer();
        go.GetComponent<Rigidbody>().AddForce(direction * attackForce);
    }
}
