using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupSO", menuName = "Mage Battle/Pickup")]
public class PickupSO : ScriptableObject
{
    [Header("Pickup Xp Attributes")]

    public float gemRotationalSpeed;
    public GameObject fxGemPickup;

    public float xp;

}