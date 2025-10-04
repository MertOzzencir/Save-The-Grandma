using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Collectable
{

    void Start()
    {
        ToolType = _toolType.InventoryInformation.ToolCanGather;
        float randomRotation = Random.Range(0f, 180f);
        transform.Rotate(0, randomRotation, 0);
    }
}
