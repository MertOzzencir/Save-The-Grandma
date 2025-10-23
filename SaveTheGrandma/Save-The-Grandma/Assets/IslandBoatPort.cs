using UnityEngine;

public class IslandBoatPort : MonoBehaviour
{

    [SerializeField] private Transform _enemySpawnPositionOnIsland;
    public void CarryEnemyToIsland(Enemy _enemyToSpawn)
    {
        Instantiate(_enemyToSpawn, _enemySpawnPositionOnIsland.position, Quaternion.identity);
    }
   
}
