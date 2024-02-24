using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public InputAction moveRight = new InputAction(type: InputActionType.Button);
    public InputAction moveLeft = new InputAction(type: InputActionType.Button);
    public InputAction jump = new InputAction(type: InputActionType.Button);
    public float moveSpeed = 90f;

    void OnEnable()
    {
        // Enable the Input System actions
        moveRight.Enable();
        moveLeft.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        // Disable the Input System actions
        moveRight.Disable();
        moveLeft.Disable();
        jump.Disable();
    }

    void Start()
    {
        OnEnable();
        // Add listeners for when the input values change
        moveRight.performed += ctx => MovePlayer(new Vector3(1f, 0f, 0f));
        moveLeft.performed += ctx => MovePlayer(new Vector3(-1f, 0f, 0f));
        jump.performed += ctx => MovePlayer(new Vector3(0f, 3f, 0f));
    }

    void Update()
    {
        // You can remove the Update method if you're using InputSystem callbacks
    }

    void MovePlayer(Vector3 movement)
    {
        // Translate the player's position based on input and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
