using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupSO : ScriptableObject
{
    public abstract void Movement();
    public abstract void Spawn(Vector3 position);

    [Header("Attributes")]

    // Value of the pickup
    public int value;

    // Prefab representation of the pickup object
    public GameObject pickupPreFab;
}
