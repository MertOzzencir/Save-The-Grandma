using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spawnable
{

    [SerializeField] private int _attackDamage;
    [Header("Icons and Indicators")]
    [SerializeField] private Sprite[] _stateIcons;
    [Header("Movement")]
    [SerializeField] private Vector2 _speedMinMax;
    [SerializeField] private Vector2 _sizeMinMax;

    [Header("State Settings")]
    public float IdleTimer;
    [Header("Patrol Settings")]
    [SerializeField] private LayerMask _collectableMask;
    [SerializeField] private float _repeatStateTimer;
    [SerializeField] private float _checkRadius;

    public EnemyIdle EnemyIdle { get; set; }
    public EnemyMove EnemyMove { get; set; }
    public EnemyPatrol EnemyPatrol { get; set; }
    public EnemyEat EnemyEat { get; set; }
    public EnemyStunned EnemyStunned { get; set; }
    public EnemyGrandmaChase EnemyGrandmaChase{ get; set;}
    public Collectable CurrentEatTarget { get; set; }
    private StateMachine _enemyStateMachine;
    private float _speed;
    public int _waypointIndex { get; set; }
    public Vector3 StunnedDirection { get; set; }
    public Grandma Grandma { get; set; }
    public bool IsAttackOverload { get; set; }
    public bool AttackMovement{ get; set; }
    private Rigidbody _rb;
    private Animator _anim;
    private EntityIndicatorHandler _indicatorManager;
    private EntityPathFinding _entityPathFinder;


    void Awake()
    {
        _entityPathFinder = GetComponent<EntityPathFinding>();
        _indicatorManager = GetComponent<EntityIndicatorHandler>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();

 
        _speed = Random.Range(_speedMinMax.x, _speedMinMax.y);
        float randomSize = Random.Range(_sizeMinMax.x, _sizeMinMax.y);
        _speed = (_speed + randomSize) / randomSize;
        transform.localScale = Vector3.one * randomSize;
        _rb.mass = _rb.mass * randomSize * 3;
    }
    void Start()
    {
        _enemyStateMachine = new StateMachine();
        EnemyIdle = new EnemyIdle(_enemyStateMachine, this, _anim,_stateIcons[2],_indicatorManager);
        EnemyMove = new EnemyMove(_enemyStateMachine, this, _anim,_stateIcons[1],_indicatorManager,_entityPathFinder);
        EnemyPatrol = new EnemyPatrol(_enemyStateMachine, this, _anim,_stateIcons[3],_indicatorManager, _collectableMask, _repeatStateTimer, _checkRadius);
        EnemyEat = new EnemyEat(_enemyStateMachine, this, _anim,_stateIcons[0],_indicatorManager);
        EnemyStunned = new EnemyStunned(_enemyStateMachine, this, _anim, _stateIcons[2],_indicatorManager, StunnedDirection);
        EnemyGrandmaChase = new EnemyGrandmaChase(_enemyStateMachine, this, _anim, _stateIcons[0],_indicatorManager,_entityPathFinder);
        _enemyStateMachine.Initialize(EnemyIdle);
    }

    void Update()
    {
        _enemyStateMachine.UpdateState();
    }
    void FixedUpdate()
    {
        _enemyStateMachine.FixedUpdateState();
    }

    public void MoveDistanceCheck(Transform from,Vector3 to, EnemyState successChangeState, float distance)
    {
        if (Vector3.Distance(from.position, to) < distance)
        {
            _enemyStateMachine.ChangeState(successChangeState);
        }
    }
    public void Move(Vector3 moveDirection)
    {
        _entityPathFinder.LookRotationToTarget(moveDirection,.2f);
        Vector3 sa;
        sa = _rb.velocity;
        _rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * _speed;
        _rb.velocity = new Vector3(_rb.velocity.x, sa.y, _rb.velocity.z);
    }


    public IEnumerator TurnEnemy(Rigidbody objectRB)
    {
        float _turn = 0;

        while (_turn <= 1f)
        {
            float step = 72f * Time.deltaTime;
            _turn += Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, step, 0);
            objectRB.MoveRotation(objectRB.rotation * deltaRotation);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        _enemyStateMachine.ChangeState(EnemyPatrol);
    }
    private float _timer;
    public void FindGrandma()
    {
        _timer += Time.deltaTime;
        if (_timer < 1.5f)
            return;
        _timer = 0;
        Collider[] grandma = Physics.OverlapSphere(transform.position, 25f);
        foreach(var a in grandma)
        {
            if(a.TryGetComponent(out Grandma gm)){
                Grandma = gm;
            }
        }
    }

    public IEnumerator AttackToGrandma(Vector3 dir)
    {
        _rb.AddForce(dir * 100f, ForceMode.Impulse);
        Grandma.GetComponent<EntityHealth>().GetDamage(_attackDamage);
        yield return new WaitForSeconds(1f);
        IsAttackOverload = false;
        AttackMovement = true;
    }
    public void DestroyEatObject()
    {
        Destroy(CurrentEatTarget.gameObject);
    }
    public void Stunned()
    {
        _enemyStateMachine.ChangeState(EnemyStunned);
    }
    
    public void HandleDeath()
    {
        ResetSpawner();
        _indicatorManager.RestartIndicator();
        Destroy(gameObject,1);
    }

   
}
