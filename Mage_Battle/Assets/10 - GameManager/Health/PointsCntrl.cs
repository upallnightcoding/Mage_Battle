using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCntrl : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float xp = 0.0f;

    public void AdjustHealth(float health) => this.health += health;

    public bool Dead() => health <= 0.0f;

    public void AdjustXp(float xp) => this.xp += xp;
}
