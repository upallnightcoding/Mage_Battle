using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellSlotCntrl : MonoBehaviour
{
    [SerializeField] private Image spellImage;
    [SerializeField] private TMP_Text label;
    [SerializeField] private Slider coolDown;
    [SerializeField] private Image coolDownColor;
    [SerializeField] private GameObject spellFrameSelected;

    private bool toggleFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSpellFrame()
    {
        toggleFrame = !toggleFrame;
        spellFrameSelected.SetActive(toggleFrame);
    }

    public void SetSprite(Sprite sprite) => this.spellImage.sprite = sprite;

    public void SetDisplayBar(float value) => this.coolDown.value = value;

    public void SetDisplayColor(Color color) => coolDownColor.color = color;
}
