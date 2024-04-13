using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCntrl : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCntrl>(out PlayerCntrl playerCntrl))
        {
            playerCntrl.StopMoving();
            animator.SetTrigger("OpenChest");
        }
    }
}
