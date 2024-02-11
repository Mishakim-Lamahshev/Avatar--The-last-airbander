using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialNPC : TargetMover
{
    public GameObject player;
    public Transform[] pointsOfInterest;
    public string[][] explanations; // An array of string arrays to hold different explanations for each point
    public Text dialogueText;
    private bool[] doneExplanations;
    private int currentPoint = 0;
    private bool isExplaining = false;

    public string[] PlayerExplanations;
    public string[] ArenaExplanations;
    public string[] TrainingCenterExplanations;

    void Start()
    {
        doneExplanations = new bool[pointsOfInterest.Length];
        explanations = new string[][] { PlayerExplanations, ArenaExplanations, TrainingCenterExplanations };
        SetTarget(pointsOfInterest[currentPoint].position);
        base.Start();
    }

    void Update()
    {
        for (int i = 0; i < pointsOfInterest.Length; i++)
        {
            if (!isExplaining && Vector3.Distance(transform.position, pointsOfInterest[i].position) <= 8f && !doneExplanations[i])
            {
                StartCoroutine(ShowPlayerExplanation(i));
                break; // Exit the loop to ensure only one explanation starts.
            }
        }
    }

    IEnumerator ShowPlayerExplanation(int pointIndex)
    {
        isExplaining = true;
        dialogueText.text = ""; // Clear previous text
        foreach (var explanation in explanations[pointIndex])
        {
            dialogueText.text = explanation;
            Debug.Log(explanation);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
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
            SetTarget(pointsOfInterest[currentPoint].position);
        }
    }
}
