using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyState
{
    public EnemyMove(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }
    private Vector3 _moveDirection;

    public override void Enter()
    {
        _moveDirection = Enemy.GetMoveDirection();
    }
    public override void Exit()
    {
        Enemy.FindNextWayPoint();

    }
    public override void Update()
    {
        if (Vector3.Distance(Enemy.transform.position, Enemy._waypoints[Enemy._waypointIndex].position) < 2f)
        {
            StateMachine.ChangeState(Enemy.EnemyIdleState);
        }
    }
    public override void FixedUpdate()
    {
        Enemy.Move(_moveDirection);
        Enemy.LookRotationToTarget();


    }

}
