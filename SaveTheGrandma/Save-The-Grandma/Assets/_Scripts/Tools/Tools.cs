
using UnityEngine;

public abstract class Tools : MonoBehaviour
{
    public ToolsSO ToolData;
    public Rigidbody RB;
    public float _lastTimeUsed { get; set; }
    public Collider[] ObjectToCheck { get; set; }
    public virtual void Use()
    {
        ObjectToCheck = Physics.OverlapSphere(transform.position, ToolData.UseRadius);
    }
    public bool CooldownTimer()
    {
        if (Time.time < ToolData.UseCoolDown + _lastTimeUsed)
            return false;
        else
            return true;
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
