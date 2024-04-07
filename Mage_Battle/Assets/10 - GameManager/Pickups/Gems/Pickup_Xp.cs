using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Xp : PickupCntrl
{
    private Vector3 gemRotation = new Vector3();

    public override void Movement()
    {
        gemRotation.y = pickupSO.gemRotationalSpeed * Time.deltaTime;

        transform.localRotation = 
            Quaternion.Euler(gemRotation + transform.localRotation.eulerAngles);
    }

    public override void Pickup(Collision collision)
    {
        //Debug.Log("Gem Pickup ...");
        //GetComponent<MeshRenderer>().enabled = false;
        Instantiate(pickupSO.fxGemPickup, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
