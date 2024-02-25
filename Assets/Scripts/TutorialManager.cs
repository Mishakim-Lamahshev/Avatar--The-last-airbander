using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Required for the new Input System
using TMPro; // Include the TextMeshPro namespace

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private string[] explanations; // Array of explanations to display
    [SerializeField]
    private TextMeshPro displayText; // Change to TextMeshProUGUI for UI text
    [SerializeField]
    private InputAction inputAction; // Input action for moving to the next explanation

    private int currentExplanationIndex = 0; // Tracks the current explanation index

    public static bool IsTutorialComplete { get; private set; } // Static flag to indicate tutorial completion

    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.performed += OnInputActionPerformed;
    }

    private void OnDisable()
    {
        inputAction.Disable();
        inputAction.performed -= OnInputActionPerformed;
    }

    private void Start()
    {
        if (explanations.Length > 0)
        {
            IsTutorialComplete = false; // Reset tutorial completion state
            DisplayCurrentExplanation();
        }
    }

    private void OnInputActionPerformed(InputAction.CallbackContext context)
    {
        MoveToNextExplanation();
    }

    private void DisplayCurrentExplanation()
    {
        if (currentExplanationIndex < explanations.Length)
        {
            displayText.text = explanations[currentExplanationIndex];
        }
    }

    private void MoveToNextExplanation()
    {
        if (currentExplanationIndex < explanations.Length - 1)
        {
            currentExplanationIndex++;
            DisplayCurrentExplanation();
        }
        else
        {
            IsTutorialComplete = true;
            displayText.enabled = false;
        }
    }
}
