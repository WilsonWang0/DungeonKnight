using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 3f;  // Allow decimal precision
    private float currentHealth;

    public GameObject healthBarPrefab;
    private Image healthFill;
    private Transform healthBar;

    private RoomManager roomManager; // Reference to the room this enemy belongs to

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject hb = Instantiate(healthBarPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        healthBar = hb.transform;
        healthBar.SetParent(transform);
        healthFill = healthBar.GetComponentInChildren<Image>();
        UpdateHealthBar();

        // Try to get the RoomManager this enemy is registered to
        roomManager = GetComponentInParent<RoomManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
                
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.rotation = Quaternion.identity; // Keeps bar upright
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0f, currentHealth);
        UpdateHealthBar();

        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0f)
        {
            // Unregister enemy before destroying it
            if (roomManager != null)
            {
                roomManager.UnregisterEnemy(gameObject);
            }

            
            

            Destroy(gameObject);  // Enemy dies
            KillCount.Instance.AddKill();

        }
    }


    private IEnumerator FlashRed()
    {
        //Color originalColor = spriteRenderer.color;
        Color originalColor = Color.white;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = originalColor;
    }
    


    private void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            float fill = currentHealth / maxHealth;
            healthFill.fillAmount = fill;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Controller controller = other.GetComponent<Controller>();
            if (controller != null)
            {
                TakeDamage(controller.damage);
            }
        }
    }
}
