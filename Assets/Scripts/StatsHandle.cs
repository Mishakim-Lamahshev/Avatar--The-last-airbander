using UnityEngine;

public class StatsHandle : MonoBehaviour
{
    // Singleton instance for player progress
    public static PlayerProgress playerProgressInstance;

    // Start is called before the first frame update
    void Start()
    {
        // Check if player progress instance already exists
        if (playerProgressInstance == null)
        {
            // If not, create a new instance
            playerProgressInstance = new PlayerProgress();
        }
    }
}
