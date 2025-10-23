using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void UseTool(int index)
    {
        switch (index)
        {
            case 1:
                foreach (var a in _currentEnemy)
                {
                    a.Stunned();
                    StartCoroutine(AttackNextFrame(a));

                }
                _anim.SetBool("useTool", false);
                isOverload = false;
                _currentEnemy.Clear();
                break;
        }


    }

    IEnumerator AttackNextFrame(Enemy a)
    {
        yield return new WaitForFixedUpdate();
        Vector3 slapDir = (a.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(slapDir);
        transform.rotation = lookRotation;
        Rigidbody enemyRB = a.GetComponent<Rigidbody>();
        enemyRB.linearVelocity = Vector3.zero;
        enemyRB.AddForce(slapDir * _slapForce, ForceMode.Impulse);
        a.StunnedDirection = transform.position;
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
