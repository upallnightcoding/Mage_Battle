using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    }

    public void NewGame()
    {

    }

    public void Cast(Vector3 position, Vector3 direction)
    {
        CastInfo castInfo = spellSystem.Cast(position, direction);

        uiCntrl.DrainSpellBar(castInfo);
    }

    public void Set(SpellSO spell)
    {
        spellSystem.Set(spell);
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
