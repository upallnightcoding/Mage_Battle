using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private GameObject pickupGem;

    // Start is called before the first frame update
    void Start()
    {
        //pickupObject.Spawn(new Vector3());
    }

    public GameObject GetPickupGem()
    {
        return (pickupGem);
    }

}
