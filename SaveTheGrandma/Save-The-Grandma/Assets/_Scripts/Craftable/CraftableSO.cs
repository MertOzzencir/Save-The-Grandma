using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craftable", menuName = "Craftable Item/New Craftable")]
public class CraftableSO : CollectableSO
{
    public List<CraftingRecipe> _recipes;
    public Craftable Prefab;
}
