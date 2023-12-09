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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(Sprite spellSprite) => this.spellImage.sprite = spellSprite;

    public void Set(float value) => this.coolDown.value = value;

    public void Set(Color color) => coolDownColor.color = color;
}
