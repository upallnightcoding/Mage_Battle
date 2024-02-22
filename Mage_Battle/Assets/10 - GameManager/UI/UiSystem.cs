using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSystem : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private UiSpellSlotCntrl[] uiSpellSlots;
    [SerializeField] private UiSpellSlotCntrl shieldSlot;

    [SerializeField] private Slider healthBar;

    private float health = 100;
    private float maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnable()
    {
        EventManager.Instance.OnSpellCoolDown += UpdateSpellBar;
        EventManager.Instance.OnSetFullSpellBar += SetFullSpellBar;
    }

    public void OnDisable()
    {
        EventManager.Instance.OnSpellCoolDown -= UpdateSpellBar;
        EventManager.Instance.OnSetFullSpellBar -= SetFullSpellBar;
    }

    /**
     * NewGame() -
     */
    public void NewGame()
    {
        InitAllSpellSlots();

        healthBar.value = 1.0f;
    }

    public void updateHealth(float value)
    {
        health += value;
        healthBar.value = health / maxHealth;
    }

    /**
     * InitAllSpellSlots() - 
     */
    public void InitAllSpellSlots()
    {
        for (int slot = 0; slot < gameData.nSpellSlots; slot++)
        {
            //uiSpellSlots[slot].SetSprite(gameData.defaultSpellDisplay.spellSprite);
            //uiSpellSlots[slot].SetDisplayBar(0.0f);
            //uiSpellSlots[slot].SetLabel(slot + 1);
            uiSpellSlots[slot].SetSpellSlot(slot, gameData.defaultSpellDisplay);
        }

        shieldSlot.SetSprite(gameData.shieldSprites[0]);
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

    private void SetFullSpellBar(int slot)
    {
        uiSpellSlots[slot].SetDisplayColor(Color.red);
        uiSpellSlots[slot].SetDisplayBar(1.0f);
    }

    private void UpdateSpellBar(int slot, float percentage)
    {
        uiSpellSlots[slot].SetDisplayColor(Color.yellow);
        uiSpellSlots[slot].SetDisplayBar(percentage);
    }
}

