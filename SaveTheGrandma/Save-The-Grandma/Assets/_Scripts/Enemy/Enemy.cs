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
    [Header("Patrol Settings")]
    [SerializeField] private LayerMask _collectableMask;
    [SerializeField] private float _repeatStateTimer;
    [SerializeField] private float _checkRadius;
    
    public EnemyIdle EnemyIdle { get; set; }
    public EnemyMove EnemyMove { get; set; }
    public EnemyPatrol EnemyPatrol { get; set; }
    public EnemyEat EnemyEat { get; set; }
    public Collectable CurrentEatTarget { get; set; }
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
        EnemyIdle = new EnemyIdle(_enemyStateMachine, this);
        EnemyMove = new EnemyMove(_enemyStateMachine, this);
        EnemyPatrol = new EnemyPatrol(_enemyStateMachine, this, _collectableMask, _repeatStateTimer, _checkRadius);
        EnemyEat = new EnemyEat(_enemyStateMachine, this);
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
    public void LookRotationToTarget(Vector3 lookRotationToTarget)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookRotationToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, .15f);
    }

    public IEnumerator TurnEnemy(Transform turnObject)
    {
        float _turn = 0;

        while (_turn <= 1f)
        {
            float step = 72f * Time.deltaTime;
            _turn += Time.deltaTime;
            turnObject.Rotate(0, step, 0);
            yield return null;
        }
        Debug.Log("Finished?");
        yield return new WaitForSeconds(.1f);
        _enemyStateMachine.ChangeState(EnemyPatrol);
    }
    public void DestroyEatObject()
    {
        Destroy(CurrentEatTarget.gameObject);    
    }

    void OnDrawGizmos()
    {
        foreach (var a in _waypoints)
        {
            Gizmos.DrawWireSphere(a.position, 2f);
        }
    }
}
