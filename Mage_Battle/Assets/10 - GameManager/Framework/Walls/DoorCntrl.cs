using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCntrl : MonoBehaviour
{
    [SerializeField] private Animator animator;

    /*private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("OpenDoor", true);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("OpenDoor", true);
    }
}
