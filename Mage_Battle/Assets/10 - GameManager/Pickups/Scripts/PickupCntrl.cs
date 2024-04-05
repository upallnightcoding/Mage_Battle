using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupCntrl : MonoBehaviour
{
    [SerializeField] protected PickupSO pickupSO;

    public abstract void Movement();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
