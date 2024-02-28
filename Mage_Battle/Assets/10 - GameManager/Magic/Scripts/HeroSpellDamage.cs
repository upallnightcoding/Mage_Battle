using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpellDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<HeroCntrl>(out HeroCntrl heroCntrl))
        {
            heroCntrl.TakeDamage(10);
        }
    }
}
