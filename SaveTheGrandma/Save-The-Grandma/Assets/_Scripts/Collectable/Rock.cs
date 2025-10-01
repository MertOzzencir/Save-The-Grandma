using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rock : Collectable
{
    void Start()
    {
        ToolType = _toolType.InventoryInformation.ToolCanGather;
    }
}
