using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private UiSpellSlotCntrl[] uiSpellSlots;
    [SerializeField] private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /**
     * NewGame() -
     */
    public void NewGame()
    {
        InitAllSpellSlots();
    }

    /**
     * InitAllSpellSlots() - 
     */
    public void InitAllSpellSlots()
    {
        for (int slot = 0; slot < gameData.nSpellSlots; slot++)
        {
            uiSpellSlots[slot].SetSprite(gameData.defaultSpellDisplay.spellSprite);
            uiSpellSlots[slot].SetDisplayBar(0.0f);
            uiSpellSlots[slot].SetLabel(slot + 1);
        }
    }

    /**
     * SelectSpell() - 
     */
    public void Set(int slot, SpellSO spell)
    {
        for (int i = 0; i < gameData.nSpellSlots; i++)
        {
            uiSpellSlots[i].SelectSpellSlot(false);
        }

        uiSpellSlots[slot].SelectSpellSlot(true);

        uiSpellSlots[slot].SetSprite(spell.spellSprite);

        SetFullSpellBar(slot);
    }

    public void DrainSpellBar(CastInfo castInfo)
    {
        uiSpellSlots[castInfo.ActiveSpell].SetDisplayBar(castInfo.Drain);
    }

    public void SetFullSpellBar(int slot)
    {
        uiSpellSlots[slot].SetDisplayColor(Color.red);
        uiSpellSlots[slot].SetDisplayBar(1.0f);
    }

    public void UpdateSpellBar(int slot, float percentage)
    {
        uiSpellSlots[slot].SetDisplayColor(Color.yellow);
        uiSpellSlots[slot].SetDisplayBar(percentage);
    }
}
