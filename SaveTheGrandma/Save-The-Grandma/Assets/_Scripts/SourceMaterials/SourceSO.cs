using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Source", menuName = "Source/New Source")]
public class SourceSO : ScriptableObject
{
    public ToolType AToolCanDig;
    public int MaterialHeath;
    public Collectable SourceDropMaterial;

}
