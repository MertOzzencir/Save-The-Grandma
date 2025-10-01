using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craftable", menuName = "Craftable Item/New Craftable")]
public class CraftableSO : ScriptableObject
{
    public List<CraftingRecipe> _recipes;
    public Craftable Prefab;
    public Sprite Icon;
    public InventoryInformation InventoryInformation;
}
