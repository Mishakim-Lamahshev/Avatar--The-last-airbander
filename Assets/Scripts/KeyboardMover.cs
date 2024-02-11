using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMover : MonoBehaviour
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private float speed = 5.0f; // Speed of movement

    void OnValidate()
    {
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Value, binding: "2DVector");
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                       .With("Up", "<Keyboard>/w")
                       .With("Down", "<Keyboard>/s")
                       .With("Left", "<Keyboard>/a")
                       .With("Right", "<Keyboard>/d");
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    protected Vector3 NewPosition()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime;
        return transform.position + movement;
    }
}
