
using UnityEngine;

public abstract class Tools : MonoBehaviour
{
    public EntityAudioManager ToolAudioManager{ get; set;}
    public ToolsSO ToolData;
    public LayerMask TargetLayer;
    public Rigidbody RB;
    public Collider[] ObjectToCheck { get; set; }

    public bool isOverload{ get; set; }
    public virtual void StartUse()
    {
        if (!isOverload)
        {
            ObjectToCheck = Physics.OverlapSphere(transform.position, ToolData.UseRadius,TargetLayer);
        }
    }
    
    public virtual void Picked()
    {
        RB.isKinematic = true;
    }
    public virtual void UnPicked()
    {
        RB.isKinematic = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ToolData.UseRadius);
    }
}
