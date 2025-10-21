using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Zoom (Y ekseni yukarı-aşağı)")]
    [SerializeField] private float zoomRange = 30f;       // başlangıç Y'den ± mesafe
    [SerializeField] private float zoomStep = 2f;         // scroll başına kaç birim
    [SerializeField] private float zoomSmoothTime = 0.1f; // yumuşatma

    [Header("Movement")]
    [SerializeField] private float moveMultiplier = 10f;
    [SerializeField] private float moveSmoothTime = 0.08f;

    private InputManager _input;
    private Vector2 _moveInput;

    private float baseY;        // başlangıç yüksekliği
    private float targetY;      // hedef Y
    private float yVelocity;    // SmoothDamp için

    private Vector3 _moveVelocity;

    void Start()
    {
        _input = FindAnyObjectByType<InputManager>();
        baseY = transform.position.y;
        targetY = baseY;
    }

    void Update()
    {
        // hareket inputu
        _moveInput = -_input.Move();

        // scroll inputu
        float scroll = _input.MouseScroolValue().y;
        if (Mathf.Abs(scroll) > 0.001f)
        {
            targetY -= scroll * zoomStep;
            targetY = Mathf.Clamp(targetY, baseY - zoomRange, baseY + zoomRange);
        }
    }

    void LateUpdate()
    {
        // YATAY hareket (X/Z düzleminde)
        Vector3 right = transform.right;
        Vector3 forward = transform.forward;
        right.y = 0;
        forward.y = 0;
        right.Normalize();
        forward.Normalize();

        Vector3 moveTarget = transform.position + (right * -_moveInput.x + forward * -_moveInput.y) * moveMultiplier;
        transform.position = Vector3.SmoothDamp(transform.position, moveTarget, ref _moveVelocity, moveSmoothTime);

        // YUKARI-AŞAĞI hareket (zoom)
        Vector3 pos = transform.position;
        pos.y = Mathf.SmoothDamp(pos.y, targetY, ref yVelocity, zoomSmoothTime);
        transform.position = pos;
    }
}
