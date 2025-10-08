using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorGroundChecker : MonoBehaviour
{
    [SerializeField] private Collider _groundCollider;
    
    void OnCollisionStay(Collision collision)
    {
        float targetY = _groundCollider.bounds.max.y + 1f;
        Transform objTransform = collision.transform;
        if (collision.gameObject.GetComponent<Collectable>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.transform.position = new Vector3(objTransform.position.x, targetY, objTransform.position.z);
        }
    }
}
