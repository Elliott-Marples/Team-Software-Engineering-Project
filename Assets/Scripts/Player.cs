using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Vector3 movement;

    public float movementSpeed = 10.0f;

    // Applies movement force to player
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(movement * movementSpeed, ForceMode.Impulse);
    }

    // Reads the value of the current input
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector2 = context.ReadValue<Vector2>();
        movement = new Vector3(movementVector2.x, 0, movementVector2.y);
    }
}
