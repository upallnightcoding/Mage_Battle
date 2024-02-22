using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour
{
    [SerializeField] private SpellSO spell;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<EnemyCntrl>(out EnemyCntrl enemyCntrl))
        {
            enemyCntrl.TakeDamage(spell.damage);
        }
    }
}