using System;
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

    private PlayerInput controls;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbitalFollow;
    private Vector2 scrollDelta;

    private float targetZoom;
    private float currentZoom;

    private void Start()
    {
        controls = new PlayerInput();
        controls.Enable();
        controls.CameraControls.MouseZoom.performed += HandleMouseScroll;

        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponent<CinemachineCamera>();
        orbitalFollow = cam.GetComponent<CinemachineOrbitalFollow>();

        targetZoom = currentZoom = orbitalFollow.Radius;
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<Vector2>();
        Debug.Log($"Mouse is Scrolling. Value: {scrollDelta}");
    }

    private void Update()
    {
        if (scrollDelta.y != 0)
        {
            if(orbitalFollow != null)
            {
                targetZoom = Mathf.Clamp(orbitalFollow.Radius - scrollDelta.y * zoomSpeed, minDistance, maxDistance);
                scrollDelta = Vector2.zero;
            }
        }

        float bumperDelta = controls.CameraControls.GamepadZoom.ReadValue<float>();
        if(bumperDelta != 0)
        {
            targetZoom = Mathf.Clamp(orbitalFollow.Radius - bumperDelta * zoomSpeed, minDistance, maxDistance);
        }

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbitalFollow.Radius = currentZoom;
    }
}
