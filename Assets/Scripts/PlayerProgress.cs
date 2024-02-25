using UnityEngine;

[System.Serializable]
public class PlayerProgress {
    private int earthTrainingSessionsCompleted;
    private int fireTrainingSessionsCompleted;
    private int waterTrainingSessionsCompleted;
    private int airTrainingSessionsCompleted;

    public int earthPower;
    public int firePower;
    public int waterPower;
    public int airPower;

    public PlayerProgress() {
        earthTrainingSessionsCompleted = 0;
        fireTrainingSessionsCompleted = 0;
        waterTrainingSessionsCompleted = 0;
        airTrainingSessionsCompleted = 0;

        earthPower = 1;
        firePower = 1;
        waterPower = 1;
        airPower = 1;
    }

    public void EarthTrainingSessionCompleted() {
        earthTrainingSessionsCompleted++;
        earthPower++;
        Debug.Log("Earth training sessions completed: " + earthTrainingSessionsCompleted);
    }

    public void FireTrainingSessionCompleted() {
        fireTrainingSessionsCompleted++;
        firePower++;
        Debug.Log("Fire training sessions completed: " + fireTrainingSessionsCompleted);
    }

    public void WaterTrainingSessionCompleted() {
        waterTrainingSessionsCompleted++;
        waterPower++;
        Debug.Log("Water training sessions completed: " + waterTrainingSessionsCompleted);
    }

    public void AirTrainingSessionCompleted() {
        airTrainingSessionsCompleted++;
        airPower++;
        Debug.Log("Air training sessions completed: " + airTrainingSessionsCompleted);
    }
}
