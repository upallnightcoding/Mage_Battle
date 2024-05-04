using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] protected EnemySO enemy;
    [SerializeField] private GameObject orbPreFab;

    public Transform Player { get; set; } = null;
    public int EnemyId { get; set; } = -1;
    public bool IsSelected { get; set; } = false;

    // Components
    private Animator animator = null;
    private NavMeshAgent navMeshAgent;
    

    private float attackArea;
    private float followArea;

    private GameObject enemySelectPrefab;
    private GameObject spellFxPreFab;

    private float attackForce;
    private bool isDead = false;

    private bool stopFSM = false;

    private FiniteStateMachine fsm = null;

    // Start is called before the first frame update
    void Awake()
    {
        fsm = CreateFsm(this);

        enemySelectPrefab = enemy.enemySelectPrefab;
        spellFxPreFab = enemy.spellFxPreFab;

        followArea = enemy.followArea;
        attackArea = enemy.attackArea;
        attackForce = enemy.attackForce;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        

        enemySelectPrefab.SetActive(false);
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

    /************************/
    /*** Virual Functions ***/
    /************************/

    public virtual FiniteStateMachine CreateFsm(EnemyCntrl enemyCntrl)
    {
        FiniteStateMachine fsm = new();

        fsm.Add(new SkeletonIdleState(enemyCntrl));
        fsm.Add(new SkeletonChaseState(enemyCntrl));
        fsm.Add(new SkeletonAttackState(enemyCntrl));
        fsm.Add(new SkeletonDieState(enemyCntrl));

        return (fsm);
    }

    public virtual void CastSpell(Vector3 position)
    {
        if (spellFxPreFab != null)
        {
            GameObject go = Instantiate(spellFxPreFab, position, Quaternion.identity);
            Vector3 direction = DirectionToPlayer();
            go.GetComponent<Rigidbody>().AddForce(direction * enemy.attackForce);
        }
    }

    /*************************/
    /*** Utility Functions ***/
    /*************************/

    public bool WithinAttackArea()
       => DistanceFromPlayer() < attackArea;

    private float DistanceFromPlayer()
        => Vector3.Distance(Player.transform.position, transform.position);

    public Vector3 Position()
        => transform.position;

    public Vector3 DirectionToPlayer()
        => (Player.transform.position - transform.position).normalized;

    public bool WithinChaseArea()
        => Player == null ? false : DistanceFromPlayer() < followArea;

    public bool IsDead()
        => isDead;

    /**
     * MovesTowardPlayer() -
     */
    public void MovesTowardPlayer()
    {
        navMeshAgent.SetDestination(Player.position);
        animator.SetFloat("Speed", 1.0f);
    }

    /**
     * SetAsEnemyTarget() - Set the current enemy as the spell
     * target.  The target indicator is turned on, the selection
     * flag is set and the enemy is moved to a new destination.
     */
    public void SetAsEnemyTarget(Vector3 position)
    {
        enemySelectPrefab.SetActive(true);
        navMeshAgent.SetDestination(position);
        IsSelected = true;
    }

    /**
     * UnSetAttackMode() - Unsets the current enemy as the 
     * spell target.
     */
    public void UnSetAttackMode()
    {
        enemySelectPrefab.SetActive(false);
        navMeshAgent.SetDestination(transform.position);
        IsSelected = false;
    }

    /**
     * KillEnemy() - 
     */
    public void KillEnemy()
    {
        stopFSM = true;
        EventSystem.Instance.InvokeOnKillEnemy(EnemyId, enemy.xp);
    }

    public void AttackPlayer()
    {
        CastSpell(orbPreFab.transform.position);
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

    
}
