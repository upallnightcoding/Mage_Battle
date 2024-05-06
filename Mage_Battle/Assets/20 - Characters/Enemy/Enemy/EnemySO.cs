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

    [Tooltip("Enemy display name.")]
    public string enemyName;

    [Tooltip("Enemy experiance points")]
    public int xp;

    [Tooltip("Enemy prefab.")]
    public GameObject enemyPrefab;

    [Tooltip("Partical system created when an emeny is spawned.")]
    public GameObject enemyFxSpawnPrefab;

    // PreFab PS - Played when a spell is casted
    public GameObject spellFxPrefab;

    //-------------------------------
    [Header("Enemy Weapon Material")]
    //-------------------------------

    [Tooltip("The amount of force used to propel the weapon.")]
    public float enemyWeaponAttackForce;

    //------------------------------------
    [Header("Attack & Follow Parameters")]
    //------------------------------------

    [Tooltip("Area around the enemy that will trigger following.")]
    public float followArea;   
    
    [Tooltip("Area around the enemy that will trigger an attack.")]
    public float attackArea;

    /**
     * Spawn() - Spawn the prefab enemy.
     */
    public virtual GameObject Spawn(Transform player, Vector3 position)
    {
        if (enemyFxSpawnPrefab != null)
        {
            GameObject fx = Instantiate(enemyFxSpawnPrefab, position, Quaternion.identity);
            Destroy(fx, 3.0f);
        }

        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.GetComponent<EnemyCntrl>().Player = player;

        return (enemy);
    }
}
