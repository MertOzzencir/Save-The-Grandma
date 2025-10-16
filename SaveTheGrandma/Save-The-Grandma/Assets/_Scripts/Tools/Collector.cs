using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Tools
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _moveOffSet;
    private bool _canUpdateMovement;
    private Animator _anim;
    private bool _canAnim;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }
    public override void StartUse()
    {
        _canAnim = false;
        _canUpdateMovement = true;
        base.StartUse();
        Debug.Log("Trying to Collect");
        foreach (var a in ObjectToCheck)
        {
            Collectable source = a.transform.GetComponent<Collectable>();
            if (source != null)
            {
                source.Collect(ToolData.TypeOfTool);
                _canAnim = true;
            }
        }
        if (_canAnim)
            _anim.SetBool("canCollect", true);
        else
            _anim.SetBool("canCollect", false);

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
    private void ToolCanceled(bool obj)
    {
        if (!obj)
        {
            _anim.SetBool("canCollect", false);
            transform.up = Vector3.up;
        }
    }
    void OnEnable()
    {
        InputManager.OnUse += ToolCanceled;
    }


    void OnDisable()
    {
        InputManager.OnUse -= ToolCanceled;
    }


}
