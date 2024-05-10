using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    [SerializeField] private PlayerCntrl playerCntrl;
    [SerializeField] private float damping;
    [SerializeField] private InputCntrl inputCntrl;

    private Vector3 delta;
    private Vector3 movePosition;
    private Vector3 velocity = Vector3.zero;

    private Vector2 prevDrag;
    private Vector2 nextDrag;
    private Vector2 dragDelta;

    private bool dragging = false;

    private float fov = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        //delta = player.position - transform.position;
        CalculateDelta(playerCntrl.transform);
        GetComponent<Camera>().fieldOfView = fov;  
    }

    void Update()
    {
        ProcessInputCmds(inputCntrl.GetClick());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playerCntrl.IsTeleporting)
        {
            UpdateCamera(playerCntrl.transform);
        } else
        {
            UpdateCamera(playerCntrl.GetTeleportPath());
        }

        UpdateScrolling(inputCntrl.GetScrollWheel());
    }

    /**
     * UpdateScrolling() - 
     */
    private void UpdateScrolling(Vector2 scroll)
    {
        if (scroll != Vector2.zero)
        {
            fov += (scroll.y > 0.0f) ? -2.0f : 2.0f;
            fov = Mathf.Clamp(fov, 30.0f, 70.0f);
            GetComponent<Camera>().fieldOfView = fov;
        }
    }

    private void UpdateCamera(Transform target)
    {
        if (dragging)
        {
            dragDelta = nextDrag - prevDrag;
            Quaternion turnAngle =
                Quaternion.AngleAxis(dragDelta.x * 0.75f * Time.deltaTime, Vector3.up);
            delta = turnAngle * delta;
        }

        movePosition = target.position - delta;

        transform.position =
            Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);

        transform.LookAt(target);
    }

    private void CalculateDelta(Transform target)
    {
        delta = target.position - transform.position;
    }

    private void ProcessInputCmds(InputCntrlClickType click)
    {
        switch (click)
        {
            case InputCntrlClickType.START_RIGHT_DRAG_CLICK:
                prevDrag = inputCntrl.GetMousePosition();
                break;
            case InputCntrlClickType.DRAGGING_RIGHT_CLICK:
                nextDrag = inputCntrl.GetMousePosition();
                dragging = true;
                break;
            case InputCntrlClickType.END_DRAG_RIGHT_CLICK:
                dragging = false;
                break;
            case InputCntrlClickType.SINGLE_RIGHT_CLICK:
                Debug.Log("Right Click");
                break;
        }
    }
}
