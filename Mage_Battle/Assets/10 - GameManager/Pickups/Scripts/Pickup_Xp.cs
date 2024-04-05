using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Xp : PickupCntrl
{
    public override void Movement()
    {
        transform.localRotation = 
            Quaternion.Euler(new Vector3(0, 100 * Time.deltaTime, 0) + transform.localRotation.eulerAngles);
    }
}
