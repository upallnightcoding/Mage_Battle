using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * How to create an enemy ...
 * 
 * 1. Define a type and name for your enemy: Soldier_Allard
 * 2. Create Enemy Mode PreFab: Soldier_Allard
 * 3. Create the SO file: Soldier_AllardSO
 * 4 [CreateAssetMenu(fileName = "Enemy", menuName = "Mage Battle/Enemy/Soldier Allard")]
 * 5 Base Class: EnemySO
 * 6. 
 */

public abstract class EnemySO : ScriptableObject
{
    /*************************************/
    /*** Abstract Function Definitions ***/
    /*************************************/

    // Spawn Enemy
    public abstract FiniteStateMachine CreateFsm(EnemyCntrl enemyCntrl);
    public abstract void CastSpell(EnemyCntrl enemyCntrl, Vector3 release);

    /***********************************/
    /*** Public Property Definitions ***/
    /***********************************/

    // Enemy Name
    public string enemyName;

    // PreFab of the enemy
    public GameObject enemyPreFab;

    // PreFab PS - Played when enemy is spawned
    public GameObject spawnFXPreFab;

    [Header("Combat")]

    // Size a area an enemy will follow
    public float followArea;   

    // Size of area an enemy will attack
    public float attackArea;

    // Amount of force to apply to a projectile spell
    public float attackForce;

    // PreFab PS - Instantiated to show enemy selection
    public GameObject selectionPreFab;

    // PreFab PS - Played when a spell is casted
    public GameObject spellFXPreFab;

    // Experience points
    public int expPoints;

    /************************/
    /*** Public Functions ***/
    /************************/

    public FiniteStateMachine Behavior(EnemyCntrl enemyCntrl)
    {
        return(CreateFsm(enemyCntrl));
    }

    public GameObject Spawn(Transform player, Vector3 position)
    {
        if (spawnFXPreFab != null)
        {
            GameObject fx = Instantiate(spawnFXPreFab, position, Quaternion.identity);
            Destroy(fx, 3.0f);
        }

        GameObject skeleton = Instantiate(enemyPreFab, position, Quaternion.identity);
        skeleton.GetComponent<EnemyCntrl>().Player = player;

        return (skeleton);
    }
}
