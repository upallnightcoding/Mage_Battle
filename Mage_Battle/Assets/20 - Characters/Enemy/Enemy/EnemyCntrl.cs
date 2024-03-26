using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private EnemySO enemy;

    [SerializeField] private GameObject selectionPreFab;
    [SerializeField] private GameObject orbPreFab;

    public Transform Player { get; set; } = null;
    public int EnemyId { get; set; } = -1;
    public bool IsSelected { get; set; } = false;

    // Components
    private Animator animator = null;
    private NavMeshAgent navMeshAgent;

    private float attackArea;
    private float followArea;

    private float health = 100.0f;
    private float xp = 0.0f;

    private bool isDeadSw = false;
    private bool stopFSM = false;

    private FiniteStateMachine fsm = null;

    // Start is called before the first frame update
    void Awake()
    {
        fsm = enemy.Behavior(this);

        followArea = enemy.followArea;
        attackArea = enemy.attackArea;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        selectionPreFab.SetActive(false);
    }

    /**
     * Update() - Game update loop that will continue until the "Stop FSM"
     * flag has been set.  This can happen when the enemy dies and the
     * FSM needs to terminate all processing.  This loop feeds the FSM
     * and continues all FSM state transitions.
     */
    void Update()
    {
        if (!stopFSM)
        {
            fsm.OnUpdate(Time.deltaTime);
        }
    }

    public bool WithinAttackArea()
       => DistanceFromPlayer() < attackArea;

    private float DistanceFromPlayer()
        => Vector3.Distance(Player.transform.position, transform.position);

    public Vector3 Position()
    {
        return (transform.position);
    }

    public Vector3 DirectionToPlayer()
    {
        return ((Player.transform.position - transform.position).normalized);
    }

    /**
     * MovesTowardPlayer() -
     */
    public void MovesTowardPlayer()
    {
        navMeshAgent.SetDestination(Player.position);
        animator.SetFloat("Speed", 1.0f);
    }

    /**
     */
    public bool WithinChaseArea()
    {
        return(Player == null ? false : DistanceFromPlayer() < followArea);
    }

    /**
     * SetAsEnemyTarget() - Set the current enemy as the spell
     * target.  The target indicator is turned on, the selection
     * flag is set and the enemy is moved to a new destination.
     */
    public void SetAsEnemyTarget(Vector3 position)
    {
        selectionPreFab.SetActive(true);
        navMeshAgent.SetDestination(position);
        IsSelected = true;
    }

    /**
     * UnSetAttackMode() - Unsets the current enemy as the 
     * spell target.
     */
    public void UnSetAttackMode()
    {
        selectionPreFab.SetActive(false);
        navMeshAgent.SetDestination(transform.position);
        IsSelected = false;
    }

    /**
     * IsDead() - Predicate function that returns if the enemy has
     * been killed or not.  This function returns true, if the enemy
     * is dead, otherwise false.
     */
    public bool IsDead()
    {
        return (isDeadSw);
    }

    /**
     * TakeDamage() - 
     */
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            isDeadSw = true;
        }
    }

    /**
     * KillEnemy() - 
     */
    public void KillEnemy()
    {
        stopFSM = true;
        EventManager.Instance.InvokeOnKillEnemy(EnemyId, enemy.expPoints);
    }

    public void CastSpell()
    {
        enemy.CastSpell(this, orbPreFab.transform.position);
    }

    /**
     * SetSpeed() - 
     */
    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    /**
     * TriggerAttack() - 
     */
    public void TriggerAttack(bool value)
    {
        animator.SetBool("Attack", value);
    }

    #region CallBackFunctions

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On CollisionEnter ...");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"On Trigger: {other.gameObject.name}");
    }*/

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, followArea);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, attackArea);
    }

    #endregion
}
