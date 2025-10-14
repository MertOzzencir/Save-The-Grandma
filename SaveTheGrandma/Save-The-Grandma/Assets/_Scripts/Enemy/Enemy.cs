using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector2 _speedMinMax;
    public Transform[] _waypoints;

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
    public EnemyStunned EnemyStunned{ get; set; }
    public Collectable CurrentEatTarget { get; set; }
    private StateMachine _enemyStateMachine;
    private float _speed;
    public int _waypointIndex { get; set; }
    public Vector3 StunnedDirection{ get; set; }
    private Rigidbody _rb;
    private Animator _anim;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _speed = Random.Range(_speedMinMax.x, _speedMinMax.y);
    }
    void Start()
    {
        _enemyStateMachine = new StateMachine();
        EnemyIdle = new EnemyIdle(_enemyStateMachine, this,_anim);
        EnemyMove = new EnemyMove(_enemyStateMachine, this,_anim);
        EnemyPatrol = new EnemyPatrol(_enemyStateMachine, this,_anim, _collectableMask, _repeatStateTimer, _checkRadius);
        EnemyEat = new EnemyEat(_enemyStateMachine, this, _anim);
        EnemyStunned = new EnemyStunned(_enemyStateMachine, this,_anim,StunnedDirection);
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

    public void MoveDistanceCheck(Vector3 CheckDistancePosition, EnemyState successChangeState,float distance)
    {
         if (Vector3.Distance(transform.position, CheckDistancePosition) < distance)
        {
            _enemyStateMachine.ChangeState(successChangeState);
        }
    }
    public void Move(Vector3 moveDirection)
    {
        LookRotationToTarget(moveDirection);
        Vector3 sa;
        sa = _rb.velocity;
        _rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * _speed;
        _rb.velocity = new Vector3(_rb.velocity.x,sa.y,_rb.velocity.z);
    }
    public Vector3 GetMoveDirection() => (_waypoints[_waypointIndex].position - transform.position).normalized;
    public Transform GetCurrentWayPoint() => _waypoints[_waypointIndex];
  
    public void FindNextWayPoint()
    {
        if (_waypointIndex >= _waypoints.Length - 1)
        {
            _waypointIndex = 0;    
            return;
        }
        _waypointIndex++;
    }
    public void LookRotationToTarget(Vector3 lookRotationToTarget)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookRotationToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, .15f);
    }

    public IEnumerator TurnEnemy(Rigidbody objectRB)
    {
        float _turn = 0;

        while (_turn <= 1f)
        {
            float step = 72f * Time.deltaTime;
            _turn += Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, step , 0);
            objectRB.MoveRotation(objectRB.rotation * deltaRotation);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        _enemyStateMachine.ChangeState(EnemyPatrol);
    }
    public void DestroyEatObject()
    {
        Destroy(CurrentEatTarget.gameObject);
    }
    public void Stunned()
    {
        _enemyStateMachine.ChangeState(EnemyStunned);
    }

    void OnDrawGizmos()
    {
        foreach (var a in _waypoints)
        {
            Gizmos.DrawWireSphere(a.position, 2f);
        }
    }
}
