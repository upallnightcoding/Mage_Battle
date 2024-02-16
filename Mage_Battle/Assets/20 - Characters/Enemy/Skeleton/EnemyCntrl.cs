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

    public event Action<int> OnKillEnemy = delegate { };

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

    // Update is called once per frame
    void Update()
    {
        if (!stopFSM)
        {
            fsm.OnUpdate(Time.deltaTime);
        }
    }

    #region PositionAndMovementFunctions

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

    #endregion

    /**
     * SetAttackMode() - 
     */
    public void SetAttackMode(Vector3 position)
    {
        selectionPreFab.SetActive(true);
        navMeshAgent.SetDestination(position);
        IsSelected = true;
    }

    public void UnSetAttackMode()
    {
        selectionPreFab.SetActive(false);
        navMeshAgent.SetDestination(transform.position);
        IsSelected = false;
    }

    #region DamageAndDeathFunctions

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
        OnKillEnemy.Invoke(EnemyId);
    }

    #endregion

    

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followArea);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }

    #endregion
}
