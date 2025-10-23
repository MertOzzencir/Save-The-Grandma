using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager Instance;

    private NavMeshSurface _manager;
    void Awake()
    {
        Instance = this;
        _manager = GetComponent<NavMeshSurface>();
    }

    public void BuildSurface()
    {
        StartCoroutine(RebuildNextFrame());
    }

    IEnumerator RebuildNextFrame()
    {
        yield return null;
        _manager.BuildNavMesh();
    }


}
