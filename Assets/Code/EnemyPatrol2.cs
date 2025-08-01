using UnityEngine;

public class EnemyPatrol2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 5f;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    public Vector2 fireballDirection = Vector2.right; // Set in Inspector

    private Vector3 startPos;
    private int direction = 1;
    private float fireTimer;

    void Start()
    {
        startPos = transform.position;
        fireTimer = fireInterval;
    }

    void Update()
    {
        // Move vertically
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - startPos.y) >= moveDistance)
        {
            direction *= -1;
            //Flip();
        }

        // Shooting
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireInterval;
        }
    }

    // void Flip()
    // {
    //     Vector3 localScale = transform.localScale;
    //     localScale.x *= -1;
    //     transform.localScale = localScale;
    // }

    void Shoot()
    {
        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            Fireball fireballScript = fb.GetComponent<Fireball>();
            if (fireballScript != null)
            {
                fireballScript.direction = fireballDirection.normalized; // ✅ Use manually set direction
                fireballScript.shooter = gameObject;
            }
        }
    }
}
