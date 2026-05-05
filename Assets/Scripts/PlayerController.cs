using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool shouldFaceMoveDirection;

    private bool isInFanArea = false;
    private Vector3 fanForce;
    [SerializeField] float fanDuration;

    private CharacterController characterController;
    private PlayerInput playerInput;

    private int playerNum;
    private Vector2 moveInput;
    private Vector3 velocity;

    private CinemachineCamera cmCamera;
    private CinemachineBrain cmBrain;
    private CinemachineInputAxisController cmInput;

    private RaceManager raceManager;
    public Checkpoint currentCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        raceManager = GameObject.FindGameObjectWithTag("RaceManager").GetComponent<RaceManager>();
        shouldFaceMoveDirection = true;

        cmCamera = GetComponentInChildren<CinemachineCamera>();
        cmBrain = GetComponentInChildren<CinemachineBrain>();
        cmInput = GetComponentInChildren<CinemachineInputAxisController>();
        playerNum = playerInput.playerIndex + 1;

        OutputChannels channel = (OutputChannels)(Math.Pow(2, playerNum));

        cmCamera.OutputChannel = channel;
        cmBrain.ChannelMask = channel;
        cmInput.PlayerIndex = playerNum - 1;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //Debug.Log($"Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jumping {context.performed} - Is Grounded: {characterController.isGrounded}");
        if(context.performed && characterController.isGrounded)
        {
            Debug.Log("Supposed to Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnStartRace(InputAction.CallbackContext context)
    {
        raceManager.StartRace(playerNum);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
           ResetPlayer(hit.gameObject);
    }

    public void ResetPlayer(GameObject other)
    {
        //Debug.Log($"Collided with: {hit.gameObject.tag}");
        if (other.CompareTag("ResetOnCollision"))
        {
            Debug.Log("Touched a NoNo");
            transform.position = currentCheckpoint.transform.position;
            fanForce = Vector3.zero;
        }
    }

    public void EnterFanArea(Vector3 force)
    {
        isInFanArea = true;
        fanForce = force;
    }

    public void ExitFanArea()
    {
        isInFanArea = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        if (!isInFanArea && fanForce != Vector3.zero)
        {
            fanForce = Vector3.Lerp(fanForce, Vector3.zero, fanDuration * Time.deltaTime);
        }

        moveDirection += fanForce;

        characterController.Move(moveDirection * speed * Time.deltaTime);

        if(shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
