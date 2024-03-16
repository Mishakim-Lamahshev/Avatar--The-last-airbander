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
            button1.gameObject.SetActive(true);
            if(this.gameObject.tag == "Arena") // Only show the button if the object has the "TutorialEnd" tag
            {
                Debug.Log("Arena tag found");
                int arenaLevel = StatsHandle.GetCurrentStat("arena");
                int fireLevel = StatsHandle.GetCurrentStat("fire");
                int waterLevel = StatsHandle.GetCurrentStat("water");
                int earthLevel = StatsHandle.GetCurrentStat("earth");
                int airLevel = StatsHandle.GetCurrentStat("air");
                if(fireLevel < arenaLevel || waterLevel < arenaLevel || earthLevel < arenaLevel || airLevel < arenaLevel)
                {
                    button1.gameObject.SetActive(false);
                    textToDisplay = "You must train all your elements to level"+arenaLevel +" to unlock this level!";
                }
                else
                {
                    button1.gameObject.SetActive(true);
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
