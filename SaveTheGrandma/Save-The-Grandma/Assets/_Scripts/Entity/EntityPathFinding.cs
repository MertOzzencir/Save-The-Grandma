using UnityEngine;

public class EntityPathFinding : MonoBehaviour
{
    public Collider PathSurface{ get; set; }

    public Vector3 ChoosedPosition { get; set; }

    void Awake()
    {
        var ground = GameObject.FindGameObjectWithTag("Ground");
        if (ground != null)
        {
            PathSurface = ground.GetComponent<Collider>();
        }

    }
    public void ChooseMovePosition()
    {
        float x = Random.Range(PathSurface.bounds.min.x, PathSurface.bounds.max.x);
        float z = Random.Range(PathSurface.bounds.min.z, PathSurface.bounds.max.z);
        Vector3 position = new Vector3(x, PathSurface.bounds.max.y, z);
        ChoosedPosition = position;
    }
    public Vector3 GetChoosedPosition()
    {
        Debug.Log("Choosed by " + transform.name + "Choosed Direction: " + ChoosedPosition);
        return ChoosedPosition;
    }
    public Vector3 MoveDirectionNormalized()
    {

        return (ChoosedPosition - transform.position).normalized;
    }
    
    public void LookRotationToTarget(Vector3 lookDirection,float percent)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, percent);
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
    }
}
