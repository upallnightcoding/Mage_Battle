using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    private SpellCasterCntrl spellCaster = null;
    private SpellSO[] spells = null;
    private int activeSpell = -1;
    private bool readyToCast = true;
    private CastInfo castInfo;

    public SpellSystem()
    {
        spellCaster = new SpellCasterCntrl();
        spells = new SpellSO[5];
        castInfo = new CastInfo();

        for (int i = 0; i < 5; i++) spells[i] = null;
    }

    public CastInfo Cast(Vector3 spawnPoint, Vector3 forward)
    {
        if (readyToCast)
        {
            castInfo.ActiveSpell = activeSpell;

            spellCaster.Cast(castInfo, spawnPoint, forward);

            if (!castInfo.IsCastsLeft)
            {
                StartCoroutine(CoolDownPeriod(activeSpell, spells[activeSpell].coolDownTimeSec));
            }
        }

        return (castInfo);
    }

    /**
     * Add() - 
     */
    public int Add(SpellSO spell)
    {
        spellCaster.Set(spell);
        spells[++activeSpell] = spell;

        return (activeSpell);
    }

    public void Select(int slot)
    {
        activeSpell = slot;
        spellCaster.Set(spells[activeSpell]);
    }

    private IEnumerator CoolDownPeriod(int slot, float coolDown)
    {
        readyToCast = false;

        float now = Time.time;

        while((Time.time - now) < coolDown)
        {
            yield return null;
            GameManager.Instance.UpdateCoolDown(slot, (Time.time - now) / coolDown);
        }

        spellCaster.ReLoad();

        GameManager.Instance.SetFullSpellBar(activeSpell);

        readyToCast = true;
    }
}
