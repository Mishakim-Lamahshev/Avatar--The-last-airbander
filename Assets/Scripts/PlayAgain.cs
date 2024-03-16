using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the button component attached to this GameObject
        Button button = GetComponent<Button>();
        
        // Add a listener for the button's click event
        button.onClick.AddListener(NewGame);
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("OpeningScene");
        
    }
}
