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

[CreateAssetMenu(fileName = "Enemy", menuName ="Mage Battle/Enemy")]
public class EnemySO : ScriptableObject
{
    /***********************************/
    /*** Public Property Definitions ***/
    /***********************************/

    // Enemy Name
    public string enemyName;

    // Experience points
    public int xp;

    // PreFab of the enemy
    public GameObject enemyPrefab;

    // PreFab PS - Played when enemy is spawned
    public GameObject spawnFxEnemyPrefab;

    // PreFab PS - Played when a spell is casted
    public GameObject spellFxPreFab;

    // PreFab PS - Instantiated to show enemy selection
    public GameObject enemySelectPrefab;

    // Size a area an enemy will follow
    public float followArea;   

    // Size of area an enemy will attack
    public float attackArea;

    // Amount of force to apply to a projectile spell
    public float attackForce;

    /**
     * Spawn() - Spawn the prefab enemy.
     */
    public virtual GameObject Spawn(Transform player, Vector3 position)
    {
        if (spawnFxEnemyPrefab != null)
        {
            GameObject fx = Instantiate(spawnFxEnemyPrefab, position, Quaternion.identity);
            Destroy(fx, 3.0f);
        }

        GameObject skeleton = Instantiate(enemyPrefab, position, Quaternion.identity);
        skeleton.GetComponent<EnemyCntrl>().Player = player;

        return (skeleton);
    }
}
