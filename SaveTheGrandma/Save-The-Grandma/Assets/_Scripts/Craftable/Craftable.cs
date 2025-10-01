using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : Collectable
{
    [SerializeField] private CraftableSO _reciept;
     void Start()
    {
        ToolType = _reciept.InventoryInformation.ToolCanGather;
    }
    public override void Collect(ToolType toolType)
    {
        if (toolType == _reciept.InventoryInformation.ToolCanGather)
        {
            SendItemToInventory(_reciept.InventoryInformation);
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
