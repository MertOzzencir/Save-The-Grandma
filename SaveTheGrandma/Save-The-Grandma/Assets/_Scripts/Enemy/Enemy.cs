using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector2 _speedMinMax;
    public Transform[] _waypoints;

    [Header("State Settings")]
    public float IdleTimer;
    public EnemyIdle EnemyIdleState { get; set; }
    public EnemyMove EnemyMoveState { get; set; }
    private StateMachine _enemyStateMachine;
    private float _speed;
    private Vector3 _currentWaypointDirection;
    public int _waypointIndex{ get; set; }
    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = Random.Range(_speedMinMax.x, _speedMinMax.y);
    }
    void Start()
    {
        _enemyStateMachine = new StateMachine();
        EnemyIdleState = new EnemyIdle(_enemyStateMachine, this);
        EnemyMoveState = new EnemyMove(_enemyStateMachine, this);
        _enemyStateMachine.Initialize(EnemyIdleState);


    }

    void Update()
    {
        _enemyStateMachine.UpdateState();
    }
    void FixedUpdate()
    {
        _enemyStateMachine.FixedUpdateState();
    }

    public void Move(Vector3 moveDirection)
    {
        _rb.velocity = moveDirection * _speed;
    }
    public Vector3 GetMoveDirection() => (_currentWaypointDirection = _waypoints[_waypointIndex].position - transform.position).normalized;
    public Transform GetCurrentWayPoint() => _waypoints[_waypointIndex];
  
    public void FindNextWayPoint()
    {
        if (_waypointIndex >= _waypoints.Length - 1 )
            return;
        _waypointIndex++;
    }
    public void LookRotationToTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_currentWaypointDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, .15f);
    }



    void OnDrawGizmos()
    {
        foreach (var a in _waypoints)
        {
            Gizmos.DrawWireSphere(a.position, 2f);
        }
    }
}
