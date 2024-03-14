using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class UpdateLife : MonoBehaviour
{
    public GameObject character;
    private LifeManager characterLifeManager; // Reference to the LifeManager script

    private TextMeshPro textMeshPro; // Reference to the TextMeshPro component

    // Start is called before the first frame update
    void Start()
    {
        characterLifeManager = character.GetComponent<LifeManager>();
        if (characterLifeManager == null)
        {
            Debug.LogError("Character object is missing LifeManager component");
        }

        // Get the TextMeshPro component from the GameObject
        textMeshPro = GetComponent<TextMeshPro>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text of the TextMeshPro component
        textMeshPro.text = "HP: " + characterLifeManager.hp.ToString();
    }
}
