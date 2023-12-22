using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;

    private FiniteStateMachine fsm = null;

    private CharacterController charCntrl = null;

    private Animator animator = null;

    private float attackArea = 2.0f;
    private float followArea = 4.0f;

    private float moveSpeed = 2.0f;
    private float rotationSpeed = 400.0f;

    // Start is called before the first frame update
    void Awake()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new SkeletonIdleState(this));
        fsm.Add(new SkeletonWalkState(this));
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

    public void MovesTowardPlayer(float dt)
    {
        Vector3 direction = DirectionToPlayer();

        Vector3 velocity = direction * moveSpeed * dt;

        charCntrl.Move(velocity);

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * dt);
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followArea);
    }

    private Vector3 DirectionToPlayer()
    {
        return ((player.transform.position - transform.position).normalized);
    }

    private float DistanceFromPlayer()
    {
        return (Vector3.Distance(player.transform.position, transform.position));
    }
}
