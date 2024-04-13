using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private InputCntrl inputCntrl;
    [SerializeField] private EnemySystem enemySystem;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform castPoint;
    [SerializeField] private GameData gameData;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private PlayerState state = PlayerState.IDLE;
    //private PlayerState prevState = PlayerState.ATTACK;
    //private InputCntrlClickType prevClick = InputCntrlClickType.END_DRAG_CLICK;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputCmds(inputCntrl.GetClick());
    }

    public void NewGame(Vector3 position)
    {
        transform.position = position;
        transform.gameObject.SetActive(true);
        Instantiate(gameData.fxPlayerCreation, position, Quaternion.identity);
    }

    private void ProcessInputCmds(InputCntrlClickType click)
    {
        switch (click)
        {
            case InputCntrlClickType.SINGLE_LEFT_CLICK:
                SelectEnemyOrMove();
                break;
            case InputCntrlClickType.FIRE_CLICK:
                FireState();
                break;
            case InputCntrlClickType.DRAGGING_LEFT_CLICK:
                MoveToState(click);
                break;
            case InputCntrlClickType.DOUBLE_CLICK:
                DisengageFromEnemy();
                break;
        }

        UpdateAnimation();
    }

    /**
     * DisengageFromEnemy() - Executes the functions needed to disengage
     * from the enemy.  This include unselecting the current enemy and
     * allowing for turning based on current movement direction.
     */
    private void DisengageFromEnemy()
    {
        animator.SetBool("Combat", false);
        navMeshAgent.angularSpeed = 1000;
        enemySystem.UnSelectTarget();
        MoveTo(GetMousePosition());
    }

    /**
     * SelectEnemyOrMove() -
     */
    private void SelectEnemyOrMove()
    {
        bool isAttack = enemySystem.SelectEnemyTarget(GetMousePosition());

        if (isAttack)
        {
            animator.SetBool("Combat", true);
            navMeshAgent.angularSpeed = 0;
        } else
        {
            MoveTo(GetMousePosition());
        }
    }

    /**
     * FireState() - 
     */
    private void FireState()
    {
        CastASpell();
    }

    /**
     * MoveToState() - 
     */
    private PlayerState MoveToState(InputCntrlClickType click)
    {
        MoveTo(GetMousePosition());

        return (PlayerState.IDLE);
    }

    private void CastASpell()
    {
        Vector3 direction = gameObject.transform.forward;

        if (enemySystem.IsSelectedEnemy())
        {
            direction = (enemySystem.GetEnemyPosition() - transform.position).normalized;
        }

        GameManager.Instance.Cast(inputCntrl.SelectSpell, castPoint.position, direction);
        inputCntrl.ReSetSelectedSpell();
    }

    private void UpdateAnimation()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        if (enemySystem.IsSelectedEnemy())
        {
            transform.rotation =
                XLib.System.TurnToTarget(enemySystem.GetEnemyPosition(), transform, rotationSpeed, Time.deltaTime);

            animator.SetFloat("Horizontal", localVelocity.x);
            animator.SetFloat("Vertical", localVelocity.z);
        }
        else
        {
            animator.SetFloat("Speed", localVelocity.z);
        }
    }

    private void MoveTo(Vector3 postion)
    {
        navMeshAgent.destination = postion;
    }

    /**
     * StopMoving() - 
     */
    public void StopMoving()
    {
        navMeshAgent.destination = transform.position;
    }

    //==============================================================================

  

    /*private PlayerState DragState(InputCntrlClickType click)
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
            case InputCntrlClickType.FIRE_CLICK:
                CastASpell();
                state = PlayerState.DRAG;
                break;
        }

        return (state);
    }*/

    /*private PlayerState DragAttackState(InputCntrlClickType click)
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
    }*/

    /*private PlayerState xxxMoveToState(InputCntrlClickType click)
    {
        MoveTo(GetMousePosition());

        return (PlayerState.IDLE);
    }*/

    /**
     * AttackState() - 
     */




    /*private PlayerState AttackMoveToState(InputCntrlClickType click)
    {
        MoveTo(GetMousePosition());

        return (PlayerState.ATTACK);
    }*/

    /**
     * MoveTo() - 
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
