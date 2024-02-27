using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Button and Text
using UnityEngine.SceneManagement;

public class TriggerButtonPopUp : MonoBehaviour
{
    public Button button1; // Assign in the inspector
    public Text txt;
    public string sceneToLoad; // Assign in the inspector\

    [SerializeField] private string textToDisplay;


    private void Start()
    {

        // Hide buttons on start
        button1.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        txt.text = textToDisplay;
        button1.gameObject.SetActive(true);
        button1.onClick.AddListener(OnClick);
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
            txt.text = "";
            button1.gameObject.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("Error: Button not found");
        }
    }

}
