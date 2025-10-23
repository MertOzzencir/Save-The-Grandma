using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Builder : Bench
{
    public event Action OnComplete;
    [SerializeField] private CinemachineCamera _builderCamera;
    [SerializeField] private Material _completedMaterial;

    private InputManager _inputManager;
  
    public override void Start()
    {
        base.Start();
        

        _inputManager = FindAnyObjectByType<InputManager>();
    }
    public override void OpenCraftMenu()
    {
        if (!OpenMenu)
        {
            _builderCamera.Priority = 11;
            _inputManager.SetActiveInputManager(false);
        }
        else
        {
            _builderCamera.Priority = 9;
            _inputManager.SetActiveInputManager(true);
        }
        base.OpenCraftMenu();
    }
    public override void Create()
    {
        if (CanCreate())
        {
            AllCraftables.Remove(ActiveCraftItem);
            MeshRenderer[] AllMeshes = ActiveCraftItem.GetComponentsInChildren<MeshRenderer>();
            foreach (var a in AllMeshes)
            {
                Vector3 localScaleMesh = a.transform.localScale;
                a.transform.localScale = Vector3.zero;
                a.material = _completedMaterial;
                TweenManager.ScaleObject(a.transform, localScaleMesh, .35f,DG.Tweening.Ease.InElastic);
            }
            
            if (AllCraftables.Count <= 0)
            {
                CloseEsc();
                OnComplete?.Invoke();
                this.enabled = false;
            }
            else
            {
                SetCraftItem(AllCraftables[0]);
                
            }

        }

    }
}
