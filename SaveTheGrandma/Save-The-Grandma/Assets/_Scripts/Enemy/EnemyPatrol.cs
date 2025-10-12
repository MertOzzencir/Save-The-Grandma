using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyPatrol : EnemyState
{
    private LayerMask _collectableMask;
    private float _repeatStateTimer;
    private float _checkRadius;
    private Collider[] _collectables;
    private Collectable _goalToCollect;
    private Vector3 _moveDirection;
    private int _timer;
    private bool _canTurn;
    public EnemyPatrol(StateMachine stateMachine, Enemy enemy, LayerMask collectableMask, float repeatTimer, float checkRadius) : base(stateMachine, enemy)
    {
        _collectableMask = collectableMask;
        _repeatStateTimer = repeatTimer;
        _checkRadius = checkRadius;
    }

    public override void Enter()
    {
        _timer += 1;
        _canTurn = true;
        _collectables = Physics.OverlapSphere(Enemy.transform.position, _checkRadius, _collectableMask);
        foreach (var a in _collectables)
        {
            _moveDirection = (a.transform.position - Enemy.transform.position).normalized;
            float isItInArea = Vector3.Dot(_moveDirection, Enemy.transform.forward);
            if (isItInArea > 0.5f)
            {
                Enemy.CurrentEatTarget =a.GetComponent<Collectable>();
                break;
            }
        }

    }
    public override void Exit()
    {
        _collectables = null;
        Debug.Log(_timer);
    }
    public override void Update()
    {
        if (Enemy.CurrentEatTarget != null)
        {
            Enemy.MoveDistanceCheck(Enemy.CurrentEatTarget.transform.position, Enemy.EnemyEat, 2.5f);
            _timer = 0;
        }
        
        else
        {
            if (_timer <= _repeatStateTimer)
            {
                if (_canTurn)
                {
                    _canTurn = false;
                    Debug.Log("Turning Begin");
                    Enemy.StartCoroutine(Enemy.TurnEnemy(Enemy.transform));
                }
            }
            else
            {
                _timer = 0;
                Debug.Log("State Changed");
                StateMachine.ChangeState(Enemy.EnemyIdle);
            }
        }
    }
    public override void FixedUpdate()
    {
        if (Enemy.CurrentEatTarget != null)
        {
            Enemy.Move(_moveDirection);
        }

    }


}
