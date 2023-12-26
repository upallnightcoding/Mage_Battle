using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform player;

    private FiniteStateMachine fsm = null;

    private CharacterController charCntrl = null;

    private Animator animator = null;

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
        charCntrl = GetComponent<CharacterController>();
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

    public void MovesTowardPlayer(float dt)
    {
        Vector3 direction = DirectionToPlayer();

        Vector3 velocity = direction * moveSpeed * dt;

        charCntrl.Move(velocity);

        //animator.SetFloat("Speed", moveSpeed);
        animator.SetFloat("Speed", 1.0f);

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * dt);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followArea);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, attackArea);
    }
}
