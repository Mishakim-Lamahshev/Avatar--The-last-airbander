using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int hp = 100; // Base hit points

    void Start()
    {
        AdjustHPBasedOnArenaLevel();
    }

    private void AdjustHPBasedOnArenaLevel()
    {
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;

        if (playerProgress != null)
        {
            // Increase HP based on the arena level, adjust this formula as needed
            hp += playerProgress.arenaLevel * 20; // Example: Increase HP by 20 for each arena level

            Debug.Log($"Adjusted HP based on arena level. Total HP: {hp}");
        }
        else
        {
            Debug.LogWarning("PlayerProgress instance not found. Using default HP.");
        }
    }

    // Modify to take damage parameter
    public void TakeDamage(int damage)
    {
        if (TutorialManager.IsTutorialComplete) // Ensure the tutorial is completed
        {
            string sceneName = SceneManager.GetActiveScene().name;
            PlayerProgress playerProgress = StatsHandle.playerProgressInstance;
            hp -= damage;

            Debug.Log($"Remaining HP: {hp}");

            if (hp <= 0)
            {
                Debug.Log(gameObject.tag+" HP reached 0");
                if(sceneName=="ArenaFight 1")
                {
                    Debug.Log("ARENA DEATHHHHHHHH");
                    if(gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Player lost - GO TO LOSE SCENE");
                        SceneManager.LoadScene("LoseScene");
                    }
                    if (!gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Enemy lost - finished level "+playerProgress.arenaLevel);
                        UpdatePlayerProgress(); // Update progress before losing
                        if(playerProgress.arenaLevel>5) // If the enemy is the shadow enemy, load the win scene
                        {
                            Debug.Log("Player won - GO TO WIN SCENE");
                            SceneManager.LoadScene("WinScene");
                        }
                        SceneManager.LoadScene("MainScene");
                    }

                }
                else
                {
                    Debug.Log("TRAINING DEATHHHHHHHH");
                    if (!gameObject.CompareTag("Player"))
                    {
                        Debug.Log("WIN TRAINING");
                        UpdatePlayerProgress();
                    }                    
                    SceneManager.LoadScene("MainScene"); // Load scene when HP reaches 0
                }                
                
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
