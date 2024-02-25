using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject WA, FA, DA, AA;
    public float AttackSpeed = 50f;

    private GameObject playerCharacter;
    private float lastAirAttackTime, lastWaterAttackTime, lastFireAttackTime, lastDirtAttackTime;
    private float baseDelay = 1.5f; // Base delay of 1 second between attacks

    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    public void CommitAttack(char a)
    {
        float currentTime = Time.time;
        float delay = GetAttackDelay(a);

        // Check if enough time has passed since the last attack of this type
        if (!IsAttackAllowed(a, currentTime, delay)) return;

        // Perform attack based on type
        GameObject attackPrefab = null;
        switch (a)
        {
            case 'a':
                Debug.Log("AIR ATTACK");
                attackPrefab = AA;
                lastAirAttackTime = currentTime;
                break;
            case 'w':
                Debug.Log("WATER ATTACK");
                attackPrefab = WA;
                lastWaterAttackTime = currentTime;
                break;
            case 'f':
                Debug.Log("FIRE ATTACK");
                attackPrefab = FA;
                lastFireAttackTime = currentTime;
                break;
            case 'd':
                Debug.Log("DIRT ATTACK");
                attackPrefab = DA;
                lastDirtAttackTime = currentTime;
                break;
        }

        if (attackPrefab != null)
        {
            PerformAttack(attackPrefab);
        }
    }

    private bool IsAttackAllowed(char attackType, float currentTime, float delay)
    {
        switch (attackType)
        {
            case 'a': return currentTime - lastAirAttackTime >= delay;
            case 'w': return currentTime - lastWaterAttackTime >= delay;
            case 'f': return currentTime - lastFireAttackTime >= delay;
            case 'd': return currentTime - lastDirtAttackTime >= delay;
            default: return false;
        }
    }

    private float GetAttackDelay(char attackType)
    {
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;
        if (playerProgress == null) return baseDelay;

        int skillPoints = 0;
        switch (attackType)
        {
            case 'a': skillPoints = playerProgress.airPower; break;
            case 'w': skillPoints = playerProgress.waterPower; break;
            case 'f': skillPoints = playerProgress.firePower; break;
            case 'd': skillPoints = playerProgress.earthPower; break;
        }

        float skillPointReduction = 0.05f * skillPoints; // Each skill point reduces delay by 0.05 seconds
        return Mathf.Max(0.2f, baseDelay - skillPointReduction); // Ensure delay doesn't go below 0.2 seconds
    }

    private void PerformAttack(GameObject attackPrefab)
    {
        GameObject attackObject = Instantiate(attackPrefab, playerCharacter.transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
        Rigidbody2D attackRb = attackObject.GetComponent<Rigidbody2D>();
        if (attackRb != null) attackRb.velocity = new Vector2(AttackSpeed, 0f);
        else Debug.LogError("Attack object is missing Rigidbody2D component");
    }
}
