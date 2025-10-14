using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static event Action OnLeftMouse;
    public static event Action<bool> OnUse;
    public static event Action<int> OnToolPick;
    public static event Action OnTab;
    public static event Action OnClose;
    private InputActions _inputActions;

    void Awake()
    {
        _inputActions = new InputActions();
    }
    private void LeftMouse(InputAction.CallbackContext context)
    {
        OnLeftMouse?.Invoke();
    }




    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.LeftMouseButton.performed += LeftMouse;
        _inputActions.Player.Use.started += OnUseStarted;
        _inputActions.Player.Use.canceled += OnUseCanceled;
        _inputActions.Player.ToolPick.performed += ctx =>
        {
            var key = ctx.control.name;
            switch (key)
            {
                case "1":
                    SelectTool(1);
                    break;
                case "2":
                    SelectTool(2);
                    break;
                case "3":
                    SelectTool(3);
                    break;
                case "4":
                    SelectTool(4);
                    break;

            }
        };
        _inputActions.Player.Inventory.performed += OnInventoryButton;
        _inputActions.Player.CloseTabs.performed += OnCloseTabs;
    }
    public Vector2 Move()
    {
        return _inputActions.Player.WASD.ReadValue<Vector2>();
    }
    public Vector2 MouseScroolValue()
    {
        return Mouse.current.scroll.ReadValue();
    }

    private void OnCloseTabs(InputAction.CallbackContext context)
    {
        OnClose?.Invoke();
    }

    private void OnInventoryButton(InputAction.CallbackContext context)
    {
        OnTab?.Invoke();
    }

    private void SelectTool(int v)
    {
        OnToolPick?.Invoke(v);
    }

    private void OnUseStarted(InputAction.CallbackContext context)
    {
        OnUse?.Invoke(true);
    }
    private void OnUseCanceled(InputAction.CallbackContext context)
    {
        OnUse?.Invoke(false);
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

}
