using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiSystem : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private UiSpellSlotCntrl[] uiSpellSlots;
    [SerializeField] private UiSpellSlotCntrl shieldSlot;

    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text expPoints;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private GameObject spellPanel;

    [SerializeField] private GameObject yourDeadPanel;

    [SerializeField] private HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
        healthPanel.SetActive(false);
        spellPanel.SetActive(false);
    }

    /**
     * NewGame() -
     */
    public void NewGame()
    {
        InitAllSpellSlots();

        healthBar.value = 1.0f;

        mainMenuPanel.SetActive(false);
        healthPanel.SetActive(true);
        spellPanel.SetActive(true);
    }

  

    /*private void UpdateExpPoints(int enemyId, int value)
    {
        expPointsValue += value;
        expPoints.text = expPointsValue.ToString();
    }*/

    private void HeroDeath()
    {
        yourDeadPanel.SetActive(true);
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

    public void DrainSpellBar(int slot, CastInfo castInfo)
    {
        uiSpellSlots[slot].SetDisplayBar(castInfo.Drain);
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

    private void UpdateUi()
    {
        healthBar.value = healthSystem.Health / 100.0f;
        Debug.Log($"Update UI ... {healthSystem.Health}");
    }

    private void OnEnable()
    {
        EventSystem.Instance.OnSpellCoolDown += UpdateSpellBar;
        EventSystem.Instance.OnSetFullSpellBar += SetFullSpellBar;
        //EventSystem.Instance.OnKillEnemy += UpdateExpPoints;
        //EventSystem.Instance.OnHeroDamage += UpdateHealth;
        EventSystem.Instance.OnHeroDeath += HeroDeath;

        EventSystem.Instance.OnUpdateUi += UpdateUi;
    }

    private void OnDisable()
    {
        EventSystem.Instance.OnSpellCoolDown -= UpdateSpellBar;
        EventSystem.Instance.OnSetFullSpellBar -= SetFullSpellBar;
        //EventSystem.Instance.OnKillEnemy -= UpdateExpPoints;
        //EventSystem.Instance.OnHeroDamage -= UpdateHealth;
        EventSystem.Instance.OnHeroDeath -= HeroDeath;

        EventSystem.Instance.OnUpdateUi -= UpdateUi;
    }
}

