using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    private SpellCasterCntrl spellCaster = null;
    private SpellSO[] spells = null;
    private int activeSpell = 0;
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
            spellCaster.Cast(castInfo, spawnPoint, forward);

            if (!castInfo.IsCastsLeft)
            {
                StartCoroutine(CoolDownPeriod(spells[activeSpell].coolDownTimeSec));
            }
        }

        return (castInfo);
    }

    public void Set(SpellSO spell)
    {
        spellCaster.Set(spell);
        activeSpell = 0;
        spells[activeSpell] = spell;
    }

    private IEnumerator CoolDownPeriod(float coolDown)
    {
        readyToCast = false;

        float now = Time.time;

        while((Time.time - now) < coolDown)
        {
            yield return null;
            Debug.Log($"CoolDown: {Time.time - now}");
            GameManager.Instance.UpdateCoolDown(0, (Time.time - now) / coolDown);
        }

        readyToCast = true;

        spellCaster.ReLoad();

        GameManager.Instance.SetFullSpellBar(activeSpell);
    }
}
