using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    private SpellCasterCntrl[] spellCaster = null;
    private int activeSpell = -1;
    private CastInfo castInfo;

    public SpellSystem()
    {
        spellCaster = new SpellCasterCntrl[5];
        castInfo = new CastInfo();

        for (int slot = 0; slot < 5; slot++)
        {
            spellCaster[slot] = new SpellCasterCntrl(slot);
        }
    }

    /**
     * Cast() - 
     */
    public CastInfo Cast(Vector3 spawnPoint, Vector3 forward)
    {
        if (GetSpellCaster().ReadyToCast)
        {
            castInfo.ActiveSpell = activeSpell;

            GetSpellCaster().Cast(castInfo, spawnPoint, forward);

            if (!castInfo.IsCastsLeft)
            {
                StartCoroutine(GetSpellCaster().CoolDownPeriod());
            }
        }

        return (castInfo);
    }

    /**
     * Add() - 
     */
    public int Add(SpellSO spell)
    {
        activeSpell += 1;

        GetSpellCaster().Set(spell);

        return (activeSpell);
    }

    /**
     * Select() - 
     */
    public void Select(int slot)
    {
        activeSpell = slot;
    }

    private SpellCasterCntrl GetSpellCaster()
    {
        return(spellCaster[activeSpell]);
    }

    /**
     * CoolDownPeriod() - 
     */
    /*private IEnumerator CoolDownPeriod(int slot, float coolDown)
    {
        GetSpellCaster().ReadyToCast = false;

        float now = Time.time;

        while((Time.time - now) <= coolDown)
        {
            yield return null;
            GameManager.Instance.UpdateCoolDown(slot, (Time.time - now) / coolDown);
        }

        GetSpellCaster().ReLoad();

        GameManager.Instance.SetFullSpellBar(activeSpell);

        GetSpellCaster().ReadyToCast = true;
    }*/
}
