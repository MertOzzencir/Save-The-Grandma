using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyStunned : EnemyState
{
    public EnemyStunned(StateMachine stateMachine, Enemy enemy, Animator animator,Sprite stateIcon,EntityIndicatorHandler indicatorManager,Vector3 dir) : base(stateMachine, enemy, animator,stateIcon,indicatorManager)
    {
        _dir = dir;
    }
    Vector3 _dir;
    private float _timer;
    public override void Enter()
    {
        base.Enter();
        Enemy.Grandma = null;
        IndicatorManager.RestartIndicator();
        Enemy.transform.forward = _dir;
        _timer = 0;
        Enemy.CurrentEatTarget = null;
        EnemyAnim.SetTrigger("canStunned");
    }
    public override void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > 2f)
        {
            StateMachine.ChangeState(Enemy.EnemyPatrol);
        }
    }



}
