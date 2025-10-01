using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private MotherSource _materialControllerToSpawn;
    [SerializeField] private float _spawnFreq;
    [SerializeField] private Transform[] _spawnPoints;

    public Dictionary<Transform, SpawnSpec> _spawnAvaliableArray = new Dictionary<Transform, SpawnSpec>();
    public float _spawnTimer { get; set; }
    void Awake()
    {
        foreach (var a in _spawnPoints)
        {
            _spawnAvaliableArray.Add(a, new SpawnSpec(true, 0));
        }
    }
    void Update()
    {
        foreach (var a in _spawnAvaliableArray)
        {
            if (a.Value.Avaliable == false)
                continue;
            a.Value.Timer += Time.deltaTime;
            if ( a.Value.Timer > _spawnFreq )
            {
                Spawn(a.Key);
                a.Value.Avaliable = false;
                a.Value.Timer = 0;
            }
        }
    }

    private void Spawn(Transform a)
    {
        MotherSource refs = Instantiate(_materialControllerToSpawn, a.position, Quaternion.identity);
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
