using UnityEngine;

public class EnemyGrandmaChase : EnemyState
{
    private Grandma _target;
    private EntityPathFinding _pathFinder;
    private EntityIndicatorHandler _indicator;
    public EnemyGrandmaChase(StateMachine stateMachine, Enemy enemy, Animator animator, Sprite stateIcon, EntityIndicatorHandler indicatorManager,EntityPathFinding pathFinder) : base(stateMachine, enemy, animator, stateIcon, indicatorManager)
    {
        _pathFinder = pathFinder;
        _indicator = indicatorManager;
    }

    public override void Enter()
    {
        base.Enter();
        Enemy.Grandma.RunFromEnemy();
        _indicator.RestartIndicator();
        _target = Enemy.Grandma;
        Enemy.AttackMovement = true;
    }
    public override void Exit()
    {
        Enemy.Grandma.SlowDown();
    }
    public override void Update()
    {
        if (Enemy.Grandma == null)
            return;

        if (Vector3.Distance(Enemy.transform.position, _target.transform.position) < 7.5f)
        {
            if (!Enemy.IsAttackOverload)
            {
                Vector3 dir = (_target.transform.position - Enemy.transform.position).normalized;
                Enemy.AttackMovement = false;
                Enemy.IsAttackOverload = true;
                Enemy.StartAttackToGrandma(dir);
            }
        }
    }
    public override void FixedUpdate()
    {
        if (Enemy.AttackMovement && Enemy.Grandma != null)
        {
            _pathFinder.MoveToDestination(Enemy.Grandma.transform.position);
        }
    }
}
