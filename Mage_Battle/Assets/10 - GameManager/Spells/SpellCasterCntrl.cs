using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasterCntrl //: MonoBehaviour
{
    private SpellSO spell;
    private int castPerRound;
    private int totalCastPerRound;
    private float lastCoolDownTime;
    private float lastCastingRateSec;

    private float coolDownTimeSec;
    private float castPerSec;
    private GameObject modelPreFab;
    private float spellForce;

    private bool readyToCast = true;

    public void Set(SpellSO spell)
    {
        this.spell = spell;

        castPerRound = spell.castPerRound;
        coolDownTimeSec = spell.coolDownTimeSec;
        modelPreFab = spell.modelPreFab;
        spellForce = spell.forwardForce;

        totalCastPerRound = castPerRound;

        castPerSec = CalcCastingRate();

        lastCoolDownTime = InitCheckTime(coolDownTimeSec);
        lastCastingRateSec = InitCheckTime(castPerSec);
    }

    public void ReLoad()
    {
        castPerRound = spell.castPerRound;
    }

    public void Cast(CastInfo castInfo, Vector3 spawnPoint, Vector3 forward)
    {
        if (castPerRound != 0)
        {
            castInfo.IsCastsLeft = true;

            if (CheckTime(lastCastingRateSec, castPerSec))
            {
                GameObject cast = Object.Instantiate(modelPreFab, spawnPoint, Quaternion.identity);
                cast.transform.forward = forward;
                cast.GetComponent<Rigidbody>().AddForce(forward * spellForce, ForceMode.Impulse);

                lastCastingRateSec = Time.time;
                castPerRound--;
                castInfo.Drain = castPerRound / (float)totalCastPerRound;
            }
        }

        if (castPerRound == 0)
        {
            castInfo.IsCastsLeft = false;
        }
    }

    private IEnumerator CoolDownPeriod()
    {
        readyToCast = false;
        yield return new WaitForSeconds(coolDownTimeSec);
        readyToCast = true;
    }

    private float InitCheckTime(float delta)
    {
        return (Time.time + delta);
    }

    private bool CheckTime(float lastTimeCheck, float delta)
    {
        return ((Time.time - lastTimeCheck) >= delta);
    }

    private float CalcCastingRate()
    {
        return (1.0f / spell.castPerSec);
    }
}

public class CastInfo
{
    public int Slot { get; set; }
    public float Drain { get; set; }
    public bool IsCastsLeft { get; set; }
}
