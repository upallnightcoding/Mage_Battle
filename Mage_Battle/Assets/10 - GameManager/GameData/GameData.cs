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
}
