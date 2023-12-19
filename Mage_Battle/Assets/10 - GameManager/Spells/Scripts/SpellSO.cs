using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Mage Battle/Spell")]
public class SpellSO : ScriptableObject
{
    [Header("Spell Attributes")]
    // Name of the spell
    public string spellName;

    // Rate inwhich a spell can be casted within a second
    public float castPerSec;

    // Number of times a spell can be casted within a round 
    public int castPerRound;

    // The amount of force produced by the spell forward
    public float spellForce;

    [Header("General Attributes")]
    // Wait time for the next round to begin
    public float coolDownTimeSec;

    [Header("Render Attributes")]
    // Spell Sprite
    public Sprite spellSprite;

    public GameObject fx;

    public GameObject modelPreFab;

    public TrailSO trail = null;

    protected bool CheckTime(float lastTimeCheck, float delta)
    {
        return ((Time.time - lastTimeCheck) >= delta);
    }
}
