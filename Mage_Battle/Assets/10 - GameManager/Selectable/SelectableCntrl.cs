using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCntrl : MonoBehaviour
{
    [SerializeField] private SelectableType selectable;

    public SelectableType GetSelectable()
    {
        return (selectable);
    }
}

public enum SelectableType
{
    Enemy,
    Weapon,
    Gold
}
