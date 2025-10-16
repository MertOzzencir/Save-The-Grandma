using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnable : MonoBehaviour
{
    public Spawner SpawnOwner { get; set; }

    public void SetSpawnerObject(Spawner obj)
    {
        SpawnOwner = obj;
    }
    public void ResetSpawner()
    {
        SpawnOwner._spawnAvaliableArray[transform.parent].Avaliable = true;
        SpawnOwner._spawnAvaliableArray[transform.parent].Timer = 0;
    }
    
}
