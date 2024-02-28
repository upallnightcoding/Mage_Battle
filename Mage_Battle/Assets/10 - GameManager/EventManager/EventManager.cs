using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    // UI events
    //-------------------------------------------------------------
    public event Action<int, float> OnSpellCoolDown = delegate { };
    public event Action<int> OnSetFullSpellBar = delegate { };
    public event Action<float> OnHeroDamage = delegate { };

    // Enemy Events
    //--------------------------------------------------
    public event Action<int, int> OnKillEnemy = delegate { };

    // Hero Events
    //---------------------------------------------
    public event Action OnHeroDeath = delegate { };

    public static EventManager Instance
    {
        get
        {
            if (aInstance == null)
            {
                aInstance = new EventManager();
            }

            return (aInstance);
        }
    }

    public static EventManager aInstance = null;

    public void InvokeOnSpellCoolDown(int slot, float percentage) => OnSpellCoolDown.Invoke(slot, percentage);

    public void InvokeOnSetFullSpellBar(int slot) => OnSetFullSpellBar.Invoke(slot);

    public void InvokeOnKillEnemy(int enemyId, int expPoints) => OnKillEnemy(enemyId, expPoints);

    public void InvokeOnHeroDamage(float value) => OnHeroDamage(value);

    public void InvokeOnHeroDeath() => OnHeroDeath();
}


