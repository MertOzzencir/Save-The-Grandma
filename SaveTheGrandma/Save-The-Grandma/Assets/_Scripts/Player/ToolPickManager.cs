using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolPickManager : MonoBehaviour
{
    public static event Action<ToolsSO> OnToolPicked;
    [SerializeField] private ToolsSO[] _tools;

    private ToolsSO _toolPicked = null;
    private void Start()
    {
        InputManager.OnToolPick += PickTool;
    }

    public void PickTool(int obj)
    {
        switch (obj)
        {
            case 1:
                _toolPicked = PickToolFromArray(ToolType.Gun);
                break;
            case 2:
                _toolPicked = PickToolFromArray(ToolType.Collector);
                break;
            case 3:
                _toolPicked = PickToolFromArray(ToolType.Axe);
                break;
            case 4:
                _toolPicked = PickToolFromArray(ToolType.Pickaxe);
                break;
        }
        OnToolPicked?.Invoke(_toolPicked);
    }

    private ToolsSO PickToolFromArray(ToolType tool)
    {
        return Array.Find(_tools, t => t.TypeOfTool == tool);
    }
}
