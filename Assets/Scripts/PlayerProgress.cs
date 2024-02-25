using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    private int earthTrainingSessionsCompleted;
    private int fireTrainingSessionsCompleted;
    private int waterTrainingSessionsCompleted;
    private int airTrainingSessionsCompleted;

    private int arenaLevelsCompleted;

    public int earthPower;
    public int firePower;
    public int waterPower;
    public int airPower;

    public int arenaLevel;

    public PlayerProgress()
    {
        earthTrainingSessionsCompleted = 0;
        fireTrainingSessionsCompleted = 0;
        waterTrainingSessionsCompleted = 0;
        airTrainingSessionsCompleted = 0;
        arenaLevelsCompleted = 0;


        earthPower = 1;
        firePower = 1;
        waterPower = 1;
        airPower = 1;
        arenaLevel = 1;
    }

    public void EarthTrainingSessionCompleted()
    {
        earthTrainingSessionsCompleted++;
        earthPower++;
        Debug.Log("Earth training sessions completed: " + earthTrainingSessionsCompleted);
    }

    public void FireTrainingSessionCompleted()
    {
        fireTrainingSessionsCompleted++;
        firePower++;
        Debug.Log("Fire training sessions completed: " + fireTrainingSessionsCompleted);
    }

    public void WaterTrainingSessionCompleted()
    {
        waterTrainingSessionsCompleted++;
        waterPower++;
        Debug.Log("Water training sessions completed: " + waterTrainingSessionsCompleted);
    }

    public void AirTrainingSessionCompleted()
    {
        airTrainingSessionsCompleted++;
        airPower++;
        Debug.Log("Air training sessions completed: " + airTrainingSessionsCompleted);
    }

    public void ArenaLevelCompleted()
    {
        arenaLevelsCompleted++;
        arenaLevel++;
        Debug.Log("Arena levels completed: " + arenaLevelsCompleted);
    }
}
