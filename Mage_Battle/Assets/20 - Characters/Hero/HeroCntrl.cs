using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Vector3 camForward;
    private Vector3 move;
    private Vector3 moveInput;
    private float forwardAmount;
    private float turnAmount;

    void Awake()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerMoveState(this));
        fsm.Add(new PlayerAttackState(this));
    }

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);

        /*PlayerMovement(Time.deltaTime);

        if (inputCntrl.HasCast)
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
}
