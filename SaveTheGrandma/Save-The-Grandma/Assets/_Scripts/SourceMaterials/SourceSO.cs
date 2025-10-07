using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Source", menuName = "Source/New Source")]
public class SourceSO : ScriptableObject
{
    public ToolType AToolCanDig;
    public int MaterialHeath;
    public int MaterialDropCount;
    public Collectable SourceDropMaterial;
    public Vector3 OnChildDeathForceDirection;
    public float OnChildDeathForcePower;
    public float SpawnTimer;

    public Vector3 GetRandomDirection()
    {
        float x = Random.Range(-OnChildDeathForceDirection.x, OnChildDeathForceDirection.x);
        float z = Random.Range(-OnChildDeathForceDirection.z, OnChildDeathForceDirection.z);
        return new Vector3(x, OnChildDeathForceDirection.y, z);
    }

}
