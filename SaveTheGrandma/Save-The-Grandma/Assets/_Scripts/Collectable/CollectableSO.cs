using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Collectable", menuName = "New Collectable/Create Collectable")]
public class CollectableSO : ScriptableObject
{
    public InventoryInformation InventoryInformation;

}

public enum InventoryType
{
    Null,
    Rock,
    Wood,
    Iron,
    ChippedRock,
    Plank,
    Nail,
Building

}
[Serializable]
public class InventoryInformation
{
    public InventoryType InventoryType;
    public Sprite InventoryIcon;
    public ToolType ToolCanGather;
    public string InventoryName;
    public int FontSize;

}
