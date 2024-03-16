using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Button and Text
using UnityEngine.SceneManagement;

public class TriggerButtonPopUp : MonoBehaviour
{
    public Button button1; // Assign in the inspector
    public Text txt;
    public Image DialogBacground;
    public string sceneToLoad; // Assign in the inspector

    public StatsHandle statsHandle; // Reference to the StatsHandle script

    [SerializeField] private string textToDisplay;

    private void Start()
    {
        // Hide buttons on start
        DialogBacground.enabled = false;
        button1.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the tutorial is completed before showing the button and text
        if (TutorialNPC.tutorialCompleted)
        {
            int arenaLevel = StatsHandle.GetCurrentStat("arena");
            int fireLevel = StatsHandle.GetCurrentStat("fire");
            int waterLevel = StatsHandle.GetCurrentStat("water");
            int earthLevel = StatsHandle.GetCurrentStat("earth");
            int airLevel = StatsHandle.GetCurrentStat("air");
            button1.gameObject.SetActive(true);
            if (this.gameObject.tag == "Arena") // Only show the button if the object has the "TutorialEnd" tag
            {
                Debug.Log("Arena tag found");

                if (fireLevel < arenaLevel || waterLevel < arenaLevel || earthLevel < arenaLevel || airLevel < arenaLevel)
                {
                    button1.gameObject.SetActive(false);
                    textToDisplay = "You must train all your elements to level" + arenaLevel + " to unlock this level!";
                }
                else
                {
                    button1.gameObject.SetActive(true);
                }
            }
            else
            {
                // allow max level 5 for all elements check tag for element to check
                bool shouldLetPlayerIn = false;
                if (this.gameObject.tag == "Ozai")
                {
                    if (fireLevel < 5)
                    {
                        shouldLetPlayerIn = true;
                    }
                    else
                    {
                        shouldLetPlayerIn = false;
                        textToDisplay = "You have reached the max level for fire!";
                    }
                }
                else if (this.gameObject.tag == "Katara")
                {
                    if (waterLevel < 5)
                    {
                        shouldLetPlayerIn = true;
                    }
                    else
                    {
                        shouldLetPlayerIn = false;
                        textToDisplay = "You have reached the max level for water!";
                    }
                }
                else if (this.gameObject.tag == "Toph")
                {
                    if (earthLevel < 5)
                    {
                        shouldLetPlayerIn = true;
                    }
                    else
                    {
                        shouldLetPlayerIn = false;
                        textToDisplay = "You have reached the max level for earth!";
                    }
                }
                else if (this.gameObject.tag == "Appa")
                {
                    if (airLevel < 5)
                    {
                        shouldLetPlayerIn = true;
                    }
                    else
                    {
                        shouldLetPlayerIn = false;
                        textToDisplay = "You have reached the max level for air!";
                    }
                }
                if (shouldLetPlayerIn)
                {
                    button1.gameObject.SetActive(true);
                }
                else
                {
                    button1.gameObject.SetActive(false);
                }
            }
            DialogBacground.enabled = true;
            txt.text = textToDisplay;
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
                DialogBacground.enabled = false;
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
