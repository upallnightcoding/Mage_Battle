using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_GainXp : Quest
{
    private int xp = 0;

    public Quest_GainXp(string description, int xp) 
        : base(description)
    {
        this.xp = xp;
    }

    public override void Completed()
    {
        
    }

    public override bool Requirement(HealthSystem healthSystem)
    {
        return (healthSystem.Xp > xp);
    }
}
