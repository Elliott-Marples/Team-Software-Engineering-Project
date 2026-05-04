using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Distance and Height")]
    [SerializeField] private float distance = 6f;
    [SerializeField] private float heightOffset = 2f;

    [Header("Rotation")]
    [SerializeField] private bool allowRotation = true;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float pitch = 20f;
    [SerializeField] private float minPitch = -10f;
    [SerializeField] private float maxPitch = 50f;
    [SerializeField] private float yaw = 0f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference lookAction;

    [Header("Follow")]
    [SerializeField] private bool smoothFollow = true;
    [SerializeField] private float followSpeed = 10f;

    [SerializeField] private bool lockCursorOnStart = true;
    [SerializeField] private bool hideCursorWhenLocked = true;
    [SerializeField] private bool allowEscapeToUnlock = true;

    private bool cursorLocked;

    private void OnEnable()
    {
        if (lookAction != null)
        {
            lookAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (lookAction != null)
        {
            lookAction.action.Disable();
        }
    }

    private void Start()
    {
        if (lockCursorOnStart)
        {
            SetCursorState(true);
        }
    }

    private void LateUpdate()
    {
        HandleCursorInput();

        if (target == null)
            return;

        HandleRotation();
        UpdateCameraPosition();
    }

    private void HandleCursorInput()
    {
        if (allowEscapeToUnlock && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SetCursorState(false);
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !cursorLocked)
        {
            SetCursorState(true);
        }
    }

    private void HandleRotation()
    {
        if (!allowRotation || lookAction == null || !cursorLocked)
            return;

        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        yaw += lookInput.x * rotationSpeed * Time.deltaTime;
        pitch -= lookInput.y * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void UpdateCameraPosition()
    {
        Vector3 focusPoint = target.position + Vector3.up * heightOffset;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
        Vector3 desiredPosition = focusPoint + offset;

        if (smoothFollow)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = desiredPosition;
        }

        transform.LookAt(focusPoint);
    }

    private void SetCursorState(bool locked)
    {
        cursorLocked = locked;

        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = locked ? !hideCursorWhenLocked : true;
    }
}