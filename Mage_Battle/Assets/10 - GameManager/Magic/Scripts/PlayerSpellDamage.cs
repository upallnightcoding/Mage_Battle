using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
        {
            EventSystem.Instance.InvokeOnTakePlayerDamage(10);
        }
    }
}
