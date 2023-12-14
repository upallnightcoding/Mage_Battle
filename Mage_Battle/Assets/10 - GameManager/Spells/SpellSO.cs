using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Mage Battle/Spell")]
public class SpellSO : ScriptableObject
{
    // Type of spell being cast
    public CastType castType;

    [Header("Spell Attributes")]
    // Name of the spell
    public string spellName;

    // Rate inwhich a spell can be casted within a second
    public float castPerSec;

    // Number of times a spell can be casted within a round 
    public int castPerRound;    

    // The amount of force produced by the spell forward
    public float forwardForce;

    [Header("Shield Attributes")]
    public float shieldCastTimeSec;

    [Header("General Attributes")]
    // Wait time for the next round to begin
    public float coolDownTimeSec;

    [Header("Render Attributes")]
    // Spell Sprite
    public Sprite spellSprite;

    public GameObject fx;

    public GameObject modelPreFab;

    public TrailSO trail = null;
}

public enum CastType
{
    SPELL,
    SHEILD
}


