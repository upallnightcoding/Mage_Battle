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
    private bool pauseInputProcessing = false;
    //private PlayerState state = PlayerState.IDLE;
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
        if (!pauseInputProcessing)
        {
            ProcessInputCmds(inputCntrl.GetClick());
        }
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

    public void Teleport(Vector3 origin, Vector3 destination)
    {
        //EventManager.Instance.InvokeOnCameraSpan(origin, destination);

        StartCoroutine(TeleportPlayer(origin, destination));
    }

    private IEnumerator TeleportPlayer(Vector3 origin, Vector3 destination)
    {
        Debug.Log("Call TeleportPlayer ...");
        pauseInputProcessing = true;

        MoveTo(origin);

        //gameObject.SetActive(false);

        navMeshAgent.enabled = false;
        yield return null;

        Debug.Log("Start moving position");
        /*while (Vector3.Distance(transform.position, destination) > 0.01) 
        {
            float step = 30.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            Debug.Log($"Distance: {Vector3.Distance(transform.position, destination)}");
            yield return null;
        }*/

        //yield return new WaitForSeconds(0.5f);
        transform.position = destination;
        yield return null;

        Debug.Log("End moving position");
        navMeshAgent.enabled = true;
        pauseInputProcessing = false;
        //gameObject.SetActive(true);
        //teleporting = false; 
        yield return null;


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
    private void MoveToState(InputCntrlClickType click)
    {
        MoveTo(GetMousePosition());

        //return (PlayerState.IDLE);
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

    public void MoveTo(Vector3 postion)
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

    /*public enum PlayerState
    {
        IDLE,
        MOVE_TO,
        ATTACK,
        DRAG,
        DRAG_ATTACK,
        ATTACK_MOVE_TO
    }*/
}
