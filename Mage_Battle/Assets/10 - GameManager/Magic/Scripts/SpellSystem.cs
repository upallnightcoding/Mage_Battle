using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    private SpellCasterCntrl[] spellCaster = null;
    //private int activeSpell = -1;
    private CastInfo castInfo;

    void Awake()
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
    public CastInfo Cast(int slot, Vector3 spawnPoint, Vector3 direction)
    {
        if (spellCaster[slot].ReadyToCast)
        {
            spellCaster[slot].Cast(castInfo, spawnPoint, direction);

            if (!castInfo.IsCastsLeft)
            {
                StartCoroutine(spellCaster[slot].CoolDownPeriod());
            }
        }

        return (castInfo);
    }

    /**
     * Add() - 
     */
    public void Add(int slot, SpellSO spell)
    {
        spellCaster[slot].Set(spell);
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
