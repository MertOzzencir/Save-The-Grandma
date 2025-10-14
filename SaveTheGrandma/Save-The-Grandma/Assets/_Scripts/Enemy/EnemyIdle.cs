using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(StateMachine stateMachine,Enemy enemy,Animator anim) : base(stateMachine,enemy,anim)
    {
    }


    private float tempTimer;
    public override void Enter()
    {
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
