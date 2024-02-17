using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private InputCntrl inputCntrl;
    [SerializeField] private Transform castPoint;
    [SerializeField] private EnemySystem enemySystem;
    [SerializeField] private float rotationSpeed;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool leftMouseButtonPressed = false;

    private bool desengageSw = false;

    //private Vector3 mousePostion;

    private HeroCntrlState currentState = HeroCntrlState.IDLE;

    //public bool IsLeftMousePressed() => inputCntrl.IsLeftMousePressed();
    //public bool IsLeftMouseReleased() => inputCntrl.IsLeftMouseReleased();

    void Awake()
    {
     
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
        InputCntrlClickType click = inputCntrl.GetClick();

        switch (currentState)
        {
            case HeroCntrlState.IDLE:
                HeroIdleState(click);
                break;
            case HeroCntrlState.MOVE:
                HeroMoveState(click);
                break;
            case HeroCntrlState.ATTACK:
                HeroAttackState(click);
                break;
        }
         
        if (inputCntrl.HasSelectedSpell())
        {
            GameManager.Instance.SelectSpell(inputCntrl.SelectSpell);
            inputCntrl.SetReadyForNextSpell();
        }
    }

   

    /**
    * HeroIdleState() - 
    */
    private void HeroIdleState(InputCntrlClickType click)
    {
        if (click == InputCntrlClickType.SINGLE_CLICK)
        {
            ChangeState(HeroCntrlState.MOVE);
        }
    }

    /**
     * HeroMoveState() - 
     */
    private void HeroMoveState(InputCntrlClickType click)
    {
        HeroCntrlState nextState = PlayerClickAndMove(click);

        

        UpdateAnimation();

        ChangeState(nextState);
    }

    private void HeroAttackState(InputCntrlClickType click)
    {
        HeroCntrlState nextState = PlayerClickAndAttack(click);

        //Vector3 lookDirection = enemyTarget.Position() - transform.position;
        //lookDirection.y = 0.0f;
        //Quaternion rot = Quaternion.LookRotation(lookDirection);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10.0f * Time.deltaTime);
        //navMeshAgent.destination = enemyTarget.Position();

        if (inputCntrl.HasRequestToCast && enemySystem.IsSelectedEnemy())
        {
            Vector3 direction = (enemySystem.GetEnemyPosition() - transform.position).normalized;
            GameManager.Instance.Cast(castPoint.position, direction);
            inputCntrl.HasRequestToCast = false;
        }

        UpdateAnimation();

        /* HeroCntrlState nextState = PlayerClickAndMove(click);

        UpdateAnimation();

        //DetachTarget();

        //animator.SetBool("CastSpell", true);
        animator.Play("Magic Heal", 0, 0.25f);
        //animator.CrossFade("CastSpell", 0.25f);

        nextState = HeroCntrlState.MOVE;*/

       
        ChangeState(nextState);
    }

    /**
     * ChangeState() - 
     */
    private void ChangeState(HeroCntrlState newState)
    {
        //if ((newState != HeroCntrlState.NO_STATE) && (newState != currentState))
        if (newState != currentState)
        {
            currentState = newState;
        }
    }

    /**
     * PlayerClickAndMove() - 
     */
    private HeroCntrlState PlayerClickAndMove(InputCntrlClickType click)
    {
        HeroCntrlState nextState = currentState;

        Vector3 mousePostion = GetMousePosition();

        switch (click)
        {
            case InputCntrlClickType.NO_CLICK:
                break;
            case InputCntrlClickType.SINGLE_CLICK:
                nextState = AttackOrMoveState(mousePostion);
                break;
            case InputCntrlClickType.START_DRAG_CLICK:
            case InputCntrlClickType.DRAGGING_CLICK:
                navMeshAgent.destination = mousePostion;
                break;
            case InputCntrlClickType.END_DRAG_CLICK:
                break;
        }

        return (nextState);
    }

    /**
     * PlayerClickAndAttack() - 
     */
    private HeroCntrlState PlayerClickAndAttack(InputCntrlClickType click)
    {
        if (!enemySystem.IsSelectedEnemy()) return (HeroCntrlState.MOVE);

        HeroCntrlState nextState = currentState;

        Vector3 mousePostion = GetMousePosition();

        transform.rotation =
            XLib.System.TurnToTarget(enemySystem.GetEnemyPosition(), transform, rotationSpeed, Time.deltaTime);

        switch (click)
        {
            case InputCntrlClickType.NO_CLICK:
                break;
            case InputCntrlClickType.SINGLE_CLICK:
                nextState = AttackOrMoveState(mousePostion);
                break;
            case InputCntrlClickType.START_DRAG_CLICK:
            case InputCntrlClickType.DRAGGING_CLICK:
                navMeshAgent.destination = mousePostion;
                break;
            case InputCntrlClickType.END_DRAG_CLICK:
                break;
        }

        if (desengageSw)
        {
            desengageSw = false;
            nextState = HeroCntrlState.MOVE;
            animator.SetBool("Combat", false);
            navMeshAgent.angularSpeed = 1000;
            enemySystem.UnSelectTarget();
        }

        return (nextState);
    }

    /**
     * AttackOrMoveState() - 
     */
    private HeroCntrlState AttackOrMoveState(Vector3 mousePostion)
    {
        HeroCntrlState nextState = currentState;

        if (enemySystem.SelectEnemyTarget(mousePostion))
        {
            nextState = HeroCntrlState.ATTACK;
            animator.SetBool("Combat", true);
            navMeshAgent.angularSpeed = 0;
        } else
        {
            navMeshAgent.destination = mousePostion;
        }

        return (nextState);
    }

    public void OnDesengage()
    {
        desengageSw = true;
    }

    /**
     * UpdateAnimation() - 
     */
    private void UpdateAnimation()
    {
        Vector3 velocity = navMeshAgent.velocity; 
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        if (currentState == HeroCntrlState.MOVE)
        {
            animator.SetFloat("Speed", localVelocity.z);
        }

        if (currentState == HeroCntrlState.ATTACK)
        {
            animator.SetFloat("Horizontal", localVelocity.x);
            animator.SetFloat("Vertical", localVelocity.z);
        }
    }

    /**
     * ClickToMove() - 
     */
    private Vector3 GetMousePosition()
    {
        Vector3 position = inputCntrl.GetMousePosition();
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(position), out RaycastHit hit, 100))
        {
            hitPoint = hit.point;
        }

        return (hitPoint);
    }

    /**
     * OnTriggerEnter() - 
     */
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hero Trigger Enter ...");
    }

    public void OnDrawGizmos()
    {
        if (navMeshAgent != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(navMeshAgent.destination, 0.15f);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.15f);

            if (enemySystem.IsSelectedEnemy())
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(enemySystem.GetEnemyPosition(), 2.0f);
            }
        }
    }

    public void OnEnable()
    {
        InputCntrl.OnDesengageEvent += OnDesengage;
    }

    public void OnDisable()
    {
        InputCntrl.OnDesengageEvent -= OnDesengage;
    }

}

public enum HeroCntrlState
{
    NO_STATE,
    IDLE,
    MOVE, 
    ATTACK
}
