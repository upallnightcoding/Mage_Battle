using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem
{
    // Update UI as spell cools down
    //------------------------------
    public event Action<int, float> OnSpellCoolDown = delegate { };
    public void InvokeOnSpellCoolDown(int slot, float percentage) => OnSpellCoolDown.Invoke(slot, percentage);

    public event Action<int> OnSetFullSpellBar = delegate { };
    public void InvokeOnSetFullSpellBar(int slot) => OnSetFullSpellBar.Invoke(slot);

    public event Action OnUpdateUi = delegate { };
    public void InvokeOnUpdateUi() => OnUpdateUi.Invoke();

    // Enemy Events
    //--------------------------------------------------
    public event Action<int> OnKillEnemy = delegate { };
    public void InvokeOnKillEnemy(int enemyId) => OnKillEnemy(enemyId);

    // Hero Events
    //---------------------------------------------
    public event Action OnPlayerDeath = delegate { };
    public void InvokeOnPlayerDeath() => OnPlayerDeath();

    // Player Events
    //--------------------------------------------------------
    public event Action<int> OnTakePlayerDamage = delegate { };
    public void InvokeOnTakePlayerDamage(int points) => OnTakePlayerDamage.Invoke(points);

    public event Action<int> OnAddXp = delegate { };
    public void InvokeOnAddXp(int points) => OnAddXp.Invoke(points);

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

    //---------------------------------------------------------------------------------------




    //public void InvokeOnHeroDamage(float value) => OnHeroDamage(value);

    
}


