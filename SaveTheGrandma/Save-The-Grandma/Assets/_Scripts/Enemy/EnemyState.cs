using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public StateMachine StateMachine;
    public Enemy Enemy;
    public Animator EnemyAnim;
    public Sprite StateIcon;
    public EntityIndicatorHandler IndicatorManager;
    public EnemyState(StateMachine stateMachine, Enemy enemy,Animator animator,Sprite stateIcon,EntityIndicatorHandler indicatorManager)
    {
        StateMachine = stateMachine;
        Enemy = enemy;
        EnemyAnim = animator;
        StateIcon = stateIcon;
        IndicatorManager = indicatorManager;
    }

    public virtual void Enter()
    {
        IndicatorManager.StateIconHandle(StateIcon);
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
