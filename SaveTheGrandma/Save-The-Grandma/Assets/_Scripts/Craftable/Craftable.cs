using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : Collectable
{
    public CraftableSO SOData;
    
    public override void Collect(ToolType toolType)
    {
        if (toolType == SOData.InventoryInformation.ToolCanGather)
        {
            SendItemToInventory(SOData.InventoryInformation);
            Destroy(gameObject);
        }
    }
}
[Serializable]
public class CraftingRecipe
{
    public Collectable Collectable;
    public int Amount;
}
