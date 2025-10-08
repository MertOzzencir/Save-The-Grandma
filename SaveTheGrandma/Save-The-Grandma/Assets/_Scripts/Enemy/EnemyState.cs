using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public StateMachine StateMachine;
    public Enemy Enemy;
    public EnemyState(StateMachine stateMachine, Enemy enemy)
    {
        StateMachine = stateMachine;
        Enemy = enemy;
    }

    public virtual void Enter()
    {

    }
    public virtual void Exit()
    {
        
    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {
        
    }
   
}
