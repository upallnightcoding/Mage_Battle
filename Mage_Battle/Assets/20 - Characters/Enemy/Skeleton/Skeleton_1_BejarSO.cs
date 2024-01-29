using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Mage Battle/Enemy/Skeleton Bejar")]
public class Skeleton_1_BejarSO : EnemySO
{
    public override GameObject Spawn(Transform player, Vector3 position)
    {
        GameObject fx = Instantiate(spawnFXPreFab, position, Quaternion.identity);
        Destroy(fx, 3.0f);

        GameObject skeleton = Instantiate(enemyPreFab, position, Quaternion.identity);
        skeleton.GetComponent<SkeletonCntrl>().Player = player;

        return (skeleton);
    }
}
