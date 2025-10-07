using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Tools
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _moveOffSet;
    private bool _canUpdateMovement;
    public override void StartUse()
    {
        _canUpdateMovement = true;
        base.StartUse();
        foreach (var a in ObjectToCheck)
        {
            Collectable source = a.transform.GetComponent<Collectable>();
            if (source != null)
            {
                source.Collect(ToolData.TypeOfTool);
            }
        }
    }
    void Update()
    {
        if (_canUpdateMovement != true)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point + _moveOffSet, .25f);
        }
    }
    public override void UnPicked()
    {
        base.UnPicked();
        _canUpdateMovement = false;
    }


}
