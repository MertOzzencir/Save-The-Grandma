using UnityEngine;

public class EnemyGrandmaChase : EnemyState
{
    private Grandma _target;
    private EntityPathFinding _pathFinder;
    public EnemyGrandmaChase(StateMachine stateMachine, Enemy enemy, Animator animator, Sprite stateIcon, EntityIndicatorHandler indicatorManager,EntityPathFinding pathFinder) : base(stateMachine, enemy, animator, stateIcon, indicatorManager)
    {
        _pathFinder = pathFinder;
    }

    public override void Enter()
    {
        base.Enter();
        _target = Enemy.Grandma;
        Enemy.AttackMovement = true;
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        if (Enemy.Grandma == null)
        {
            StateMachine.ChangeState(Enemy.EnemyIdle);
            return;
        }

        dir = (_target.transform.position - Enemy.transform.position).normalized;
        if (Vector3.Distance(Enemy.transform.position, _target.transform.position) < 7.5f)
        {
            if (!Enemy.IsAttackOverload)
            {
                _pathFinder.LookRotationToTarget(dir,.85f);
                Enemy.AttackMovement = false;
                Enemy.IsAttackOverload = true;
                Enemy.StartCoroutine(Enemy.AttackToGrandma(dir));
            }
        }
    }
    private Vector3 dir;
    public override void FixedUpdate()
    {
        if (Enemy.AttackMovement)
        {
            Enemy.Move(dir);
        }
    }
}
