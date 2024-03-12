using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class TutorialNPC : TargetMover
{
    public GameObject player;
    public Image DialogBacground;
    public PointOfInterest[] pointsOfInterest; // Changed to PointOfInterest array
    public Text dialogueText;
    private bool[] doneExplanations;
    private int currentPoint = 0;
    private bool isExplaining = false;

    public InputAction continueButton;

    public static bool tutorialCompleted = false; // Static flag to mark tutorial completion

    void Start()
    {
        DialogBacground.enabled = false;
        // Check if the tutorial has already been completed
        if (tutorialCompleted)
        {
            gameObject.SetActive(false); // Optionally deactivate the NPC if tutorial is done
            return; // Exit to avoid reinitializing the tutorial
        }

        doneExplanations = new bool[pointsOfInterest.Length];
        SetTarget(pointsOfInterest[currentPoint].transform.position); // Access transform here
        continueButton.Enable();
        base.Start();
    }

    void OnEnable()
    {
        if (!tutorialCompleted) // Only enable if tutorial hasn't been completed
        {
            continueButton.Enable();
        }
    }

    void OnDisable()
    {
        continueButton.Disable();
    }

    void Update()
    {
        if (tutorialCompleted) return; // Skip update logic if tutorial is already completed

        for (int i = 0; i < pointsOfInterest.Length; i++)
        {
            if (!isExplaining && Vector3.Distance(transform.position, pointsOfInterest[i].transform.position) <= 5f && !doneExplanations[i]) // Access transform here
            {
                DialogBacground.enabled = true;
                StartCoroutine(ShowPlayerExplanation(i));
                Debug.Log("Showing explanation for point " + i);
                break; // Exit the loop to ensure only one explanation starts.
            }
        }
    }

    IEnumerator ShowPlayerExplanation(int pointIndex)
    {
        isExplaining = true;
        dialogueText.text = ""; // Clear previous text
        foreach (var explanation in pointsOfInterest[pointIndex].explanations) // Access explanations directly
        {
            dialogueText.text = explanation;
            Debug.Log(explanation);
            yield return new WaitUntil(() => continueButton.triggered);
            yield return new WaitForSeconds(0.2f);
        }

        dialogueText.text = ""; // Clear text after explanations
        isExplaining = false;
        doneExplanations[pointIndex] = true;
        DialogBacground.enabled = false;
        MoveToNextPoint();

        // Check if all explanations are done to mark the tutorial as completed
        tutorialCompleted = Array.TrueForAll(doneExplanations, done => done);
        if (tutorialCompleted)
        {
            Debug.Log("Tutorial completed!");
            // Additional actions upon completing the tutorial, if needed
        }
    }

    void MoveToNextPoint()
    {
        if (currentPoint < pointsOfInterest.Length - 1)
        {
            currentPoint++;
            SetTarget(pointsOfInterest[currentPoint].transform.position); // Access transform here
        }
    }
}
