using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tools", menuName = "Create Tool/New Tool")]
public class ToolsSO : ScriptableObject
{
    public ToolType TypeOfTool;
    public float SpeedMultiplier;
    public int AttackDamage;
    public float UseRadius;
    public Tools Prefab;


}

public enum ToolType
{
    Hand,
    Axe,
    Pickaxe,
    Collector,
    Gun
}