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
    private Dictionary<int, GameObject> enemyMap;

    private int enemyId = 0;

    //public Vector3 GetEnemyDirection() => (selectedEnemyTarget.Position() - player.position).normalized ;
    public Vector3 GetEnemyPosition() => selectedEnemyTarget.Position();
    public bool IsSelectedEnemy() => (selectedEnemyTarget != null);

    void Start()
    {
        enemyMap = new Dictionary<int, GameObject>();

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

    private void KillSelectedEnemy(int enemyId)
    {
        Debug.Log("EnemyId: " + enemyId);

        if(enemyMap.TryGetValue(enemyId, out GameObject target))
        {
            if (target.GetComponent<EnemyCntrl>().IsSelected)
            {
                selectedEnemyTarget = null;
            }

            Destroy(target);
        }
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
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = XLib.System.RandomPoint(5.0f);
            GameObject newEnemy = skeleton.Spawn(player, position);
            newEnemy.GetComponent<EnemyCntrl>().EnemyId = ++enemyId;
            newEnemy.GetComponent<EnemyCntrl>().OnKillEnemy += KillSelectedEnemy;
            enemyMap.Add(enemyId, newEnemy);
        }
    }
}
