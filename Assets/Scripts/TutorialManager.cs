using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Required for the new Input System
using TMPro; // Include the TextMeshPro namespace
using System.Collections.Generic; // Required for using List

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<ExplanationSet> explanationSets; // List of explanation sets

    [SerializeField]
    private TextMeshPro displayText;

    [SerializeField]
    private InputAction inputAction; // Input action for moving to the next explanation

    private string[] currentExplanations; // Current explanations array
    private int currentExplanationIndex = 0; // Tracks the current explanation index

    public static bool IsTutorialComplete { get; private set; } // Static flag to indicate tutorial completion

    // Level or element name to set the current explanations
    [SerializeField]
    public string elementOrLevelName;

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
        SetCurrentElement(elementOrLevelName);
    }

    public void SetCurrentElement(string elementOrLevelName)
    {
        string levelToShow = StatsHandle.GetCurrentStat(elementOrLevelName).ToString();
        ExplanationSet set = explanationSets.Find(s => s.setName == levelToShow);
        if (set != null)
        {
            currentExplanations = set.explanations;
            currentExplanationIndex = 0; // Reset index
            IsTutorialComplete = false; // Reset completion flag
            DisplayCurrentExplanation();
        }
        else
        {
            Debug.LogWarning("Explanation set not found for element or level: " + elementOrLevelName);
        }
    }

    private void OnInputActionPerformed(InputAction.CallbackContext context)
    {
        MoveToNextExplanation();
    }

    private void DisplayCurrentExplanation()
    {
        if (currentExplanationIndex < currentExplanations.Length)
        {
            displayText.text = currentExplanations[currentExplanationIndex];
        }
    }

    private void MoveToNextExplanation()
    {
        if (currentExplanationIndex < currentExplanations.Length - 1)
        {
            currentExplanationIndex++;
            DisplayCurrentExplanation();
        }
        else
        {
            IsTutorialComplete = true;
            displayText.enabled = false; // Hide the display text
        }
    }
}
