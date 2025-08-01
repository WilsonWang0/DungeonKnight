using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 15f;
    public float stopDistance = 3f;         // Minimum distance to stop chasing
    public float shootingRange = 7f;        // Start shooting when inside this range

    [Header("Shooting")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;         // Time between shots (in seconds)
    public float initialShotDelay = 0.5f;   // Small delay before first shot

    private Transform player;
    private float fireTimer;
    private bool isInShootingRange = false;
    private bool canShoot = false;         // Additional flag to control shooting

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        fireTimer = initialShotDelay; // Initial delay before first shot
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            LookAtPlayer();

            // Check if enemy is in shooting range
            bool nowInShootingRange = distanceToPlayer <= shootingRange;

            // Entered shooting range? Allow shooting after cooldown
            if (nowInShootingRange && !isInShootingRange)
            {
                canShoot = true;
                fireTimer = initialShotDelay; // Reset timer for first shot
            }

            // Exited shooting range? Prevent shooting
            if (!nowInShootingRange)
            {
                canShoot = false;
            }

            isInShootingRange = nowInShootingRange;

            if (isInShootingRange && distanceToPlayer > stopDistance)
            {
                // Handle shooting cooldown
                if (canShoot)
                {
                    fireTimer -= Time.deltaTime;
                    if (fireTimer <= 0f)
                    {
                        ShootAtPlayer();
                        fireTimer = fireInterval; // Reset cooldown
                    }
                }
            }
            else if (distanceToPlayer > stopDistance)
            {
                // Chase the player (but don't shoot)
                ChasePlayer();
            }
            else
            {
                // Too close — do nothing (or add retreat logic)
            }
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void LookAtPlayer()
    {
        if ((player.position.x > transform.position.x && transform.localScale.x < 0) ||
            (player.position.x < transform.position.x && transform.localScale.x > 0))
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void ShootAtPlayer()
    {
        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            Fireball fireballScript = fb.GetComponent<Fireball>();

            if (fireballScript != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                fireballScript.direction = direction;
                fireballScript.shooter = gameObject;
            }
        }
    }
}