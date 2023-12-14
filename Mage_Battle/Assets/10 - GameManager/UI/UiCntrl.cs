using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private SpellSlotCntrl[] spellSlots;
    [SerializeField] private SpellSO defaultSpellDisplay;
    [SerializeField] private GameData gameData;

    [SerializeField] private SpellSO[] testSpells;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        InitAllSpellSlots();

        for (int slot = 0; slot < testSpells.Length; slot++)
        {
            spellSlots[slot].SetSprite(testSpells[slot].spellSprite);
            spellSlots[slot].SetDisplayBar(1.0f);
        }
    }

    public void InitAllSpellSlots()
    {
        for (int slot = 0; slot < gameData.nSpellSlots; slot++)
        {
            spellSlots[slot].SetSprite(defaultSpellDisplay.spellSprite);
            spellSlots[slot].SetDisplayBar(0.0f);
        }
    }

    public void ToggleSpellFrame(int slot)
    {
        spellSlots[slot].ToggleSpellFrame();
    }

    public void DrainSpellBar(CastInfo castInfo)
    {
        spellSlots[castInfo.Slot].SetDisplayBar(castInfo.Drain);
    }

    public void SetFullSpellBar(int slot)
    {
        spellSlots[slot].SetDisplayColor(Color.red);
        spellSlots[slot].SetDisplayBar(1.0f);
    }

    public void UpdateSpellBar(int slot, float percentage)
    {
        spellSlots[slot].SetDisplayColor(Color.yellow);
        spellSlots[slot].SetDisplayBar(percentage);
    }
}
