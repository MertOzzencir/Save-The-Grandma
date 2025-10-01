using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ToolUseManager : MonoBehaviour
{
    [SerializeField] private LayerMask _toolLayerMask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _moveOffSet;

    private Tools _pickedObject = null;
    private bool _canUse;
    private Tools[] _useableTools;
    void Start()
    {
        InputManager.OnUse += UseTool;
        ToolPickManager.OnToolPicked += PickTool;
        _useableTools = FindObjectsByType<Tools>(
        FindObjectsInactive.Include,
        FindObjectsSortMode.None
        );

        Debug.Log(_useableTools[1]);
    }

    private void UseTool(bool canUse)
    {
        _canUse = canUse;

    }

    void Update()
    {
        if (_pickedObject == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
        {
            _pickedObject.transform.position = Vector3.Lerp(_pickedObject.transform.position, hit.point + _moveOffSet, .25f);
        }
        if (_canUse)
            _pickedObject.Use();

    }

    private void PickTool(ToolsSO toolSO)
    {
        if (_pickedObject == null)
        {
            CreateNewTool(toolSO);
        }
        else if (_pickedObject?.ToolData == toolSO)
        {
            DestroyTool();
        }
        else
        {
            DestroyTool();
            CreateNewTool(toolSO);
        }
    }

    private void DestroyTool()
    {
        _pickedObject.UnPicked();
        _pickedObject.gameObject.SetActive(false);
        _pickedObject = null;
    }

    private void CreateNewTool(ToolsSO toolSO)
    {
        _pickedObject = _useableTools.FirstOrDefault(value => value.ToolData.TypeOfTool == toolSO.TypeOfTool);
        _pickedObject.gameObject.SetActive(true);
        _pickedObject.transform.position = GetMousePosition();
        _pickedObject.Picked();
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
        {
            return hit.point;
        }
        else
            return Vector3.zero;
    }
}
