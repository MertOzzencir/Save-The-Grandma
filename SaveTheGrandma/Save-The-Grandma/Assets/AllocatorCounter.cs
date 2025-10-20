using UnityEngine;

public class AllocatorCounter : MonoBehaviour
{
    private float _timer;
     void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f) // her 1 saniyede bir
        {
            TotalGCMemoryUSED();
            _timer = 0f;
        }
    }
    void TotalGCMemoryUSED()
    {
        long memUsed = System.GC.GetTotalMemory(false);
        //Debug.Log((memUsed/ 1024) /1024 );
    }
}
