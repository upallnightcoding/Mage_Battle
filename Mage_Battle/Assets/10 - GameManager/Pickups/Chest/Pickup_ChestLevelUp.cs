using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_ChestLevelUp : PickupCntrl
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject levelUp_Left;
    [SerializeField] private GameObject levelUp_Center;
    [SerializeField] private GameObject levelUp_Right;

    [SerializeField] private GameObject GO_Left;
    [SerializeField] private GameObject GO_Center;
    [SerializeField] private GameObject GO_Right;

    public override void Movement()
    {
        
    }

    public override void Pickup(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCntrl>(out PlayerCntrl playerCntrl))
        {
            playerCntrl.StopMoving();
            animator.SetTrigger("OpenChest");

            Instantiate(GO_Left, levelUp_Left.transform.position, Quaternion.identity);
            Instantiate(GO_Center, levelUp_Center.transform.position, Quaternion.identity);
            Instantiate(GO_Right, levelUp_Right.transform.position, Quaternion.identity);
        }
    }
}
