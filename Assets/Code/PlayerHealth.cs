using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth = 10f;

    public Image imageHealthBar;  // Assign in Inspector

    private GameOverUI gameOverUI;
    private bool isDead = false;
 
    public GameObject normalModel; 

    public GameObject attackModel;

    private SpriteRenderer normalSpriteRenderer;

    private SpriteRenderer attackSpriteRenderer;

    public AudioClip takeDamageSound;
    private AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        gameOverUI = FindObjectOfType<GameOverUI>();
        if (gameOverUI == null)
        {
            Debug.LogWarning("GameOverUI script not found in the scene!");
        }

        if (imageHealthBar != null)
        {
            imageHealthBar.fillAmount = 1f;
        }

        if (normalModel != null) normalSpriteRenderer = normalModel.GetComponent<SpriteRenderer>();
        if (attackModel != null) attackSpriteRenderer = attackModel.GetComponent<SpriteRenderer>();

        if (normalSpriteRenderer == null) Debug.LogWarning("Missing SpriteRenderer on normalModel");
        if (attackSpriteRenderer == null) Debug.LogWarning("Missing SpriteRenderer on attackModel");  
                
    }

    public void TakeDamage(float amount)
    {
        Controller controller = GetComponent<Controller>();

        // Camera Shake
        CameraShake.Instance.ShakeCamera(0.3f, 0.5f); 

        if (normalSpriteRenderer != null || attackSpriteRenderer != null)

        {
            StartCoroutine(FlashRed());
            if (takeDamageSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(takeDamageSound);
            }
        }


        if (controller != null && controller.IsShieldActive())
        {
            // Shield is active; ignore damage
            return;
        }

        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log("Player hit! Health = " + currentHealth);

        if (imageHealthBar != null)
        {
            imageHealthBar.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }


    public void TakeDamageWithoutRed(float amount)
    {
        Controller controller = GetComponent<Controller>();

        

        if (controller != null && controller.IsShieldActive())
        {
            // Shield is active; ignore damage
            return;
        }

        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log("Player hit! Health = " + currentHealth);

        if (imageHealthBar != null)
        {
            imageHealthBar.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }



    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died!");
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
        }

        // Optional: disable movement or play animation
        // gameObject.SetActive(false);
    }



    private IEnumerator FlashRed()
    {
        // Backup original colors
        // Color originalNormal = normalSpriteRenderer != null ? normalSpriteRenderer.color : Color.white;
        // Color originalAttack = attackSpriteRenderer != null ? attackSpriteRenderer.color : Color.white;

        Color originalNormal = Color.white;
        Color originalAttack = Color.white;

        // Set both to red regardless of which is active
        if (normalSpriteRenderer != null) normalSpriteRenderer.color = Color.red;
        if (attackSpriteRenderer != null) attackSpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        // Restore both to their original color
        if (normalSpriteRenderer != null) normalSpriteRenderer.color = originalNormal;
        if (attackSpriteRenderer != null) attackSpriteRenderer.color = originalAttack;
    }










    public void IncreaseHealth(float amount)
    {
        currentHealth = maxHealth;

        if (imageHealthBar != null)
        {
            imageHealthBar.fillAmount = currentHealth / maxHealth;

            // Adjust the X scale of the health bar to reflect new max health
            float baseScale = 1f;           // scale for 10 health
            float newScaleX = baseScale * (maxHealth / 10f);
            imageHealthBar.transform.localScale = new Vector3(newScaleX, 1f, 1f);
        }
    }





}
