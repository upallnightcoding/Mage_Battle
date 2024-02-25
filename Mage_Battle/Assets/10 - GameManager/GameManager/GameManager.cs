using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private UiSystem uiCntrl;

    public static GameManager Instance = null;

    private SpellSystem spellSystem;
    private EnemySystem enemySystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            spellSystem = GetComponent<SpellSystem>();
            enemySystem = GetComponent<EnemySystem>();
        }
        else
        {
            Destroy(gameObject);
        }

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

        for (int slot = 0; slot < gameData.testSpells.Length; slot++)
        {
            Set(slot, gameData.testSpells[slot]);
        }
    }

    /**
     * Cast() - 
     */
    public void Cast(int slot, Vector3 position, Vector3 direction)
    {
        CastInfo castInfo = spellSystem.Cast(slot, position, direction);

        uiCntrl.DrainSpellBar(slot, castInfo);
    }

    /**
     * Set() - 
     */
    public void Set(int slot, SpellSO spell)
    {
        spellSystem.Add(slot, spell);

        uiCntrl.Set(slot, spell);
    }
}


