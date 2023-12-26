using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Mage Battle/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Spell Attributes")]
    public int nSpellSlots;
    public SpellSO[] testSpells;

    public SpellSO defaultSpellDisplay;

    [Space]
    public Sprite[] shieldSprites;

    [Header("Skeleton Attributes")]
    public float moveSpeed;
    public float rotationSpeed;
    public float attackArea;
    public float followArea;
}
