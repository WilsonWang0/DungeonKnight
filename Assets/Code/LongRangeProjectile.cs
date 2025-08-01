using UnityEngine;

public class LongRangeProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 3f;
    public float lifetime = 5f;

    private Rigidbody2D rb;
    private int direction = 1;  // 1 for right, -1 for left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direction * speed, 0f);
        }

        Destroy(gameObject, lifetime); // Auto-destroy after time
    }

    public void SetDirection(int dir)
    {
        direction = dir;

        // Flip the sprite if needed
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = direction < 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy on hit
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
