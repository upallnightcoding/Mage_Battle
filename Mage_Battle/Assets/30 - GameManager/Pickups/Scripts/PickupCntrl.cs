using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupCntrl : MonoBehaviour
{
    public abstract void Movement();
    public abstract void Pickup(Collision collision);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Pickup(collision);
    }
}
