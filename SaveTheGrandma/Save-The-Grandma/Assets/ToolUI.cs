using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ToolUI : MonoBehaviour
{
    [SerializeField] private List<ToolUISpecs> _toolUIS;
    [SerializeField] private UnityEngine.Color _highLightedColor;


    private ToolUISpecs _pickedUIToolElement;
    void Start()
    {
        ToolPickManager.OnToolPicked += HighLightToolBar;
    }

    private void HighLightToolBar(ToolsSO sO)
    {
        if (_pickedUIToolElement?.UITool == sO.TypeOfTool)
        {
            _pickedUIToolElement._toolImage.GetComponent<Image>().color = Color.black;
            _pickedUIToolElement = null;
            return;
        }
        if (_pickedUIToolElement != null)
        {
            if (_pickedUIToolElement.UITool != sO.TypeOfTool)
            {
                _pickedUIToolElement._toolImage.GetComponent<Image>().color = Color.black;
                _pickedUIToolElement = null;
            }
        }

        _pickedUIToolElement = _toolUIS.FirstOrDefault(value => value.UITool == sO.TypeOfTool);
        if (_pickedUIToolElement != null)
            _pickedUIToolElement._toolImage.GetComponent<Image>().color = _highLightedColor;
    }
}

[Serializable]
public class ToolUISpecs
{
    public GameObject _toolImage;
    public ToolType UITool;

}
