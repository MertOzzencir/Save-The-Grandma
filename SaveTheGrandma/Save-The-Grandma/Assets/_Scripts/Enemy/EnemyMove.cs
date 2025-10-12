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
        Enemy.MoveDistanceCheck(Enemy._waypoints[Enemy._waypointIndex].position, Enemy.EnemyPatrol,2f);
    }
    public override void FixedUpdate()
    {
        Enemy.Move(_moveDirection);
        Enemy.LookRotationToTarget(_moveDirection);


    }

}
