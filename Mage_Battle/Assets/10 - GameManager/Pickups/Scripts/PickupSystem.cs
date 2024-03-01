using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private PickupSO pickupObject;

    // Start is called before the first frame update
    void Start()
    {
        pickupObject.Spawn(new Vector3());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
