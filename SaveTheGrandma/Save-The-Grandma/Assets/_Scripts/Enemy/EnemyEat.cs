using UnityEngine;

public class EnemyEat : EnemyState
{
    private float _timer;
    private EntityIndicatorHandler _indicatorHandler;
    public EnemyEat(StateMachine stateMachine, Enemy enemy,Animator anim,Sprite stateIcon,EntityIndicatorHandler indicatorManager) : base(stateMachine, enemy,anim,stateIcon,indicatorManager)
    {
        _indicatorHandler = indicatorManager;
    }
    public override void Enter()
    {
        base.Enter();
        _indicatorHandler.SetLastIndicatorColorToFinishColor();
        Enemy.CurrentEatTarget.Collected = true;
        Enemy.DestroyEatObject();
        Enemy.CurrentEatTarget = null;
    }
    public override void Exit()
    {
        _timer = 0;
    }
    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1f)
            StateMachine.ChangeState(Enemy.EnemyPatrol);
    }
}