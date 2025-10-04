

using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action<InventoryInformation> OnItemCollected;
    public CollectableSO _toolType;
    public ToolType ToolType { get; set; }
   
    public virtual void Collect(ToolType toolType)
    {
        if (toolType == ToolType)
        {
            SendItemToInventory(_toolType.InventoryInformation);
            Destroy(gameObject);
        }
    }

    public void SendItemToInventory(InventoryInformation a)
    {
        OnItemCollected?.Invoke(a);
    }

}
