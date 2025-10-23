using Unity.VisualScripting;
using UnityEngine;

public class Axe : Tools
{

    private MotherSource _activeSource;
    private AnimationEventHandler _animEventHandler;
    private Animator _anim;
    void Awake()
    {

        ToolAudioManager = GetComponent<EntityAudioManager>();
        _animEventHandler = GetComponentInChildren<AnimationEventHandler>();
        _anim = _animEventHandler.gameObject.GetComponent<Animator>();
        _animEventHandler.OnAnimEventFire += UseTool;
    }
    public override void StartUse()
    {
        _anim.SetBool("hardUseTool", false);
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
    private void UseTool(int index)
    {

        switch (index)
        {
            case 1:
                if (_activeSource != null)
                {
                    ToolAudioManager.SetRandomPitch();
                    ToolAudioManager.PlayEntityClip(0,3);
                    _activeSource.Dig(ToolData.AttackDamage, ToolData.TypeOfTool, out bool isSourceDead);
                    if (isSourceDead)
                    {
                        _activeSource = null;
                        _anim.SetBool("useTool", false);
                        _anim.SetBool("hardUseTool", false);
                        isOverload = true;
                    }
                }
                break;
            case 2:
                ToolAudioManager.PlayOneShotByIndex(3);
            break;
        }
    }
    private void ToolCanceled(bool obj)
    {
        if (!obj)
        {
            _anim.SetBool("useTool", false);
            _anim.SetBool("hardUseTool", true);
            _activeSource = null;
            isOverload = false;
            transform.up = Vector3.up;
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
