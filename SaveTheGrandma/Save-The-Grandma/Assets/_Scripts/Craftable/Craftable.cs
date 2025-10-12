using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : Collectable
{
    public CraftableSO SOData;
    public override void Start()
    {
        ToolType = SOData.InventoryInformation.ToolCanGather;
        SetIdleAnimation();
    }
    public override void Collect(ToolType toolType)
    {
        if (toolType == SOData.InventoryInformation.ToolCanGather && !Collected)
        {
            Collected = true;
            InventoryManager.Instance.CollectItem(SOData.InventoryInformation);
            StartCoroutine(CollectAnimation()); 
            Destroy(gameObject,1f);
        }
    }
    public override void Update()
    {
        base.Update();
    }
}
[Serializable]
public class CraftingRecipe
{
    public Collectable Collectable;
    public int Amount;
}
