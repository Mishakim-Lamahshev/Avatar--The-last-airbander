using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    void Start()
    {
        // Get the button component attached to this GameObject
        Button button = GetComponent<Button>();
        
        // Add a listener for the button's click event
        button.onClick.AddListener(SkipTutorial);
    }

    // This method will be called when the button is clicked
    public void SkipTutorial()
    {
        SceneManager.LoadScene("MainScene");
        
    }
}
