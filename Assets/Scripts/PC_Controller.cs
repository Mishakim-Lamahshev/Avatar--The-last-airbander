using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 20f;
    public float attackInterval = 1.5f; // Time interval between actions
    public GameObject ZA;
    private Rigidbody2D rb;
    public GameObject pcCharacter;
    public GameObject playerCharacter;
    public float AttackSpeed = 50f;
    public GameObject AttackController;

    // Difficulty adjustment factors
    private float difficultyFactor = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pcCharacter = GameObject.FindGameObjectWithTag("Enemy");
        playerCharacter = GameObject.FindGameObjectWithTag("Player");

        AdjustDifficultyBasedOnPlayerStats();

        // Start the repeating action method after a delay
        InvokeRepeating("RandomAction", Random.Range(1f, 3f), attackInterval);
    }

    void AdjustDifficultyBasedOnPlayerStats()
    {
        // Access the playerProgressInstance
        PlayerProgress playerProgress = StatsHandle.playerProgressInstance;

        if (playerProgress != null)
        {
            // Example adjustment: Increase move speed based on player's combined power levels
            int totalPlayerPower = playerProgress.earthPower + playerProgress.firePower + playerProgress.waterPower + playerProgress.airPower;

            // Calculate a difficulty factor based on total player power
            difficultyFactor = 1.0f + (totalPlayerPower / 10.0f); // Adjust this formula as needed

            // Adjust opponent's attributes based on difficulty factor
            moveSpeed *= difficultyFactor;
            jumpForce *= difficultyFactor;
            attackInterval /= difficultyFactor; // Shorter interval = more difficult
            AttackSpeed *= difficultyFactor;
        }
        else
        {
            Debug.LogWarning("PlayerProgress instance not found.");
        }
    }

    void Update()
    {

    }

    void RandomAction()
    {
        float randomValue = Random.value;

        if (randomValue < 0.33f)
        {
            MoveRandomly();
        }
        else if (randomValue < 0.66f)
        {
            Jump();
        }
        else
        {
            Attack();
        }
    }

    void MoveRandomly()
    {
        float horizontalInput = Random.Range(-1f, 1f);
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Attack()
    {
        GameObject attackObject = Instantiate(ZA, pcCharacter.transform.position - new Vector3(3f, 0f, 0f), Quaternion.identity);
        Rigidbody2D attackRb = attackObject.GetComponent<Rigidbody2D>();
        if (attackRb != null)
        {
            attackRb.velocity = new Vector2(-AttackSpeed, 0f);
        }
        else
        {
            Debug.LogError("Attack object is missing Rigidbody2D component");
        }
        Debug.Log("Attack!");
    }
}
