using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 5f;
    public float lifetime = 3f;
    public GameObject shooter;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Avoid hitting whoever shot the fireball
        if (collision.gameObject == shooter)
            return;

        // Damage the player if it hits them
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Reduce health by 1
            }
        }

        Destroy(gameObject); // Destroy fireball on contact
    }

    
}
