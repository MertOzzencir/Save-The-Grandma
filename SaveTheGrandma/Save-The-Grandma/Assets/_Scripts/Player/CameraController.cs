using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Zooming")]
    [SerializeField] private Vector2 _zoomLimits;
    [SerializeField] private float _zoomStep;
    [SerializeField] private float _scroolSmoothTime;
    
    [Header("Movement")]
    [SerializeField] private float _moveMultiplier;
    [SerializeField] private float _moveSmoothTime;
    private InputManager _input;
    private Vector2 _moveInput;
    private float _zoom;
    private float _zoomVelocity;
    private Vector3 _moveVelocity;

    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        _zoom = cam.orthographicSize;
        _input = FindObjectOfType<InputManager>();
    }
    void Update()
    {
        _moveInput = -_input.Move();
        float y = _input.MouseScroolValue().y;
        if (y > 0)
            y = 1;
        else if (y < 0)
            y = -1;
        else
            y = 0;

        _zoom -= y * _zoomStep;
        _zoom = Mathf.Clamp(_zoom, _zoomLimits.x, _zoomLimits.y);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, _zoom, ref _zoomVelocity, _scroolSmoothTime);
        
        
    }
    void LateUpdate()
    {
        Vector3 right = transform.right;
        Vector3 up = transform.forward;
        right.y = 0;
        up.y = 0;
        right.Normalize();
        up.Normalize();
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + (right * -_moveInput.x + up* - _moveInput.y)*_moveMultiplier,ref _moveVelocity,_moveSmoothTime);
    }
}
