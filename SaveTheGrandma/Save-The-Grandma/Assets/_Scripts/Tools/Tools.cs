
using UnityEngine;

public abstract class Tools : MonoBehaviour
{
    public EntityAudioManager ToolAudioManager{ get; set;}
    public ToolsSO ToolData;
    public LayerMask TargetLayer;
    public Rigidbody RB;
    public Collider[] ObjectToCheck { get; set; }

    public bool isOverload { get; set; }
    private ToolUI _toolUI;
    void Awake()
    {
        
    }
    public virtual void StartUse()
    {
        if (!isOverload)
        {
            ObjectToCheck = Physics.OverlapSphere(transform.position, ToolData.UseRadius,TargetLayer);
        }
    }
    
    public virtual void Picked()
    {
        _toolUI = FindAnyObjectByType<ToolUI>();
        _toolUI.HighLightToolBar(ToolData);
        RB.isKinematic = true;
    }
    public virtual void UnPicked()
    {
        _toolUI = FindAnyObjectByType<ToolUI>();
        _toolUI.HighLightToolBar(ToolData);
        RB.isKinematic = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ToolData.UseRadius);
    }
}
