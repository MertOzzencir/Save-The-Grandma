using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public StateMachine StateMachine;
    public Enemy Enemy;
    public Animator EnemyAnim;
    public EnemyState(StateMachine stateMachine, Enemy enemy,Animator animator)
    {
        StateMachine = stateMachine;
        Enemy = enemy;
        EnemyAnim = animator;
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
