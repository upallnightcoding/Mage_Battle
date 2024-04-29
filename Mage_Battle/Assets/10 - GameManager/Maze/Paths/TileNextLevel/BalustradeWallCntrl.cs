using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BalustradeWallCntrl : MonoBehaviour
{
    [SerializeField] private GameObject topPortalEnter;
    [SerializeField] private GameObject topPortalExit;
    [SerializeField] private GameObject bottomPortalExit;
    [SerializeField] private GameObject bottomPortalEnter;

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
                playerCntrl.Teleport(topPortalEnter, bottomPortalExit);
            } else
            {
                playerCntrl.Teleport(bottomPortalEnter, topPortalExit);
            }
            
        }
    }
}
