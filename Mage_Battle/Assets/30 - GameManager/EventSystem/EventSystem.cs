using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem
{
    // UI events
    //-------------------------------------------------------------
    public event Action<int, float> OnSpellCoolDown = delegate { };
    public event Action<int> OnSetFullSpellBar = delegate { };
    //public event Action<float> OnHeroDamage = delegate { };

    public event Action OnUpdateUi = delegate { };
    public void InvokeOnUpdateUi() => OnUpdateUi.Invoke();

    // Enemy Events
    //--------------------------------------------------
    public event Action<int, int> OnKillEnemy = delegate { };

    // Hero Events
    //---------------------------------------------
    public event Action OnHeroDeath = delegate { };

    // Player Events
    //--------------------------------------------------------
    public event Action<int> OnTakePlayerDamage = delegate { };
    public void InvokeOnTakePlayerDamage(int points) => OnTakePlayerDamage.Invoke(points);

    public static EventSystem Instance
    {
        get
        {
            if (aInstance == null)
            {
                aInstance = new EventSystem();
            }

            return (aInstance);
        }
    }

    public static EventSystem aInstance = null;

    // Verified
    //---------

    //---------------------------------------------------------------------------------------

    public void InvokeOnSpellCoolDown(int slot, float percentage) => OnSpellCoolDown.Invoke(slot, percentage);

    public void InvokeOnSetFullSpellBar(int slot) => OnSetFullSpellBar.Invoke(slot);

    public void InvokeOnKillEnemy(int enemyId, int expPoints) => OnKillEnemy(enemyId, expPoints);

    //public void InvokeOnHeroDamage(float value) => OnHeroDamage(value);

    public void InvokeOnHeroDeath() => OnHeroDeath();
}


