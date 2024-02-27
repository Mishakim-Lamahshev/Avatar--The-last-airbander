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

    public static int GetCurrentStat(string elementOrLevel)
    {
        switch (elementOrLevel.ToLower())
        {
            case "fire":
                return StatsHandle.playerProgressInstance.firePower;
            case "water":
                return StatsHandle.playerProgressInstance.waterPower;
            case "earth":
                return StatsHandle.playerProgressInstance.earthPower;
            case "air":
                return StatsHandle.playerProgressInstance.airPower;
            case "arena":
                return StatsHandle.playerProgressInstance.arenaLevel;
            default:
                Debug.LogWarning("Unknown element or level: " + elementOrLevel);
                return 1;
        }
    }
}
