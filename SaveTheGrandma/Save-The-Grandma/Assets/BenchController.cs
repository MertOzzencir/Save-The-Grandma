using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour
{
    public static BenchController Instance;

    public Bench[] AllBenches;
    public Bench ActiveBench;
    void Start()
    {
        Instance = this;
        AllBenches = FindObjectsByType<Bench>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var a in AllBenches)
        {
            Debug.Log(a);
        }
    }

    public void SetActiveBench(Bench bench)
    {
        ActiveBench = bench;
    }
    public void ResetActiveBench()
    {
        ActiveBench = null;
    }



}
