using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private InputCntrl inputCntrl;
    //[SerializeField] private float maximumSpeed;
    //[SerializeField] private float rotationSpeed;
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform mainCamera;

    private FiniteStateMachine fsm = null;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool leftMouseButtonPressed = false;

    void Awake()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerIdleState(this));
        fsm.Add(new PlayerMoveState(this));
        fsm.Add(new PlayerAttackState(this));
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);

        if (inputCntrl.HasCast)
        {
            GameManager.Instance.Cast(castPoint.position, transform.forward);
            inputCntrl.HasCast = false;
        }

        /*if (inputCntrl.HasSelectedSpell())
        {
            GameManager.Instance.Select(inputCntrl.SelectSpell);
            inputCntrl.SelectSpell = -1;
        }*/
    }

    public bool IsLeftMousePressed()
    {
        return (inputCntrl.IsLeftMousePressed());
    }

    public bool IsLeftMouseReleased()
    {
        return (inputCntrl.IsLeftMouseReleased());
    }

    /**
     * GoOnAttack() - 
     */
    public bool GoOnAttack()
    {
        return (inputCntrl.GoOnAttack);
    }

    /**
     * StartAttack() - 
     */
    public void StartAttack()
    {
        animator.SetBool("OnAttack", true);
    }

    /**
     * EndAttack() - 
     */
    public void EndAttack()
    {
        animator.SetBool("OnAttack", false);
    }

    /**
     * PlayerMovement() - 
     */
    public void PlayerMovement(Vector3 position)
    {
        //Vector3 position = GetMousePosition();

        navMeshAgent.destination = position;

        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        animator.SetFloat("Speed", Mathf.Abs(localVelocity.z));
    }

    /**
     * StopPlayer() - 
     */
    public void StopPlayer()
    {
        navMeshAgent.destination = gameObject.transform.position;
        animator.SetFloat("Speed", 0.0f);
    }

    public void PlayerAttack(float dt)
    {
        /*playerMove = inputCntrl.GetPlayerMovement();

        moveDirection.x = playerMove.x; // Horizontal
        moveDirection.y = 0.0f;
        moveDirection.z = playerMove.y; // Vertical

        camForward = Vector3.Scale(mainCamera.up, new Vector3(1, 0, 1)).normalized;
        move = playerMove.y * camForward + playerMove.x * mainCamera.right;
        move.Normalize();
        moveInput = move;

        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;

        if (moveDirection != Vector3.zero)
        {
            charCntrl.Move(moveDirection * 4.5f * Time.deltaTime);

            animator.SetFloat("Horizontal", turnAmount, 0.1f, dt);
            animator.SetFloat("Vertical", forwardAmount, 0.1f, dt);
        }*/

}

/**
 * ClickToMove() - 
 */
public Vector3 GetMousePosition()
    {
        Vector3 position = inputCntrl.GetMousePosition();
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(position), out RaycastHit hit, 100))
        {
            hitPoint = hit.point;
        }

        return (hitPoint);
    }

    public bool HasReachedTarget()
    {
        bool reached = false;

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    reached = true;
                }
            }
        }

        return (reached);
    }

}
