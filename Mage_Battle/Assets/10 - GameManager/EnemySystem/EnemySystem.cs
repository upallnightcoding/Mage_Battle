using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XLib;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private EnemySO skeleton;

    private EnemyCntrl selectedEnemyTarget = null;

    public Vector3 GetEnemyDirection() => (selectedEnemyTarget.Position() - player.position).normalized ;
    public Vector3 GetEnemyPosition() => selectedEnemyTarget.Position();
    public bool IsLockedOn() => (selectedEnemyTarget != null);

    void Start()
    {
        SpawnEnemy();
    }

    /**
     * UnSelectTarget() - Unselects the current selected enemy object.  The 
     * object looses its selection icon and moves out of its attack state.
     */
    public void UnSelectTarget()
    {
        if (selectedEnemyTarget != null)
        {
            selectedEnemyTarget.UnSetAttackMode();
            selectedEnemyTarget = null;
        }
    }

    /**
     * SelectEnemyTarget() - Selects the enemy target based on the mouse 
     * click position.  This function chooses any of the enemies that
     * are in casting range.  If there are no enemies in range, this 
     * function returns null.
     */
    public bool SelectEnemyTarget(Vector3 position)
    {
        bool selected = false;

        RaycastHit[] hits = Physics.SphereCastAll(position, 1.0f, transform.forward, 0.0f, enemyLayerMask);

        // If a target is in range, select the target
        if (hits.Length > 0)
        {
            selected = true;
            GameObject go = hits[0].transform.gameObject;
            EnemyCntrl target = go.GetComponent<EnemyCntrl>();
            SelectTarget(target);
        }

        return (selected);
    }

    /**
    * SelectTarget() - 
    */
    private void SelectTarget(EnemyCntrl target)
    {
        if (selectedEnemyTarget != target)
        {
            if (selectedEnemyTarget != null)
            {
                selectedEnemyTarget.UnSetAttackMode();
            }

            selectedEnemyTarget = target;
            selectedEnemyTarget.SetAttackMode(player.position);
        }
    }

    /**
     * SpawnEnemy() - 
     */
    private void SpawnEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 position = XLib.System.RandomPoint(5.0f);
            skeleton.Spawn(player, position);
        }
    }
}
