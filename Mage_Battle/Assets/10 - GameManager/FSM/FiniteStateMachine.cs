using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private Dictionary<string, FiniteState> machine = null;
    private FiniteState currentState = null;
    private string nextState = null;

    public FiniteStateMachine()
    {
        this.machine = new Dictionary<string, FiniteState>();
    }

    /**
     * Add() - 
     */
    public void Add(FiniteState state)
    {
        machine.Add(state.Title, state);

        if (currentState == null)
        {
            currentState = state;
        }
    }

    public void OnUpdate(float dt)
    {
        nextState = currentState.OnUpdate(dt);

        if (nextState != null)
        {
            if (machine.TryGetValue(nextState, out FiniteState state))
            {
                currentState.OnExit();
                currentState = state;
                currentState.OnEnter();
            }
        }
    }
}
