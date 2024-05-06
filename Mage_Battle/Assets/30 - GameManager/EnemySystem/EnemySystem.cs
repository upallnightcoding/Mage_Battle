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

    // Contains a map of all existing enemies
    private Dictionary<int, GameObject> enemyMap;

    // Enemy index counter
    private int enemyId = 0;

    public Vector3 GetEnemyPosition() => selectedEnemyTarget.Position();
    public bool IsSelectedEnemy() => (selectedEnemyTarget != null);

    void Start()
    {
        enemyMap = new Dictionary<int, GameObject>();
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

        RaycastHit[] hits = Physics.SphereCastAll(position, 1.5f, transform.forward, 0.0f, enemyLayerMask);
        
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
    * SelectTarget() - Sets the "Target" as the selected target.  If the
    * selected target is already the target then it is skipped.  If the 
    * already selected target is pointing to something unselected what it 
    * is already pointing.  Set the selected target as the "Target" and 
    * face it in the direction of the player.
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
            selectedEnemyTarget.SetAsEnemyTarget(player.position);
        }
    }

    /**
     * KillEnemy() - 
     */
    private void KillEnemy(int enemyId)
    {
        if (enemyMap.TryGetValue(enemyId, out GameObject target))
        {
            enemyMap.Remove(enemyId);

            EnemyCntrl enemyCntrl = target.GetComponent<EnemyCntrl>();

            if (enemyCntrl.IsSelected)
            {
                Vector3 position = target.transform.position;
                target.gameObject.layer = LayerMask.NameToLayer("Default");
                selectedEnemyTarget = null;
                SelectEnemyTarget(position);
            } 
                
            Destroy(target);

            if (enemyMap.Count == 0)
            {
                UnSelectTarget();
            }

            EventSystem.Instance.InvokeOnAddXp(enemyCntrl.GetXp());
        }
    }

    /**
     * SpawnEnemy() - 
     */
    public void SpawnEnemy()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 position = XLib.System.RandomPoint(2.0f);
            GameObject newEnemy = skeleton.Spawn(player, player.transform.position + position);
            newEnemy.GetComponent<EnemyCntrl>().EnemyId = ++enemyId;
            
            enemyMap.Add(enemyId, newEnemy);
        }
    }

    private void OnKillEnemy(int enemyId)
    {
        KillEnemy(enemyId);
    }

    private void OnEnable()
    {
        EventSystem.Instance.OnKillEnemy += OnKillEnemy;
    }

    private void OnDisable()
    {
        EventSystem.Instance.OnKillEnemy -= OnKillEnemy;
    }
}
