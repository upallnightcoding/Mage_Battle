using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest 
{
    public string Description { set; get; } = "";

    public Quest(string description)
    {
        this.Description = description;
    }

    public abstract bool Requirement(HealthSystem healthSystem);
    public abstract void Completed();
}
