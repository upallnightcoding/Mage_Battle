using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    private ParticleSystem ps;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<EnemyCntrl>(out EnemyCntrl enemyCntrl))
        {
            enemyCntrl.TakeDamage(25.0f);
        }
    }
}
