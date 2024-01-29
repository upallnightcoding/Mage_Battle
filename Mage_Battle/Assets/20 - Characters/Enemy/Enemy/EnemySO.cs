using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySO : ScriptableObject
{
    // Enemy Name
    public string enemyName;

    // PreFab of the enemy
    public GameObject enemyPreFab;

    [Header("Combat")]
    // Size a area an enemy will follow
    public float followArea;   

    // Size of area an enemy will attack
    public float attackArea;

    // Amount of force to apply to a projectile spell
    public float attackForce;

    // PreFab PS - Instantiated to show enemy selection
    public GameObject selectionPreFab;

    // PreFab PS - Played when enemy is spawned
    public GameObject spawnFXPreFab;

    // Spawn Enemy
    public abstract GameObject Spawn(Transform player, Vector3 position);
}
