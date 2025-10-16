using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(StateMachine stateMachine,Enemy enemy,Animator anim,Sprite stateIcon,EntityIndicatorHandler indicatorManager) : base(stateMachine,enemy,anim,stateIcon,indicatorManager)
    {
        
    }


    private float tempTimer;
    public override void Enter()
    {
        base.Enter();
        tempTimer = 0;
        EnemyAnim.SetBool("canIdle", true);
    }
    public override void Exit()
    {
        EnemyAnim.SetBool("canIdle", false);
    }
    public override void Update()
    {
        tempTimer += Time.deltaTime;
        
        if (tempTimer > Enemy.IdleTimer)
            StateMachine.ChangeState(Enemy.EnemyMove);
    }
}
