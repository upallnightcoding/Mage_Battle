using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Mage Battle/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;

    [Header("Combat")]
    public float followArea;
    public float attackArea;
    public float attackForce;

    public GameObject selectionPreFab;

   
}
