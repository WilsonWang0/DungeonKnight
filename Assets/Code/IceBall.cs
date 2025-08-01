using UnityEngine;

public class Iceball : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 5f;
    public float lifetime = 5f;
    public GameObject shooter;

    private Rigidbody2D rb;


    private SpriteRenderer normalSpriteRenderer;
    private SpriteRenderer attackSpriteRenderer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Avoid hitting whoever shot the fireball
        if (shooter != null && collision.gameObject == shooter)
            return;

        // Damage and slow the player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamageWithoutRed(2); // Reduce health by 2

            }

            Controller controller = collision.gameObject.GetComponent<Controller>();
            if (controller != null)
            {
                controller.StartCoroutine(SlowAndFlash(controller, 3f, 0.5f)); // Slow for 3s to 50% speed
            }
        }

        Destroy(gameObject); // Destroy iceball on contact
    }

    private System.Collections.IEnumerator SlowAndFlash(Controller controller, float duration, float slowFactor)
    {
        float originalSpeed = 5f;
        controller.speed *= slowFactor;

        SpriteRenderer sr = null;
        Color originalColor = Color.white;

        if (controller.normalModel != null)
        {
            sr = controller.normalModel.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                originalColor = Color.white;
                sr.color = Color.cyan;
            }
        }

        // Wait for the full effect duration
        yield return new WaitForSeconds(duration);

        // Revert color if it was changed
        if (sr != null)
        {
            sr.color = originalColor;
        }

        // Revert speed after slowing
        controller.speed = originalSpeed;
    }




}
