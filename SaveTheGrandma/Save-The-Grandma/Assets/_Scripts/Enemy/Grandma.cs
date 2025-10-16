using UnityEngine;

[RequireComponent(typeof(EntityPathFinding))]
public class Grandma : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;
    private bool _canMove;
    private EntityPathFinding _pathFindging;
    void Start()
    {
        _pathFindging = GetComponent<EntityPathFinding>();
        _rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (!_canMove)
        {
            _pathFindging.ChooseMovePosition();
            _canMove = true;
        }
        if (Vector3.Distance(transform.position, _pathFindging.GetChoosedPosition()) < 8f)
        {
            _canMove = false;
        }

    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            _pathFindging.LookRotationToTarget(_pathFindging.MoveDirectionNormalized(),0.5f);
            _rb.velocity = new Vector3(_pathFindging.MoveDirectionNormalized().x, 0, _pathFindging.MoveDirectionNormalized().z) * _speed + new Vector3(0, _rb.velocity.y, 0);
        }

    }


}
