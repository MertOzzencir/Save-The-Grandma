using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Spawnable _spawnPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    public float SpawnTimer;
    private Vector3 _objSpawnRotation;

    public Dictionary<Transform, SpawnSpec> _spawnAvaliableArray = new Dictionary<Transform, SpawnSpec>();
    void Start()
    {
        foreach (var a in _spawnPoints)
        {
            _spawnAvaliableArray.Add(a, new SpawnSpec(false, 0));
            Spawn(a);
        }
    }
    void Update()
    {
        foreach (var a in _spawnAvaliableArray)
        {
            if (a.Value.Avaliable == false)
                continue;
            a.Value.Timer += Time.deltaTime;
            if (a.Value.Timer >SpawnTimer)
            {
                Spawn(a.Key);
                a.Value.Avaliable = false;
                a.Value.Timer = 0;
            }
        }
    }

    private void Spawn(Transform a)
    {
        _objSpawnRotation = new Vector3(0, UnityEngine.Random.Range(0, 180f), 0);
        Spawnable refs = Instantiate(_spawnPrefab, a.position, Quaternion.Euler(_objSpawnRotation));
        refs.transform.parent = a.transform;
        refs.SetSpawnerObject(this);
    }

    void OnDrawGizmos()
    {
        if (_spawnPoints == null)
            return;
        foreach (var a in _spawnPoints)
        {
            Gizmos.DrawSphere(a.position, 3f);
        }
    }
}

public class SpawnSpec
{
    public bool Avaliable;
    public float Timer;

    public SpawnSpec(bool avaliable, float timer)
    {
        this.Avaliable = avaliable;
        this.Timer = timer;
    }

}
