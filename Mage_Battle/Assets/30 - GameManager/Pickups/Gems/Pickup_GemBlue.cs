using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_GemBlue : PickupCntrl
{
    [SerializeField] private float gemRotationalSpeed;
    [SerializeField] private GameObject fxGemPickup;

    private Vector3 gemRotation = new Vector3();

    public override void Movement()
    {
        gemRotation.y = gemRotationalSpeed * Time.deltaTime;

        transform.localRotation = 
            Quaternion.Euler(gemRotation + transform.localRotation.eulerAngles);
    }

    public override void Pickup(Collision collision)
    {
        Instantiate(fxGemPickup, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
