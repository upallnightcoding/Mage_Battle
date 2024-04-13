using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float damping;
    [SerializeField] private InputCntrl inputCntrl;

    private Vector3 delta;
    private Vector3 movePosition;
    private Vector3 scrollPosition;
    private Vector3 velocity = Vector3.zero;

    private Vector2 prevDrag;
    private Vector2 nextDrag;
    private Vector2 dragDelta;
    private bool dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        delta = player.position - transform.position;
        scrollPosition = transform.position;
    }

    void Update()
    {
        ProcessInputCmds(inputCntrl.GetClick());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*Vector2 scroll = inputCntrl.GetScrollWheel();
        if (scroll != Vector2.zero)
        {
            Debug.Log($"Scroll: {inputCntrl.GetScrollWheel()}");
            if (scroll.y > 0.0)
            {
                scrollPosition = delta.normalized * 0.5F + transform.position; 
            } else
            {
                scrollPosition = -delta.normalized * 0.5F + transform.position;
            }

            transform.position =
                Vector3.SmoothDamp(transform.position, scrollPosition, ref velocity, damping);
        }*/

        if (dragging)
        {
            dragDelta = nextDrag - prevDrag;
            Quaternion turnAngle =
                Quaternion.AngleAxis(dragDelta.x * 0.75f * Time.deltaTime, Vector3.up);
            delta = turnAngle * delta;
        }

        movePosition = player.position - delta;

        transform.position = 
            Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);

        transform.LookAt(player);
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
