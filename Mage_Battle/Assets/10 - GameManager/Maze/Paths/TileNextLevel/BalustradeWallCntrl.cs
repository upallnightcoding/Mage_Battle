using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BalustradeWallCntrl : MonoBehaviour
{
    [SerializeField] private GameObject topLevelPoint;
    [SerializeField] private GameObject bottomLevelPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCntrl>(out PlayerCntrl playerCntrl))
        {
            if (playerCntrl.gameObject.transform.position.y > 2.5)
            {
                playerCntrl.Teleport(
                    topLevelPoint.transform.position,
                    bottomLevelPoint.transform.position
                );
            } else
            {
                playerCntrl.Teleport(
                    bottomLevelPoint.transform.position,
                    topLevelPoint.transform.position
                );
            }
            
        }
    }
}
