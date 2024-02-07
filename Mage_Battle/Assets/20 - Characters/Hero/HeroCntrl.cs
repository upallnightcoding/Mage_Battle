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
    [SerializeField] private LayerMask selectableMaskLayer;
    [SerializeField] private EnemySystem enemySystem;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool leftMouseButtonPressed = false;
    private SkeletonCntrl enemyTarget = null;

    private bool desengageSw = false;

    //private Vector3 mousePostion;

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

        Vector3 lookDirection = enemyTarget.Position() - transform.position;
        lookDirection.y = 0.0f;
        Quaternion rot = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10.0f * Time.deltaTime);

        //navMeshAgent.destination = enemyTarget.Position();

        if (inputCntrl.HasCast && LockedOn())
        {
            GameManager.Instance.Cast(castPoint.position, (enemyTarget.Position() - transform.position).normalized);
            inputCntrl.HasCast = false;
        }


        /* HeroCntrlState nextState = PlayerClickAndMove(click);

        //UpdateAnimation();

        //DetachTarget();

        //animator.SetBool("CastSpell", true);
        animator.Play("Magic Heal", 0, 0.25f);
        //animator.CrossFade("CastSpell", 0.25f);

        nextState = HeroCntrlState.MOVE;*/

        /*if (desengageSw)
        {
            nextState = HeroCntrlState.MOVE;
            desengageSw = false;
        }*/

        ChangeState(nextState);
    }

    /**
     * ChangeState() - 
     */
    private void ChangeState(HeroCntrlState newState)
    {
        if ((newState != HeroCntrlState.NO_STATE) && (newState != currentState))
        {
            currentState = newState;
        }
    }

    /**
     * PlayerMovement() - 
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

    private HeroCntrlState PlayerClickAndAttack(InputCntrlClickType click)
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

        if (desengageSw)
        {
            desengageSw = false;
            nextState = HeroCntrlState.MOVE;
        }

        return (nextState);
    }

    private HeroCntrlState AttackOrMoveState(Vector3 mousePostion)
    {
        HeroCntrlState nextState = currentState;

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
                    navMeshAgent.angularSpeed = 0;
                    break;
            }
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

    private bool LockedOn()
    {
        return (enemyTarget != null);
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

    private bool HasReachedAttackTarget()
    {
        bool reached = false;

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (navMeshAgent.hasPath /*|| navMeshAgent.velocity.sqrMagnitude == 0f */)
                {
                    //navMeshAgent.SetDestination(transform.position);
                    reached = true;
                }
            }
        }

        return (reached);
    }

    private bool xxxHasReachedTarget()
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
