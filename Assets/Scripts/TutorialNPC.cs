using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class TutorialNPC : TargetMover
{
    public GameObject player;
    public PointOfInterest[] pointsOfInterest; // Changed to PointOfInterest array
    public Text dialogueText;
    private bool[] doneExplanations;
    private int currentPoint = 0;
    private bool isExplaining = false;

    public InputAction continueButton;

    void Start()
    {
        doneExplanations = new bool[pointsOfInterest.Length];
        SetTarget(pointsOfInterest[currentPoint].transform.position); // Access transform here
        continueButton.Enable();
        base.Start();
    }

    void OnEnable()
    {
        continueButton.Enable();
    }

    void OnDisable()
    {
        continueButton.Disable();
    }

    void Update()
    {
        for (int i = 0; i < pointsOfInterest.Length; i++)
        {
            if (!isExplaining && Vector3.Distance(transform.position, pointsOfInterest[i].transform.position) <= 5f && !doneExplanations[i]) // Access transform here
            {
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
        MoveToNextPoint();
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
