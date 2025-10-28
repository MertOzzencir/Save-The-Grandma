using UnityEngine;
using UnityEngine.AI;

public class EntityPathFinding : MonoBehaviour
{
    public float EntitySpeed;

    public Vector3 ChoosedPosition { get; set; }
    public NavMeshAgent Agent { get; set; }

    private GameObject[] WaterCarves;
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        WaterCarves = GameObject.FindGameObjectsWithTag("WaterCarves");
    }
    void Start()
    {
        Agent.speed = EntitySpeed;
    }

    public void ChooseMovePosition(Collider PathSurfaceCollider)
    {
        float x = Random.Range(PathSurfaceCollider.bounds.min.x  , PathSurfaceCollider.bounds.max.x );
        float z = Random.Range(PathSurfaceCollider.bounds.min.z  , PathSurfaceCollider.bounds.max.z );
        Vector3 position = new Vector3(x, transform.position.y, z);
        ChoosedPosition = position;
    }
    public void MoveToDestination(Vector3 position)
    {
        if (Agent.isOnNavMesh)
        {
            Agent.SetDestination(position);
        }
    }
    public Vector3 GetChoosedPosition()
    {
        return ChoosedPosition;
    }
    public Vector3 MoveDirectionNormalized()
    {

        return (ChoosedPosition - transform.position).normalized;
    }

    public void LookRotationToTarget(Vector3 lookDirection, float percent)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, percent);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    public void ChooseMovePositionByEnemyBoat(Collider PathSurfaceCollider)
    {
        ChoosedPosition = Vector3.zero;
        bool isOutOfIsland = false;
        float x = Random.Range(PathSurfaceCollider.bounds.min.x  , PathSurfaceCollider.bounds.max.x );
        float z = Random.Range(PathSurfaceCollider.bounds.min.z, PathSurfaceCollider.bounds.max.z);
        
        foreach (var a in WaterCarves)
        {
            Collider islandBounds = a.GetComponent<Collider>();

            if (x > islandBounds.bounds.max.x || x < islandBounds.bounds.min.x)
            {
                if (z > islandBounds.bounds.max.z || z < islandBounds.bounds.min.z)
                {
                    isOutOfIsland = true;
                }
                else
                {
                    isOutOfIsland = false;
                    break;
                }
            }
            else
            {
                isOutOfIsland = false;
                break;
            }
        }
        if (!isOutOfIsland)
            ChooseMovePositionByEnemyBoat(PathSurfaceCollider);
        else
            ChoosedPosition = new Vector3(x, transform.position.y, z);
        
    }
   
}
