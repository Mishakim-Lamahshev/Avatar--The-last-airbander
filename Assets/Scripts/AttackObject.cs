using UnityEngine;
using System.Collections;

public class AttackObject : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private SpriteRenderer enemyRenderer;
    private Color originalColor;

    private LifeManager enemyLifeManager; // Reference to the LifeManager script

    void Start()
    {
        // Ensure the enemy object has a SpriteRenderer component
        enemyRenderer = enemy.GetComponent<SpriteRenderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.color;
        }
        else
        {
            Debug.LogError("Enemy object is missing SpriteRenderer component");
        }

        // Get the LifeManager script component from the enemy
        enemyLifeManager = enemy.GetComponent<LifeManager>();
        if (enemyLifeManager == null)
        {
            Debug.LogError("Enemy object is missing LifeManager component");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == player.tag)
        {
            return;
        }
        // Check if the collided object has the "Enemy" tag
        if (other.tag == enemy.tag)
        {
            HandleEnemyCollision(other.gameObject);
        }
    }

    void HandleEnemyCollision(GameObject character)
    {
        if (character.gameObject == enemy)
        {
            int damage = CalculateDamageBasedOnElement();

            if (enemyLifeManager != null)
            {
                enemyLifeManager.TakeDamage(damage);
            }

            Color colorToChangeTo = DetermineColorBasedOnTag();
            enemyRenderer.color = colorToChangeTo;
            StartCoroutine(RestoreOriginalColorAfterDelay(0.2f));
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    int CalculateDamageBasedOnElement()
    {
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;
        if (playerProgress == null) return 10; // Default damage if player progress is not available

        switch (this.tag) // Assuming tags are "FA", "WA", "AA", "DA"
        {
            case "FA":
                return playerProgress.firePower * 5; // Example damage calculation
            case "WA":
                return playerProgress.waterPower * 5;
            case "AA":
                return playerProgress.airPower * 5;
            case "DA":
                return playerProgress.earthPower * 5;
            default:
                return 10;
        }
    }

    Color DetermineColorBasedOnTag()
    {
        switch (this.tag) // Using this.tag to get the tag of the attack object
        {
            case "FA":
                return Color.red;
            case "WA":
                return Color.blue;
            case "AA":
                return Color.white;
            case "DA":
                return new Color(0.6f, 0.4f, 0.2f); // A brown color
            default:
                return originalColor; // Return the original color if the tag doesn't match
        }
    }

    IEnumerator RestoreOriginalColorAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Restore the original color of the enemy character
        enemyRenderer.color = originalColor;
    }
}
