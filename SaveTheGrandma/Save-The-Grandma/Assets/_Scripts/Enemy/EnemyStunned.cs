using UnityEngine;

public class EnemyStunned : EnemyState
{
    EntityPathFinding _pathfinder;
    Rigidbody _enemyRB;
    public EnemyStunned(StateMachine stateMachine, Enemy enemy, Animator animator,Sprite stateIcon,EntityIndicatorHandler indicatorManager,Vector3 dir,EntityPathFinding pathFinder,Rigidbody enemyRB) : base(stateMachine, enemy, animator,stateIcon,indicatorManager)
    {
        _dir = dir;
        _pathfinder = pathFinder;
        _enemyRB = enemyRB;
    }
    Vector3 _dir;
    private float _timer;
    public override void Enter()
    {
        base.Enter();
        _pathfinder.Agent.enabled = false;
        _enemyRB.isKinematic = false;
        _enemyRB.useGravity = true;
        Enemy.Grandma = null;
        IndicatorManager.RestartIndicator();
        Enemy.transform.forward = _dir;
        _timer = 0;
        Enemy.CurrentEatTarget = null;
        EnemyAnim.SetTrigger("canStunned");
    }
    public override void Exit()
    {
        base.Exit();
         _pathfinder.Agent.enabled = true;
        _enemyRB.isKinematic = true;
        _enemyRB.useGravity = false;
    }
    public override void Update()
    {
        if (_pathfinder.Agent.isOnNavMesh)
            _pathfinder.Agent.ResetPath();
        
        _timer += Time.deltaTime;
        if(_timer > 3f)
        {
            StateMachine.ChangeState(Enemy.EnemyPatrol);
        }
    }



}
