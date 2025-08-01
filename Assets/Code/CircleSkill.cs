using UnityEngine;

public class CircleSkill : MonoBehaviour
{
    public float duration = 3f;
    public float damage = 5f;

    void Start()
    {
        Destroy(gameObject, duration);  // Auto-destroy after duration
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
