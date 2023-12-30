using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private InputCntrl inputCntrl;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform mainCamera;

    private FiniteStateMachine fsm = null;

    private Vector3 moveDirection;
    private Vector2 playerMove;

    private CharacterController charCntrl;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Vector3 camForward;
    private Vector3 move;
    private Vector3 moveInput;
    private float forwardAmount;
    private float turnAmount;

    private bool firstClickPosition = true;

    Vector3 clickPosition = Vector3.zero;

    void Awake()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerMoveState(this));
        fsm.Add(new PlayerAttackState(this));
    }

    // Start is called before the first frame update
    void Start()
    {
        //charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);

        PlayerMovement(Time.deltaTime);

        /*if (inputCntrl.HasCast)
        {
            GameManager.Instance.Cast(castPoint.position, transform.forward);
            inputCntrl.HasCast = false;
        }

        if (inputCntrl.HasSelectedSpell())
        {
            GameManager.Instance.Select(inputCntrl.SelectSpell);
            inputCntrl.SelectSpell = -1;
        }*/
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

    public void PlayerMovement(float dt)
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            clickPosition = ClickToMove(Mouse.current.position.ReadValue());
            //clickPosition.y = 0.0f;
            navMeshAgent.destination = clickPosition;
        }

        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("Speed", speed);
    }

    public void xxPlayerMovement(float dt)
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            clickPosition = ClickToMove(Mouse.current.position.ReadValue());
            clickPosition.y = 0.0f;
        }

        Vector3 playerPosition = transform.position;
        playerPosition.y = 0.0f;

        if (Vector3.Distance(clickPosition, playerPosition) > 0.01f)
        {
            Vector3 moveDirection = (clickPosition - playerPosition);

            animator.SetFloat("Speed", 2.913f /* magnitude */, 0.05f, dt);
            navMeshAgent.destination = clickPosition;

            Quaternion directionRotation = Quaternion.LookRotation(moveDirection);
            Quaternion rotation =
                Quaternion.RotateTowards(transform.rotation, directionRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }
    }

    public void xxxPlayerMovement(float dt)
    {
        playerMove = inputCntrl.GetPlayerMovement();

        moveDirection.x = playerMove.x; // Horizontal
        moveDirection.y = 0.0f;
        moveDirection.z = playerMove.y; // Vertical

        float magnitude = Mathf.Clamp01(moveDirection.magnitude);

        animator.SetFloat("Speed", magnitude, 0.05f, dt);

        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();

            Vector3 velocity = magnitude * maximumSpeed * moveDirection;

            charCntrl.Move(velocity * dt);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * dt);
        }
    }

    public void PlayerAttack(float dt)
    {
        playerMove = inputCntrl.GetPlayerMovement();

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
        }

    }

    public void xPlayerAttack(float dt)
    {
        playerMove = inputCntrl.GetPlayerMovement();

        moveDirection.x = playerMove.x; // Horizontal
        moveDirection.y = 0.0f;
        moveDirection.z = playerMove.y; // Vertical

        float magnitude = Mathf.Clamp01(moveDirection.magnitude);

        animator.SetFloat("Horizontal", playerMove.x, 0.05f, dt);
        animator.SetFloat("Vertical", playerMove.y, 0.05f, dt);

        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();

            Vector3 velocity = magnitude * maximumSpeed * moveDirection;

            charCntrl.Move(velocity * dt);

            //Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * dt);
        }
    }

    private Vector3 ClickToMove(Vector2 targetMousePosition)
    {
        Vector3 hitPoint = Vector3.zero;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(targetMousePosition), out hit, 100))
        {
            hitPoint = hit.point;
        }

        return (hitPoint);
    }

}
