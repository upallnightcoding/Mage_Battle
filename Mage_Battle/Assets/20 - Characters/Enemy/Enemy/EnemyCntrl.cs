using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private EnemySO enemy;
    [SerializeField] private GameObject enemySelector;
    [SerializeField] private GameObject enemyProjectile;

    // These fields are serialized for the test bed
    //---------------------------------------------
    [field:SerializeField] public Transform Player { get; set; } = null;
    [field:SerializeField] public int EnemyId { get; set; } = -1;
    [field:SerializeField] public bool IsSelected { get; set; } = false;

    // Components
    private Animator animator = null;
    private NavMeshAgent navMeshAgent;

    private float attackArea;
    private float followArea;

    private GameObject spellFxPrefab;
    
    private float attackForce;
    private bool isDead = false;

    private bool stopFSM = false;
    private int health = 100;

    private FiniteStateMachine fsm = null;

    // Start is called before the first frame update
    void Awake()
    {
        fsm = CreateFsm(this);

        spellFxPrefab = enemy.spellFxPrefab;
        followArea = enemy.followArea;
        attackArea = enemy.attackArea;
        attackForce = enemy.enemyWeaponAttackForce;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        enemySelector.SetActive(false);
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
        if (spellFxPrefab != null)
        {
            GameObject go = Instantiate(spellFxPrefab, position, Quaternion.identity);
            Vector3 direction = DirectionToPlayer();
            go.GetComponent<Rigidbody>().AddForce(direction * enemy.enemyWeaponAttackForce);
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

    public int GetXp() => enemy.xp;

    /**
     * TakeDamage() - 
     */
    public void TakeDamage(int points)
    {
        health -= points;

        if(health <= 0.0)
        {
            isDead = true;
        }
    }

    /**
     * KillEnemy() - 
     */
    public void KillEnemy()
    {
        stopFSM = true;
        EventSystem.Instance.InvokeOnKillEnemy(EnemyId);
    }

    /**
     * MovesTowardPlayer() -
     */
    public void MovesTowardPlayer()
    {
        navMeshAgent.SetDestination(Player.position);
        animator.SetFloat("Speed", enemy.chaseSpeed);
    }

    /**
     * SetAsEnemyTarget() - Set the current enemy as the spell
     * target.  The target indicator is turned on, the selection
     * flag is set and the enemy is moved to a new destination.
     */
    public void SetAsEnemyTarget(Vector3 position)
    {
        enemySelector.SetActive(true);
        navMeshAgent.SetDestination(position);
        IsSelected = true;
    }

    /**
     * UnSetAttackMode() - Unsets the current enemy as the 
     * spell target.
     */
    public void UnSetAttackMode()
    {
        enemySelector.SetActive(false);
        navMeshAgent.SetDestination(transform.position);
        IsSelected = false;
    }

    /**
     * AttackPlayer() - 
     */
    public void AttackPlayer()
    {
        if (enemyProjectile != null)
        {
            CastSpell(enemyProjectile.transform.position);
        }
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
