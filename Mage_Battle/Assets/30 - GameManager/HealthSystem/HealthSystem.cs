using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int Health { get; private set; } = 100;
    public int Xp { get; private set; } = 0;

    public void TakePlayerDamage(int points)
    {
        Health -= points;

        EventSystem.Instance.InvokeOnUpdateUi();
    }

    public void AddXp(int points)
    {
        Xp += points;

        EventSystem.Instance.InvokeOnUpdateUi();
    }

    public bool IsDead() => (Health <= 0.0f);

    public bool IsUpLevel(int level) => (Xp >= level);

    private void OnEnable()
    {
        EventSystem.Instance.OnTakePlayerDamage += TakePlayerDamage;
    }

    private void OnDisable()
    {
        EventSystem.Instance.OnTakePlayerDamage -= TakePlayerDamage;
    }
}
