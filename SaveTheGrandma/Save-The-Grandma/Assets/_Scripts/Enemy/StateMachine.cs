using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    private EnemyState _currentState;

    public void Initialize(EnemyState setupState)
    {
        _currentState = setupState;
        _currentState.Enter();
    }
    public void ChangeState(EnemyState nextState)
    {
        _currentState.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }
    public void UpdateState()
    {
        _currentState.Update();
    }
    public void FixedUpdateState()
    {
        _currentState.FixedUpdate();
        
    }
    
}
