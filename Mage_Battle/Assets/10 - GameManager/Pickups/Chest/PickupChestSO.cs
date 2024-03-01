using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Mage Battle/Pickup/Chest")]
public class PickupChestSO : PickupSO
{
    public override void Movement()
    {
        
    }

    public override void Spawn(Vector3 position)
    {
        GameObject go = Instantiate(pickupPreFab, position, Quaternion.identity);
    }
}
