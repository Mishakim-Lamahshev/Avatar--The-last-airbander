using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentLevelManager : MonoBehaviour
{
    private PlayerProgress playerProgress = StatsHandle.playerProgressInstance;

    [Tooltip("An array of the sprites for the different levels of the opponent")] // Corrected capitalization
    [SerializeField] public Sprite[] opponentSprites;

    // Start function
    void Start()
    {
        // Get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the player progress is not null
        if (playerProgress != null)
        {
            // Prevent index out of range errors by ensuring the arena level is within the bounds of the array
            int index = Mathf.Clamp(playerProgress.arenaLevel - 1, 0, opponentSprites.Length - 1);

            // Set the sprite based on the arena level, adjusted for zero-based indexing
            spriteRenderer.sprite = opponentSprites[index];
        }
        else
        {
            Debug.LogWarning("PlayerProgress instance not found. Using default sprite.");
        }
    }
}
