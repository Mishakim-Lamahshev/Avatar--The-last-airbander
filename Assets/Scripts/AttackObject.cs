using UnityEngine;
using System.Collections;

public class AttackObject : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public int burnDamage = 1;

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

            // Determine and apply elemental effect
            ApplyElementalEffect(character);

            // Existing color change logic
            Color colorToChangeTo = DetermineColorBasedOnTag();
            enemyRenderer.color = colorToChangeTo;
            StartCoroutine(RestoreOriginalColorAfterDelay(5f));
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void ApplyElementalEffect(GameObject character)
    {
        switch (this.tag)
        {
            case "FA":
                StartCoroutine(ApplyBurningEffect(character, duration: 5f));
                break;
            case "WA":
                StartCoroutine(ApplySlowingEffect(character, duration: 5f));
                break;
            case "DA":
                StartCoroutine(ApplyRootEffect(character, duration: 5f));
                break;
            case "AA":
                ApplyKnockbackEffect(character, force: 5f);
                break;
        }
    }

    IEnumerator ApplyBurningEffect(GameObject character, float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            enemyLifeManager.TakeDamage(burnDamage);
            yield return new WaitForSeconds(1f); // Damage every second
        }
    }

    IEnumerator ApplySlowingEffect(GameObject character, float duration)
    {
        float originalSpeed = character.GetComponent<OpponentController>().moveSpeed;
        character.GetComponent<OpponentController>().moveSpeed /= 2; // Reduce speed by half
        yield return new WaitForSeconds(duration);
        character.GetComponent<OpponentController>().moveSpeed = originalSpeed; // Restore original speed
    }

    IEnumerator ApplyRootEffect(GameObject character, float duration)
    {
        character.GetComponent<OpponentController>().enabled = false; // Disable movement
        yield return new WaitForSeconds(duration);
        character.GetComponent<OpponentController>().enabled = true; // Enable movement
    }

    void ApplyKnockbackEffect(GameObject character, float force)
    {
        Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(new Vector2(force, 0f), ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Character is missing Rigidbody2D component");
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
