using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Tools
{
    private Animator _anim;
    private AnimationEventHandler _animEventHandler;
    private List<Enemy> _currentEnemy;
    private float _slapForce;
    private void Awake()
    {
        _slapForce = ToolData.AttackDamage;
        _currentEnemy = new List<Enemy>();
        _animEventHandler = GetComponentInChildren<AnimationEventHandler>();
        _anim = _animEventHandler.gameObject.GetComponent<Animator>();
        _animEventHandler.OnAnimEventFire += UseTool;
    }

    private void UseTool()
    {

        foreach (var a in _currentEnemy)
        {
            Vector3 slapDir = (a.transform.position - transform.position).normalized;
            Rigidbody enemyRB = a.GetComponent<Rigidbody>();
            enemyRB.velocity = Vector3.zero;
            enemyRB.AddForce(slapDir * _slapForce, ForceMode.Impulse);
            a.StunnedDirection = transform.position;
            a.Stunned();
            
        }
        Debug.Log("as kardeş");
        _anim.SetBool("useTool", false);
        isOverload = false;
        _currentEnemy.Clear();

    }

    public override void StartUse()
    {
        if (_currentEnemy.Count > 0)
            return;
        base.StartUse();
        foreach (var a in ObjectToCheck)
        {
            if (a.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _currentEnemy.Add(enemy);
                Debug.Log("sa again");
                _anim.SetBool("useTool", true);
                isOverload = false;
            }
        }
        isOverload = true;
    }

    private void ToolCanceled(bool obj)
    {
        if (!obj)
        {
            isOverload = false;
            _anim.SetBool("useTool", false);
            _currentEnemy.Clear();
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
