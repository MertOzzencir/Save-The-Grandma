using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;

public class Axe : Tools
{

    private AnimationEventHandler _animEventHandler;
    private Animator _anim;
    void Awake()
    {
        _animEventHandler = GetComponentInChildren<AnimationEventHandler>();
        _anim = _animEventHandler.gameObject.GetComponent<Animator>();
        _animEventHandler.OnAnimEventFire += UseTool;
    }
    public override void StartUse()
    {
        if (_activeSource != null)
            return;
        base.StartUse();

        foreach (var a in ObjectToCheck)
        {
            if (a == null)
                continue;
            if (a.TryGetComponent(out MotherSource source))
            {
                _activeSource = source;
                _anim.SetBool("useTool", true);
                transform.position = _activeSource.DigPosition.position;
                transform.forward = _activeSource.DigPosition.forward;
                isOverload = false;
                return;
            }
        }
        isOverload = true;
    }
    private void UseTool()
    {
        if (_activeSource != null)
        {
            float timer = _activeSource._sourceSO.SpawnTimer;
            _activeSource.Dig(ToolData.AttackDamage, ToolData.TypeOfTool, out bool isSourceDead);
            if (isSourceDead)
            {
                _activeSource = null;
                _anim.SetBool("useTool", false);
                Invoke(nameof(Tampon), timer + 0.1f);
                isOverload = false;
            }
        }
    }
    private void Tampon()
    {
        isOverload = false;
        StartUse();
    }
    private void ToolCanceled(bool obj)
    {
        if (!obj)
        {
            _anim.SetBool("useTool", false);
            _activeSource = null;
            isOverload = false;
        }
    }



    void OnEnable()
    {
        InputManager.OnUse += ToolCanceled;
        _anim.SetFloat("animSpeed", ToolData.SpeedMultiplier);
    }
    void OnDisable()
    {
        InputManager.OnUse -= ToolCanceled;
        _activeSource = null;
        isOverload = false;
    }
}
