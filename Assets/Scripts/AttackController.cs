using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject WA, FA, DA, AA;
    public float AttackSpeed = 50f;

    private GameObject pcCharacter;
    private GameObject playerCharacter;
    private SpriteRenderer pcRenderer;
    private Color originalColor;

    void Start()
    {
        // Find the PC character by tag
        pcCharacter = GameObject.FindGameObjectWithTag("Enemy");
        playerCharacter=GameObject.FindGameObjectWithTag("Player");
        // Ensure the PC character has a SpriteRenderer component
        pcRenderer = pcCharacter.GetComponent<SpriteRenderer>();
        if (pcRenderer != null)
        {
            originalColor = pcRenderer.color;
        }
        else
        {
            Debug.LogError("Player character is missing SpriteRenderer component");
        }
    }

    public void CommitAttack(char a)
    {
        GameObject attackPrefab = null;

        switch (a)
        {
            case 'a':
                Debug.Log("AIR ATTACK");
                attackPrefab = AA;
                break;
            case 'w':
                Debug.Log("WATER ATTACK");
                attackPrefab = WA;
                break;
            case 'f':
                Debug.Log("FIRE ATTACK");
                attackPrefab = FA;
                break;
            case 'd':
                Debug.Log("DIRT ATTACK");
                attackPrefab = DA;
                break;
            default:
                Debug.Log("Invalid attack type");
                return;
        }

        GameObject attackObject = Instantiate(attackPrefab, playerCharacter.transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
        Rigidbody2D attackRb = attackObject.GetComponent<Rigidbody2D>();

        if (attackRb != null)
        {
            attackRb.velocity = new Vector2(AttackSpeed, 0f);
        }
        else
        {
            Debug.LogError("Attack object is missing Rigidbody2D component");
        }
    }
}
