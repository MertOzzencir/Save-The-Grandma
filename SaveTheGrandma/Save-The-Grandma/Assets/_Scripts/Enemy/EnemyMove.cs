using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyState
{
    EntityPathFinding _pathFinder;

    public EnemyMove(StateMachine stateMachine, Enemy enemy,Animator anim,Sprite stateIcon,EntityIndicatorHandler indicatorManager,EntityPathFinding pathFinder) : base(stateMachine, enemy,anim,stateIcon,indicatorManager)
    {
        _pathFinder = pathFinder;
    }
    private Vector3 _moveDirection;

    public override void Enter()
    {
        base.Enter();
        EnemyAnim.SetBool("canMove", true);
        _pathFinder.ChooseMovePosition();
        _moveDirection = _pathFinder.MoveDirectionNormalized();
    }
    public override void Exit()
    {
        EnemyAnim.SetBool("canMove", false);
    }
    public override void Update()
    {
        Enemy.FindGrandma();
        if (Enemy.Grandma != null)
            StateMachine.ChangeState(Enemy.EnemyGrandmaChase);

        Enemy.MoveDistanceCheck(Enemy.transform,_pathFinder.GetChoosedPosition(), Enemy.EnemyPatrol,7f);
    }
    public override void FixedUpdate()
    {
        Enemy.Move(_moveDirection);
    }

}
