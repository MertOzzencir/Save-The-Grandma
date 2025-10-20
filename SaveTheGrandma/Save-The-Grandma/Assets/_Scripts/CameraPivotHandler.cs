using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pivotObject;
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == _pivotObject)
            Debug.Log("sa");
        
    }
}
