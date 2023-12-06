using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private SpellSlotCntrl[] spellSlots;
    [SerializeField] private Sprite imageSprite;

    // Start is called before the first frame update
    void Start()
    {
        spellSlots[0].Set(imageSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
