using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private EnemySO enemy;

    [SerializeField] private GameObject selectionPreFab;
    [SerializeField] private GameObject orbPreFab;

    private FiniteStateMachine fsm = null;

    public int EnemyId { get; set; } = 0;

    public Transform Player { get; set; } = null;

    // Components
    private Animator animator = null;
    private NavMeshAgent navMeshAgent;

    private float attackArea;
    private float followArea;

    public bool WithinAttackArea() => DistanceFromPlayer() < attackArea;

    private float DistanceFromPlayer() => Vector3.Distance(Player.transform.position, transform.position);

    // Start is called before the first frame update
    void Awake()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new SkeletonIdleState(this));
        fsm.Add(new SkeletonChaseState(this));
        fsm.Add(new SkeletonAttackState(this));

        followArea = enemy.followArea;
        attackArea = enemy.attackArea;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        //selectionModel = Instantiate(selectionPreFab, transform.position + gameData.yOffSet, Quaternion.identity);
        selectionPreFab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);
    }

    /**
     * SetEnemyCB() - 
     */
    public void SetEnemyId(int enemyId)
    {
        this.EnemyId = enemyId;
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

    public void ThrowOrb()
    {
        GameObject go = Instantiate(orbPreFab, orbPreFab.transform.position, Quaternion.identity);
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        go.GetComponent<Rigidbody>().AddForce(direction * enemy.attackForce);
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

    /**
     * DirectionToPlayer() - 
     */
    private Vector3 DirectionToPlayer()
    {
        return ((Player.transform.position - transform.position).normalized);
    }

    /**
     * DistanceFromPlayer() - 
     */
    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I got hit ...");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followArea);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, attackArea);
    }
}
