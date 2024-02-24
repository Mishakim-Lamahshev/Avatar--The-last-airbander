using UnityEngine;
using UnityEngine.InputSystem;

public class TrainPlayerInputHandler : MonoBehaviour
{
    public InputAction Attack = new InputAction(type: InputActionType.Button);

    private AttackController attackController;

    void OnEnable()
    {
        Attack.Enable();
    }

    void OnDisable()
    {
        Attack.Disable();
    }

    void Start()
    {
        OnEnable();

        // Find the AttackController in the scene
        attackController = FindObjectOfType<AttackController>();

        Attack.performed += ctx =>
        {
            var key = ctx.control.path; // Get the path of the control, e.g., "<Keyboard>/f"
            char attackKey = GetKeyFromControlPath(key);
            attackController.CommitAttack(attackKey);
        };
    }

    private char GetKeyFromControlPath(string controlPath)
    {
        // Extract the last character of the control path and convert it to lowercase
        // This assumes the key is always the last character in the path
        return controlPath[^1]; // Using the ^ operator to get the last character
    }
}
