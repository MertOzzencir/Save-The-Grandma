using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : EnemyState
{
    private EntityPathFinding _pathfinder;
    private LayerMask _collectableMask;
    private float _repeatStateTimer;
    private float _checkRadius;
    private Collider[] _collectables;
    private Collectable _goalToCollect;
    private Vector3 _movePosition;
    private int _timer;
    private bool _canTurn;
    public EnemyPatrol(StateMachine stateMachine, Enemy enemy, Animator animator,Sprite stateIcon,EntityIndicatorHandler indicatorManager, LayerMask collectableMask, float repeatTimer, float checkRadius,EntityPathFinding pathfinder) : base(stateMachine, enemy, animator,stateIcon,indicatorManager)
    {
        _collectableMask = collectableMask;
        _repeatStateTimer = repeatTimer;
        _checkRadius = checkRadius;
        _pathfinder = pathfinder;
    }

    public override void Enter()
    {
        base.Enter();
        Enemy.FindGrandma();
        Enemy.transform.eulerAngles = new Vector3(0,Enemy.transform.eulerAngles.y,Enemy.transform.eulerAngles.z);
        EnemyAnim.SetBool("canPatrol", true);
        _timer += 1;
        _canTurn = true;
        _collectables = Physics.OverlapSphere(Enemy.transform.position, _checkRadius, _collectableMask);
        foreach (var a in _collectables)
        {
            Vector3 _tempMovePos = (a.transform.position - Enemy.transform.position).normalized;
            float isItInArea = Vector3.Dot(_tempMovePos, Enemy.transform.forward);
            if (isItInArea > 0.5f)
            {
                Enemy.CurrentEatTarget = a.GetComponent<Collectable>();
                _movePosition = a.transform.position;
                _pathfinder.MoveToDestination(_movePosition);
                break;
            }
        }

    }
    public override void Exit()
    {
        EnemyAnim.SetBool("canPatrol", false);
        _collectables = null;
    }
    public override void Update()
    {
        Enemy.FindGrandma();
        if (Enemy.Grandma != null)
        {
            StateMachine.ChangeState(Enemy.EnemyGrandmaChase);
            return;
        }
        if (Enemy.CurrentEatTarget != null)
        {
            IndicatorManager.ImplementEatIndicator(Enemy.CurrentEatTarget.transform);
            Enemy.MoveDistanceCheck(Enemy.transform,Enemy.CurrentEatTarget.transform.position, Enemy.EnemyEat, 4f);
            _timer = 0;
        }

        else
        {
            if (_timer <= _repeatStateTimer)
            {
                if (_canTurn)
                {
                    _canTurn = false;
                    StateMachine.ChangeState(Enemy.EnemyPatrolTurning);
                }
            }
            else
            {
                _timer = 0;
                StateMachine.ChangeState(Enemy.EnemyIdle);
            }
        }
    }
}
