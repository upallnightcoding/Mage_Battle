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

        Health = (int) Mathf.Clamp((float)Health, 0.0f, 100.0f);

        EventSystem.Instance.InvokeOnUpdateUi();

        if (IsDead())
        {
            EventSystem.Instance.InvokeOnPlayerDeath();
        }
    }

    public void AddXp(int points)
    {
        Xp += points;

        EventSystem.Instance.InvokeOnUpdateUi();
    }

    public bool IsDead() => (Health <= 0);

    public bool IsUpLevel(int level) => (Xp >= level);

    private void OnEnable()
    {
        EventSystem.Instance.OnTakePlayerDamage += TakePlayerDamage;
        EventSystem.Instance.OnAddXp += AddXp;
    }

    private void OnDisable()
    {
        EventSystem.Instance.OnTakePlayerDamage -= TakePlayerDamage;
        EventSystem.Instance.OnAddXp -= AddXp;
    }
}
