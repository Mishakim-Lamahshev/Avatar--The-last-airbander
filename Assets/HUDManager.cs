using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class HUDManager : MonoBehaviour
{
    // Reference to the single TextMeshPro UI element
    public TextMeshPro statsText;

    void Update()
    {
        // Assuming you have a way to access the player's current stats
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;

        if (playerProgress != null)
        {
            // Update the single UI element with all current stats
            statsText.text = $"Level: {playerProgress.arenaLevel}\t" +
                             $"Earth Power: {playerProgress.earthPower}\t" +
                             $"Fire Power: {playerProgress.firePower}\t" +
                             $"Water Power: {playerProgress.waterPower}\t" +
                             $"Air Power: {playerProgress.airPower}";
        }
    }
}
