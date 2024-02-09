using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XLib;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform player;

    [SerializeField] private EnemySO skeleton;

    private Dictionary<int, GameObject> enemyMap = null;

    private int enemyId = 1;

    private SkeletonCntrl targetEnemy = null;

    void Start()
    {
        this.enemyMap = new Dictionary<int, GameObject>();

        CreateEnemy();
    }

    /**
     * SelectTarget() - 
     */
    public void SelectTarget(Vector3 player, SkeletonCntrl target)
    {
        if (targetEnemy != target)
        {
            if (targetEnemy != null)
            {
                targetEnemy.UnSetAttackMode();
            } 
        
            targetEnemy = target;
            targetEnemy.SetAttackMode(player);
        }
    }

    public void UnSelectTarget()
    {
        targetEnemy.UnSetAttackMode();
        targetEnemy = null;
    }

    private void CreateEnemy()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 position = XLib.System.RandomPoint(5.0f);
            GameObject go = skeleton.Spawn(player, position);

            go.GetComponent<SkeletonCntrl>().SetEnemyId(++enemyId);
            enemyMap.Add(enemyId, go);
        }
    }
}
