using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Boss takes damage! Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss has been defeated!");
        PlayerRoomTracker tracker = FindObjectOfType<PlayerRoomTracker>();
        if (tracker != null && tracker.currentRoom != null)
        {
            tracker.currentRoom.UnregisterEnemy(gameObject);
        }

        Destroy(gameObject);
    }
}
