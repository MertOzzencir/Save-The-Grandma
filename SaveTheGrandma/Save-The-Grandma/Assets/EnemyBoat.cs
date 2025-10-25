using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : Spawnable
{
    [SerializeField] private List<Enemy> _enemyPrefab;
    [SerializeField] private Collider Water;
    private EntityPathFinding _pathFinder;
    private IslandBoatPort _currentPortToTransfer;
    private bool _canTransport;
    private bool _canChooseRandomPosition;
    private void Awake()
    {
        _pathFinder = GetComponent<EntityPathFinding>();
        _canTransport = true;
    }

    void Start()
    {
        Water = GameObject.FindGameObjectWithTag("Water").GetComponent<Collider>();
        _canChooseRandomPosition = true;
        FindPort();
    }

    void Update()
    {
        if (_currentPortToTransfer != null)
        {
            if (_canTransport)
            {
                if (Vector3.Distance(transform.position, _currentPortToTransfer.transform.position) < 20f)
                {
                    StartCoroutine(SpawnEnemyOnIsland());
                }
            }
            else
            {
                ResetBoat();
            }
        }
        else
        {
            FindPort();
            if (_canChooseRandomPosition && _canTransport)
            {
                _pathFinder.ChooseMovePosition(Water);
                _pathFinder.MoveToDestination(_pathFinder.GetChoosedPosition());
                _canChooseRandomPosition = false;
            }
            else
            {
                if (Vector3.Distance(transform.position, _pathFinder.GetChoosedPosition()) < 20f)
                {
                    _canChooseRandomPosition = true;
                }
            }
        }

    }

    private void ResetBoat()
    {
        if (Vector3.Distance(transform.position, SpawnOwner.transform.position) < 5f)
        {
            ResetSpawner();
            Destroy(gameObject);
        }
    }
    private void FindPort()
    {
        _currentPortToTransfer = PathColliderManager.PathSurface.GetComponent<Island>().BoatPort;
        if (_currentPortToTransfer != null)
        {
            _pathFinder.MoveToDestination(_currentPortToTransfer.transform.position);
        }
    }

    IEnumerator SpawnEnemyOnIsland()
    {
        _pathFinder.Agent.enabled = false;
        _canTransport = false;
        yield return null;
        foreach (var a in _enemyPrefab)
        {
            if (_currentPortToTransfer != null)
            {
                _currentPortToTransfer.CarryEnemyAnimation();
                yield return new WaitForSeconds(1f);
                _currentPortToTransfer.CarryEnemyToIsland(a);
            }
        }
        _pathFinder.Agent.enabled = true;
        _pathFinder.MoveToDestination(SpawnOwner.transform.position);
    }
}
