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
    [SerializeField] private GameObject selectionPreFab;
    [SerializeField] private LayerMask selectableMaskLayer;
    [SerializeField] private EnemySystem enemySystem;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool leftMouseButtonPressed = false;
    private SkeletonCntrl enemyTarget = null;

    private GameObject selectionModel;

    private Vector3 mousePostion;

    private HeroCntrlState currentState = HeroCntrlState.IDLE;

    public bool IsLeftMousePressed() => inputCntrl.IsLeftMousePressed();
    public bool IsLeftMouseReleased() => inputCntrl.IsLeftMouseReleased();

    void Awake()
    {
     
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        selectionModel = Instantiate(selectionPreFab, transform.position + gameData.yOffSet, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //fsm.OnUpdate(Time.deltaTime);

        switch(currentState)
        {
            case HeroCntrlState.IDLE:
                HeroIdleState();
                break;
            case HeroCntrlState.MOVE:
                HeroMoveState();
                break;
            case HeroCntrlState.ATTACK:
                HeroAttackState();
                break;
        }
         
        /*if (inputCntrl.HasCast && lockedOn)
        {
            GameManager.Instance.Cast(castPoint.position, (enemy.position - transform.position).normalized);
            inputCntrl.HasCast = false;
        }

        if (inputCntrl.HasSelectedSpell())
        {
            GameManager.Instance.SelectSpell(inputCntrl.SelectSpell);
            inputCntrl.SetReadyForNextSpell();
        }*/
    }

    private void HeroAttackState()
    {
        HeroCntrlState nextState = HeroCntrlState.NO_STATE;

        PlayerClickAndMove();

        UpdateAnimation();

        if (HasReachedTarget())
        {
            DetachTarget();
            
            nextState = HeroCntrlState.MOVE;
        }

        ChangeState(nextState);
    }

    private void HeroMoveState()
    {
        HeroCntrlState nextState = PlayerClickAndMove();

        UpdateAnimation();

        ChangeState(nextState);
    }

    private void HeroIdleState()
    {
        if (IsLeftMousePressed())
        {
            ChangeState(HeroCntrlState.MOVE);
        }
    }

    private void ChangeState(HeroCntrlState newState)
    {
        if ((newState != HeroCntrlState.NO_STATE) && (newState != currentState))
        {
            currentState = newState;
        }
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
    private HeroCntrlState PlayerClickAndMove()
    {
        HeroCntrlState nextState = currentState;

        if (IsLeftMousePressed())
        {
            mousePostion = GetMousePosition();

            RaycastHit[] hits = Physics.SphereCastAll(mousePostion, 1.0f, transform.forward, 0.0f, selectableMaskLayer);

            if (hits.Length > 0)
            {
                Transform selected = hits[0].transform;
                switch (selected.GetComponent<SelectableCntrl>().GetSelectable())
                {
                    case SelectableType.Enemy:
                        enemyTarget = selected.GetComponent<SkeletonCntrl>();
                        enemySystem.SelectTarget(transform.position, enemyTarget);
                        nextState = HeroCntrlState.ATTACK;
                        break;
                }
            }
            else
            {
                nextState = HeroCntrlState.MOVE;
            }
        } 
            
        SetDestination((enemyTarget != null) ? enemyTarget.transform.position : mousePostion);

        return (nextState);
    }

    private HeroCntrlState xxxPlayerClickAndMove()
    {
        HeroCntrlState nextState = HeroCntrlState.NO_STATE;
        Vector3 mousePostion = GetMousePosition();

        RaycastHit[] hits = Physics.SphereCastAll(mousePostion, 1.0f, transform.forward, 0.0f, selectableMaskLayer);

        if (hits.Length > 0)
        {
            Transform selected = hits[0].transform;
            switch (selected.GetComponent<SelectableCntrl>().GetSelectable())
            {
                case SelectableType.Enemy:
                    enemyTarget = selected.GetComponent<SkeletonCntrl>();
                    enemySystem.SelectTarget(transform.position, enemyTarget);
                    nextState = HeroCntrlState.ATTACK;
                    break;
            }
        }
        else
        {
            SetDestination((enemyTarget != null) ? enemyTarget.transform.position : mousePostion);
        }

        return (nextState);
    }

    private void PlayerMoveToTarget()
    {
        if (enemyTarget != null)
        {
            SetDestination(enemyTarget.transform.position);
        }
    }

    /**
     * SetDestination() -
     */
    private void SetDestination(Vector3 position)
    {
        navMeshAgent.destination = position;
        selectionModel.transform.position = position;
    }

    public void DetachTarget()
    {
        enemyTarget = null;
    }

    /**
     * UpdateAnimation() - 
     */
    private void UpdateAnimation()
    {
        Vector3 velocity = navMeshAgent.velocity; 
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        animator.SetFloat("Speed", localVelocity.z);
    }

    /**
     * StopPlayer() - 
     */
    private void StopPlayer()
    {
        navMeshAgent.destination = gameObject.transform.position;
        animator.SetFloat("Speed", 0.0f);
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

    private bool HasReachedTarget()
    {
        bool reached = false;

        if (!navMeshAgent.pathPending)
        {
            Debug.Log($"Remaining: {navMeshAgent.remainingDistance} / {Vector3.Distance(transform.position, navMeshAgent.destination)}");

            if (navMeshAgent.remainingDistance <= 1.0f)
            //if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (navMeshAgent.hasPath /*|| navMeshAgent.velocity.sqrMagnitude == 0f */)
                {
                    navMeshAgent.SetDestination(transform.position);
                    reached = true;
                }
            }
        }

        return (reached);
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

            if (enemyTarget != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(enemyTarget.transform.position, 2.0f);
            }
        }
    }

}

public enum HeroCntrlState
{
    NO_STATE,
    IDLE,
    MOVE, 
    ATTACK
}
