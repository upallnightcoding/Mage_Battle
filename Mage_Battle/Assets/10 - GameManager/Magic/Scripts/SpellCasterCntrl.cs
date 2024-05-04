using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasterCntrl 
{
    private SpellSO spell;
    private int castPerRound;
    private float lastCastingRateSec;

    private float castPerSec;

    private int slot;

    public bool ReadyToCast { get; set; } = true;

    public SpellCasterCntrl(int slot)
    {
        this.slot = slot;
    }

    /**
     * Set() - Sets the controller when a new spell is added to a slot. 
     * Timing and count variable are set in order to keep track of 
     * when the spell can be executed.  Definitions of the spell attribute
     * are in the spell scriptable object.
     */
    public void Set(SpellSO spell)
    {
        this.spell = spell;

        castPerRound = spell.castPerRound;

        castPerSec = CalcCastingRate();

        lastCastingRateSec = InitCheckTime(castPerSec);
    }

    /**
     * CoolDownTime() - Returns the cool down time of the spell.
     */
    public float CoolDownTime()
    {
        return (spell.coolDownTimeSec);
    }

    /**
     * ReLoad() - Reloads the spell for casting and resets the spell bar
     * in the UI.
     */
    public void ReLoad()
    {
        castPerRound = spell.castPerRound;

        EventSystem.Instance.InvokeOnSetFullSpellBar(slot);
    }

    /**
     * Cast() - 
     */
    public void Cast(CastInfo castInfo, Vector3 spawnPoint, Vector3 forward)
    {
        if (castPerRound != 0)
        {
            castInfo.IsCastsLeft = true;

            if (CheckTime(lastCastingRateSec, castPerSec))
            {
                GameObject cast = Object.Instantiate(spell.modelPreFab, spawnPoint, Quaternion.identity);
                cast.transform.forward = forward;
                GameObject.Destroy(cast, 1.0f);

                lastCastingRateSec = Time.time;
                castPerRound--;
                castInfo.Drain = castPerRound / (float) spell.castPerRound;
            }
        }

        if (castPerRound == 0)
        {
            castInfo.IsCastsLeft = false;
        }
    }

    /**
    * CoolDownPeriod() - 
    */
    public IEnumerator CoolDownPeriod()
    {
        // Calculated percentage of cooldown
        float percentage = 0.0f;

        // Time at start of cooldown
        float now = Time.time;

        // Flag set if cooldown has completed 
        ReadyToCast = false;

        while ((Time.time - now) <= CoolDownTime())
        {
            yield return null;

            percentage = (Time.time - now) / CoolDownTime();

            EventSystem.Instance.InvokeOnSpellCoolDown(slot, percentage);
        }

        ReLoad();

        ReadyToCast = true;
    }

    /**
     * InitCheckTime() - 
     */
    private float InitCheckTime(float delta)
    {
        return (Time.time + delta);
    }

    /**
     * CheckTime() - 
     */
    private bool CheckTime(float lastTimeCheck, float delta)
    {
        return ((Time.time - lastTimeCheck) >= delta);
    }

    /**
     * CalcCastingRate() - 
     */
    private float CalcCastingRate()
    {
        return (1.0f / spell.castPerSec);
    }
}

public class CastInfo
{
    //public int ActiveSpell { get; set; }
    public float Drain { get; set; }
    public bool IsCastsLeft { get; set; }
}
