using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Button and Text
using UnityEngine.SceneManagement;

public class TriggerButtonPopUp : MonoBehaviour
{
    public Button button1; // Assign in the inspector
    public Text txt;
    public string sceneToLoad; // Assign in the inspector

    [SerializeField] private string textToDisplay;

    private void Start()
    {
        // Hide buttons on start
        button1.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the tutorial is completed before showing the button and text
        if (TutorialNPC.tutorialCompleted)
        {
            txt.text = textToDisplay;
            button1.gameObject.SetActive(true);
            // Remove all listeners to ensure no duplicates, then add the new listener
            button1.onClick.RemoveAllListeners();
            button1.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Optionally, add an OnTriggerExit2D method to hide the buttons when the objects stop intersecting
    private void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            if (TutorialNPC.tutorialCompleted) // Only clear text and hide button if tutorial was completed
            {
                txt.text = "";
                button1.gameObject.SetActive(false);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Error: Button not found");
        }
    }
}
