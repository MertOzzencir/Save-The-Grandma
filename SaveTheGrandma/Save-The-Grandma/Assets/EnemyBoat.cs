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
    private float _timer;
    private void Awake()
    {
        _pathFinder = GetComponent<EntityPathFinding>();
        _canTransport = true;
    }

    void Start()
    {
        FindPort();
    }

    void Update()
    {
        if (_currentPortToTransfer != null)
        {
            if (_canTransport)
            {
                if (Vector3.Distance(transform.position, _currentPortToTransfer.transform.position) < 9f)
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
            _currentPortToTransfer.CarryEnemyToIsland(a);
            yield return new WaitForSeconds(1f);
        }
        _pathFinder.Agent.enabled = true;
        _pathFinder.MoveToDestination(SpawnOwner.transform.position);
    }
}
