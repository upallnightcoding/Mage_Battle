using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private SpellSlotCntrl[] spellSlots;
    [SerializeField] private Sprite imageSprite;
    [SerializeField] private SpellSO testSpell;

    // Start is called before the first frame update
    void Start()
    {
        InitSpellSlot(0, testSpell);
        InitSpellSlot(1, testSpell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSpellSlot(int index, SpellSO spell)
    {
        spellSlots[index].Set(spell.spellSprite);
        spellSlots[index].Set(1.0f);
        spellSlots[index].Set(Color.blue);
    }
}
