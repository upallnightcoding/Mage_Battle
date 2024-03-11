using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private InputCntrl inputCntrl;
    [SerializeField] private EnemySystem enemySystem;
    [SerializeField] private float rotationSpeed;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private PlayerState state = PlayerState.IDLE;
    private PlayerState prevState = PlayerState.ATTACK;
    private InputCntrlClickType prevClick = InputCntrlClickType.END_DRAG_CLICK;

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

        if (click != prevClick)
        {
            //Debug.Log($"Click: {click.ToString()}");
            prevClick = click;
        }

        if (state != prevState)
        {
            Debug.Log($"State: {state.ToString()}");
            prevState = state;
        }

        switch(state)
        {
            case PlayerState.IDLE:
                state = IdleState(click);
                break;
            case PlayerState.MOVE_TO:
                state = MoveToState(click);
                break;
            case PlayerState.ATTACK:
                state = AttackState(click);
                break;
            case PlayerState.DRAG:
                state = DragState(click);
                break;
            case PlayerState.DRAG_ATTACK:
                state = DragAttackState(click);
                break;
            case PlayerState.ATTACK_MOVE_TO:
                state = AttackMoveToState(click);
                break;
        }
    }

    private PlayerState DragState(InputCntrlClickType click)
    {
        switch(click)
        {
            case InputCntrlClickType.DRAGGING_CLICK:
                MoveTo(GetMousePosition());
                state = PlayerState.DRAG;
                break;
            case InputCntrlClickType.END_DRAG_CLICK:
                state = PlayerState.IDLE;
                break;
        }

        return (state);
    }

    private PlayerState DragAttackState(InputCntrlClickType click)
    {
        switch (click)
        {
            case InputCntrlClickType.DRAGGING_CLICK:
                MoveTo(GetMousePosition());
                state = PlayerState.DRAG_ATTACK;
                break;
            case InputCntrlClickType.END_DRAG_CLICK:
                state = PlayerState.ATTACK;
                break;
        }

        return (state);
    }

    private PlayerState IdleState(InputCntrlClickType click)
    {
        switch(click)
        {
            case InputCntrlClickType.SINGLE_CLICK:
                state = AttackOrMove(PlayerState.MOVE_TO);
                break;
            case InputCntrlClickType.START_DRAG_CLICK:
                state = PlayerState.DRAG;
                break;
        }

        return (state);
    }

    private PlayerState AttackOrMove(PlayerState nextMove)
    {
        bool isAttack = enemySystem.SelectEnemyTarget(GetMousePosition());

        if (isAttack)
        {
            animator.SetBool("Combat", true);
            navMeshAgent.angularSpeed = 0;
            state = PlayerState.ATTACK;
        }
        else
        {
            state = nextMove;
        }

        return (state);
    }

    private PlayerState MoveToState(InputCntrlClickType click)
    {
        MoveTo(GetMousePosition());

        return (PlayerState.IDLE);
    }

    /**
     * AttackState() - 
     */
    private PlayerState AttackState(InputCntrlClickType click)
    {
        switch(click)
        {
            case InputCntrlClickType.START_DRAG_CLICK:
                state = PlayerState.DRAG_ATTACK;
                break;
            case InputCntrlClickType.SINGLE_CLICK:
                state = AttackOrMove(PlayerState.ATTACK_MOVE_TO);
                break;
            case InputCntrlClickType.DOUBLE_CLICK:
                animator.SetBool("Combat", false);
                navMeshAgent.angularSpeed = 1000;
                enemySystem.UnSelectTarget();
                state = PlayerState.MOVE_TO;
                break;
            case InputCntrlClickType.FIRE_CLICK:
                Debug.Log($"Fire: {inputCntrl.SelectSpell}");
                inputCntrl.ReSetSelectedSpell();
                state = PlayerState.ATTACK;
                break;
        }

        if (state != PlayerState.MOVE_TO)
        {
            transform.rotation =
                XLib.System.TurnToTarget(enemySystem.GetEnemyPosition(), transform, rotationSpeed, Time.deltaTime);

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            animator.SetFloat("Horizontal", localVelocity.x);
            animator.SetFloat("Vertical", localVelocity.z);
        }

        return (state);
    }

    private PlayerState AttackMoveToState(InputCntrlClickType click)
    {
        MoveTo(GetMousePosition());

        return (PlayerState.ATTACK);
    }

    /**
     * MoveTo() - 
     */
    private void MoveTo(Vector3 postion)
    {
        navMeshAgent.destination = postion;

        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        animator.SetFloat("Speed", localVelocity.z);
    }

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

    public enum PlayerState
    {
        IDLE,
        MOVE_TO,
        ATTACK,
        DRAG,
        DRAG_ATTACK,
        ATTACK_MOVE_TO
    }
}
