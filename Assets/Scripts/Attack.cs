using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public InputAction AirAttack = new InputAction(type: InputActionType.Button);
    public InputAction DirtAttack = new InputAction(type: InputActionType.Button);
    public InputAction FireAttack = new InputAction(type: InputActionType.Button);
    public InputAction WaterAttack = new InputAction(type: InputActionType.Button);

    private AttackController attackController;

    void OnEnable()
    {
        AirAttack.Enable();
        DirtAttack.Enable();
        FireAttack.Enable();
        WaterAttack.Enable();
    }

    void OnDisable()
    {
        AirAttack.Disable();
        DirtAttack.Disable();
        FireAttack.Disable();
        WaterAttack.Disable();
    }

    void Start()
    {
        OnEnable();

        // Find the AttackController in the scene
        attackController = FindObjectOfType<AttackController>();

        AirAttack.performed += ctx => attackController.CommitAttack('a');
        WaterAttack.performed += ctx => attackController.CommitAttack('w');
        FireAttack.performed += ctx => attackController.CommitAttack('f');
        DirtAttack.performed += ctx => attackController.CommitAttack('d');
    }
}
