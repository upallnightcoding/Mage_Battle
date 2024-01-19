using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject selectionPreFab;

    private FiniteStateMachine fsm = null;

    // Components
    private Animator animator = null;
    private NavMeshAgent navMeshAgent;

    private float attackArea;
    private float followArea;

    private float moveSpeed;
    private float rotationSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new SkeletonIdleState(this));
        fsm.Add(new SkeletonChaseState(this));
        fsm.Add(new SkeletonAttackState(this));

        moveSpeed = gameData.moveSpeed;
        rotationSpeed = gameData.rotationSpeed;

        followArea = gameData.followArea;
        attackArea = gameData.attackArea;
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

    public bool WithinFollowArea()
    {
        return (DistanceFromPlayer() < followArea);
    }

    public bool WithinAttackArea()
    {
        return (DistanceFromPlayer() < attackArea);
    }

    public void SetAttackMode(Vector3 position)
    {
        selectionPreFab.SetActive(true);
        navMeshAgent.SetDestination(position);
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
        return ((player.transform.position - transform.position).normalized);
    }

    /**
     * DistanceFromPlayer() - 
     */
    private float DistanceFromPlayer()
    {
        return (Vector3.Distance(player.transform.position, transform.position));
    }

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
