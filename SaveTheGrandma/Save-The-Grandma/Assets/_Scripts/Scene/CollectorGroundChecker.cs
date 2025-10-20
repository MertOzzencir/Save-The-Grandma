using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorGroundChecker : MonoBehaviour
{
    [SerializeField] private Collider _groundCollider;
    void OnTriggerStay(Collider other)
    {
           float targetY = _groundCollider.bounds.max.y + 1f;
        Transform objTransform = other.transform;
        if (other.gameObject.GetComponent<Collectable>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.gameObject.transform.position = new Vector3(objTransform.position.x, targetY, objTransform.position.z);
        }
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.HandleDeath();
        }
    }
   
}
