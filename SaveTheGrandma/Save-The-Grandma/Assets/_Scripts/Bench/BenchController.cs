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
       
    }

    public void SetActiveBench(Bench bench)
    {
        if (ActiveBench != null)
            ActiveBench.OpenCraftMenu();
        ActiveBench = bench;
    }
    public void ResetActiveBench()
    {
        ActiveBench = null;
    }



}
