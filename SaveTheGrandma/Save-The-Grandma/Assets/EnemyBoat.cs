using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBoat : Spawnable
{
    [SerializeField] private List<Enemy> _enemyPrefab;
    [SerializeField] private Collider Water;
    private EntityPathFinding _pathFinder;
    private List<IslandBoatPort> _currentPortToTransfer;
    private IslandBoatPort _pickedPort;
    private bool _canTransport;
    private bool _canChooseRandomPosition;
    private void Awake()
    {
        _pathFinder = GetComponent<EntityPathFinding>();
        _canTransport = true;
        _currentPortToTransfer = new List<IslandBoatPort>();
    }

    void Start()
    {
        Water = GameObject.FindGameObjectWithTag("Water").GetComponent<Collider>();
        _canChooseRandomPosition = true;
        FindPorts();
        _pathFinder.ChooseMovePosition(Water);

    }
    void Update()
    {
       
        if (_pickedPort != null)
        {
            if (_canTransport)
            {
                if (Vector3.Distance(transform.position, _pickedPort.transform.position) < 20f)
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
            FindPorts();
            Vector3 movePosition = SpawnOwner.transform.position;
            if (_canChooseRandomPosition && !_canTransport)
            {
                movePosition = SpawnOwner.transform.position;
                _pathFinder.MoveToDestination(movePosition);
                ResetBoat();
            }

            else if (_canChooseRandomPosition && _canTransport)
            {
                movePosition = _pathFinder.GetChoosedPosition();
                _pathFinder.MoveToDestination(movePosition);
                _canChooseRandomPosition = false;
            }
            else
            {
                if (Vector3.Distance(transform.position,movePosition) < 20f)
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
    private void FindPorts()
    {
        _currentPortToTransfer.Clear();
        IslandBoatPort[] tempArray = FindObjectsByType<IslandBoatPort>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (tempArray.Length < 1)
            return;
        _currentPortToTransfer = tempArray.ToList();
        _pickedPort = _currentPortToTransfer[0];
        foreach (var a in _currentPortToTransfer)
        {
            if (a.PortID < _pickedPort.PortID)
            {
                _pickedPort = a;
            }
        }
        

        _pathFinder.MoveToDestination(_pickedPort.transform.position);
        
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
                _pickedPort.CarryEnemyAnimation();
                yield return new WaitForSeconds(1f);
                _pickedPort.CarryEnemyToIsland(a);
            }
        }
        _pathFinder.Agent.enabled = true;
        _pathFinder.MoveToDestination(SpawnOwner.transform.position);
    }
}
