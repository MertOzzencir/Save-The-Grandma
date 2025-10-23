using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolTurning : EnemyState
{
    public EnemyPatrolTurning(StateMachine stateMachine, Enemy enemy, Animator animator, Sprite stateIcon, EntityIndicatorHandler indicatorManager) : base(stateMachine, enemy, animator, stateIcon, indicatorManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _timer = 0;


    }
    public override void Exit()
    {
        base.Exit();
    }
    private float _timer;
    public override void Update()
    {
        base.Update();
        Enemy.FindGrandma();
        _timer += Time.deltaTime;
        float step = 72f * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, step * 5, 0);
        Enemy.transform.rotation = Quaternion.Lerp(Enemy.transform.rotation, Enemy.transform.rotation * deltaRotation, 0.5f);
        if (_timer >= 1.5f)
        {
            StateMachine.ChangeState(Enemy.EnemyPatrol);
        }
    }


}
