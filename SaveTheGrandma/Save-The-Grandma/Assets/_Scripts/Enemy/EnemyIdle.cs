using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(StateMachine stateMachine,Enemy enemy) : base(stateMachine,enemy)
    {
    }


    private float tempTimer;
    public override void Enter()
    {
        tempTimer = 0;
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        tempTimer += Time.deltaTime;
        
        if (tempTimer > Enemy.IdleTimer)
            StateMachine.ChangeState(Enemy.EnemyMove);
    }
}
