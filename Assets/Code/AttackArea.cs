using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Controller playerController;

    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        playerController = GetComponentInParent<Controller>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null && playerController != null)
        {
            enemy.TakeDamage(playerController.damage);
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}
