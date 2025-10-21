using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Builder : Bench
{
    [SerializeField] private CinemachineCamera _builderCamera;

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
        Debug.Log("sa");
        base.OpenCraftMenu();
    }
    public override void Create()
    {
        if (CanCraft())
        {
            AllCraftables.Remove(ActiveCraftItem);
            ActiveCraftItem.gameObject.SetActive(true);
            if (AllCraftables.Count <= 0)
            {
                CloseEsc();
                Destroy(this);
            }
            else
            {
                SetCraftItem(0);
                
            }

        }

    }
}
