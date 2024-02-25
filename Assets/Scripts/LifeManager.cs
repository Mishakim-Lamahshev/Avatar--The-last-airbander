using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int life = 3; // Default life count

    void Start()
    {
        AdjustLivesBasedOnProgress();
    }

    // Adjust the player's lives based on their progress
    private void AdjustLivesBasedOnProgress()
    {
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;

        if (playerProgress != null)
        {
            // Add extra life for every 4 total power points
            int totalPower = playerProgress.earthPower + playerProgress.firePower + playerProgress.waterPower + playerProgress.airPower;
            int extraLives = totalPower / 4; // For every 4 power points, gain an extra life

            life += extraLives;

            Debug.Log("Adjusted lives based on player progress. Total lives: " + life);
        }
        else
        {
            Debug.LogWarning("PlayerProgress instance not found. Using default lives.");
        }
    }

    // Call this method whenever the character loses a life
    public void LoseLife()
    {
        if (TutorialManager.IsTutorialComplete) // Check if the tutorial is over before updating lives
        {
            life--;

            if (life <= 0) // Check if life is zero, then load the lose scene
            {
                if (!gameObject.CompareTag("Player"))
                {
                    UpdatePlayerProgress(); // Call method to update player progress
                }
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    // Method to update player progress
    private void UpdatePlayerProgress()
    {
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;
        Debug.Log("Session: " + gameObject.name);

        if (playerProgress != null)
        {
            if (gameObject.name == "EarthEnemy") playerProgress.EarthTrainingSessionCompleted();
            else if (gameObject.name == "FireEnemy") playerProgress.FireTrainingSessionCompleted();
            else if (gameObject.name == "WaterEnemy") playerProgress.WaterTrainingSessionCompleted();
            else if (gameObject.name == "AirEnemy") playerProgress.AirTrainingSessionCompleted();
            else if (gameObject.name == "ShadowEnemy") playerProgress.ArenaLevelCompleted();
        }
        else
        {
            Debug.LogWarning("PlayerProgress instance not found.");
        }
    }
}
