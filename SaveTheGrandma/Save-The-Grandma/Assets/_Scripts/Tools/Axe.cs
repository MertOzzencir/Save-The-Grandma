using Unity.VisualScripting;
using UnityEngine;

public class Axe : Tools
{

    private MotherSource _activeSource;
    private AnimationEventHandler _animEventHandler;
    private Animator _anim;
    private bool _isCanceled;
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
        Debug.Log(index);

        switch (index)
        {
            case 1:
                if (_activeSource != null)
                {
                    ToolAudioManager.SetRandomPitch();
                    ToolAudioManager.PlayEntityClip(0,3);
                    float timer = _activeSource.SpawnOwner.SpawnTimer;
                    _activeSource.Dig(ToolData.AttackDamage, ToolData.TypeOfTool, out bool isSourceDead);
                    if (isSourceDead)
                    {
                        _activeSource = null;
                        _anim.SetBool("useTool", false);
                        _anim.SetBool("hardUseTool", false);
                        Invoke(nameof(Tampon), timer + 0.1f);
                        isOverload = false;
                    }
                }
                break;
            case 2:
                ToolAudioManager.PlayOneShotByIndex(3);
            break;
        }

    }
    private void Tampon()
    {
        isOverload = false;
        if (!_isCanceled)
        {
            Debug.Log("sa?");

            StartUse();
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
            _isCanceled = true;
            transform.up = Vector3.up;
        }
        else
            _isCanceled = false;
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
