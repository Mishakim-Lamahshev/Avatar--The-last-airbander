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
        // Check if the attack collides with the enemy character
        if (character.gameObject == enemy)
        {
            // Call LoseLife on the LifeManager script attached to the enemy
            if (enemyLifeManager != null)
            {
                enemyLifeManager.LoseLife();
            }

            // Determine the color based on the attack object's tag
            Color colorToChangeTo = DetermineColorBasedOnTag();

            // Make the enemy character glow with the determined color
            enemyRenderer.color = colorToChangeTo;

            // Start the coroutine to restore the original color after 0.2 seconds
            StartCoroutine(RestoreOriginalColorAfterDelay(0.2f));
            // Disable the renderer of the attack object
            gameObject.GetComponent<Renderer>().enabled = false;
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
