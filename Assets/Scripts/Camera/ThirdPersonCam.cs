using System;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float sensitivity = 0.015f;
    [SerializeField] private float controllerSensitivity = 0.05f;

    private PlayerInput playerInput;
    private PlayerInputHandler controls;
    private bool isKeyboard;
    InputDevice cameraDevice;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbitalFollow;
    private Vector2 scrollDelta;
    private Vector2 mouseDelta;

    [SerializeField] private float targetZoom;
    [SerializeField] private float currentZoom;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();

        controls = new PlayerInputHandler();
        controls.Enable();
        controls.CameraControls.MouseZoom.performed += HandleMouseScroll;
        controls.CameraControls.Look.performed += HandleMouseMovement;

        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponent<CinemachineCamera>();
        orbitalFollow = cam.GetComponent<CinemachineOrbitalFollow>();

        targetZoom = currentZoom = orbitalFollow.Radius;

        isKeyboard = false;
        cameraDevice = playerInput.devices[0];

        for (int i = 0; i < playerInput.devices.Count; i++)
        {
            if (playerInput.devices[i] is Keyboard)
            {
                isKeyboard = true;
                break;
            }
        }
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<Vector2>();
        Debug.Log($"Mouse is Scrolling. Value: {scrollDelta}");
    }

    private void HandleMouseMovement(InputAction.CallbackContext context)
    {
        // Device sending input
        InputDevice inputDevice = context.control.device;

        // Device checks
        bool isCorrectDevice = cameraDevice == inputDevice;
        bool isKeyboardAndMouse = isKeyboard && inputDevice is Mouse;

        // Ensures camera device is same as input device
        if (isCorrectDevice || isKeyboardAndMouse)
        {
            Vector2 delta = context.ReadValue<Vector2>();

            if (inputDevice is Mouse)
            {
                delta *= sensitivity;
            }
            else
            {
                delta *= controllerSensitivity;
            }

                orbitalFollow.HorizontalAxis.Value += delta.x;
            orbitalFollow.VerticalAxis.Value -= delta.y;
        }
    }

    private void Update()
    {
        if (scrollDelta.y != 0)
        {
            if (orbitalFollow != null)
            {
                targetZoom = Mathf.Clamp(orbitalFollow.Radius - scrollDelta.y * zoomSpeed, minDistance, maxDistance);
                scrollDelta = Vector2.zero;
            }
        }

        float bumperDelta = controls.CameraControls.GamepadZoom.ReadValue<float>();
        if (bumperDelta != 0)
        {
            targetZoom = Mathf.Clamp(orbitalFollow.Radius - bumperDelta * zoomSpeed, minDistance, maxDistance);
        }

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbitalFollow.Radius = currentZoom;
    }
}