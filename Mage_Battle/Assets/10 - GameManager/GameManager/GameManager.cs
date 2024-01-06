using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private UiCntrl uiCntrl;

    public static GameManager Instance = null;

    private SpellSystem spellSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        spellSystem = GetComponent<SpellSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    /**
     * NewGame() - 
     */
    public void NewGame()
    {
        uiCntrl.NewGame();

        for (int i = 0; i < gameData.testSpells.Length; i++)
        {
            Set(gameData.testSpells[i]);
        }
    }

    /**
     * Cast() - 
     */
    public void Cast(Vector3 position, Vector3 direction)
    {
        CastInfo castInfo = spellSystem.Cast(position, direction);

        uiCntrl.DrainSpellBar(castInfo);
    }

    /**
     * SelectSpell() - 
     */
    public void SelectSpell(int slot)
    {
        spellSystem.Select(slot);
    }

    public void Set(SpellSO spell)
    {
        int slot = spellSystem.Add(spell);

        uiCntrl.Set(slot, spell);
    }

    public void UpdateCoolDown(int slot, float percentage)
    {
        uiCntrl.UpdateSpellBar(slot, percentage);
    }

    public void SetFullSpellBar(int slot)
    {
        uiCntrl.SetFullSpellBar(slot);
    }
}
