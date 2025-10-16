using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyGrandmaChase : EnemyState
{
    private Grandma _target;
    public EnemyGrandmaChase(StateMachine stateMachine, Enemy enemy, Animator animator, Sprite stateIcon,EntityIndicatorHandler indicatorManager,Grandma grandma) : base(stateMachine, enemy, animator, stateIcon,indicatorManager)
    {
        _target = grandma;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        Vector3 dir = (_target.transform.position - Enemy.transform.position).normalized;
        Enemy.Move(dir);
    }
}
