using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private EnemySO enemy;

    [SerializeField] private GameObject selectionPreFab;
    [SerializeField] private GameObject orbPreFab;

    public Transform Player { get; set; } = null;

    // Components
    private Animator animator = null;
    private NavMeshAgent navMeshAgent;

    private float attackArea;
    private float followArea;

    private FiniteStateMachine fsm = null;

    public bool WithinAttackArea() 
        => DistanceFromPlayer() < attackArea;
    
    private float DistanceFromPlayer() 
        => Vector3.Distance(Player.transform.position, transform.position);

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
        fsm.OnUpdate(Time.deltaTime);
    }

    public Vector3 Position()
    {
        return (transform.position);
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
     * SetAttackMode() - 
     */
    public void SetAttackMode(Vector3 position)
    {
        selectionPreFab.SetActive(true);
        navMeshAgent.SetDestination(position);
    }

    public void UnSetAttackMode()
    {
        selectionPreFab.SetActive(false);
        navMeshAgent.SetDestination(transform.position);
    }

    public Vector3 DirectionToPlayer()
    {
        return ((Player.transform.position - transform.position).normalized);
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On CollisionEnter ...");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger ...");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followArea);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }
}
