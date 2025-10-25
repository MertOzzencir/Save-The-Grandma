using UnityEngine;

[RequireComponent(typeof(EntityPathFinding))]
public class Grandma : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _canMove;
    private EntityPathFinding _pathFindging;
    void Start()
    {
        _pathFindging = GetComponent<EntityPathFinding>();
        _rb = GetComponent<Rigidbody>();
        _pathFindging.ChooseMovePosition(PathColliderManager.PathSurface);
    }


    void Update()
    {
        if (!_canMove)
        {
            _pathFindging.MoveToDestination(_pathFindging.GetChoosedPosition());
            _canMove = true;
        }
        if (Vector3.Distance(transform.position, _pathFindging.GetChoosedPosition()) < 6f)
        {
            _pathFindging.ChooseMovePosition(PathColliderManager.PathSurface);
            _canMove = false;
        }

    }

    public void RunFromEnemy()
    {
        _pathFindging.Agent.speed = _pathFindging.EntitySpeed * 1.75f;
        _pathFindging.ChooseMovePosition(PathColliderManager.PathSurface);
        _pathFindging.MoveToDestination(_pathFindging.GetChoosedPosition());
    }
    public void SlowDown()
    {
        if(gameObject != null)
            _pathFindging.Agent.speed = _pathFindging.EntitySpeed;
    }


}
