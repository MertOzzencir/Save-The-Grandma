using UnityEngine;

public class PathColliderManager : MonoBehaviour
{
    public static Collider PathSurface;
    void Awake()
    {
        Initialize();
    }
    public static void Initialize()
    {
        var ground = GameObject.FindGameObjectWithTag("Ground");
        if (ground != null)
        {
            PathSurface = ground.GetComponent<Collider>();
        }
    }
     public static void UpdatePathMeshCollider(Collider newCollider)
    {
        PathSurface = newCollider;
    }

}
