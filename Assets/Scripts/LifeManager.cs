using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int life = 3;

    // Call this method whenever the character loses a life
    public void LoseLife()
    {
        // Check if the tutorial is over before updating lives
        if (TutorialManager.IsTutorialComplete)
        {
            life--;

            // Check if life is zero, then load the lose scene
            if (life <= 0)
            {
                if (!gameObject.CompareTag("Player"))
                {
                    // Call method to update player progress
                    UpdatePlayerProgress();
                }
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    // Method to update player progress
    private void UpdatePlayerProgress()
    {
        // Access the playerProgressInstance from the StatsHandle script
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;
        Debug.Log("Session: " + gameObject.name);
        // Check if the playerProgressInstance is not null
        if (playerProgress != null)
        {
            if (gameObject.name == "EarthEnemy")
                playerProgress.EarthTrainingSessionCompleted();
            else if (gameObject.name == "FireEnemy")
                playerProgress.FireTrainingSessionCompleted();
            else if (gameObject.name == "WaterEnemy")
                playerProgress.WaterTrainingSessionCompleted();
            else if (gameObject.name == "AirEnemy")
                playerProgress.AirTrainingSessionCompleted();
        }
        else
        {
            Debug.LogWarning("PlayerProgress instance not found.");
        }
    }

}
