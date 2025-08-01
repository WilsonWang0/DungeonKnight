using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed = 3.0f;

    public float damage = 1f;



    public GameObject attackArea;
    public GameObject attackEffect;
    public GameObject normalModel;
    public GameObject attackModel;

    public GameObject winPanel;
    public GameObject shopPanel;
    public Transform endlessTelePoint;

    public GameObject shieldObject; // Assign in inspector
    public float attackDuration = 0.3f;

    private Vector3 originalScale;
    private bool isAttacking = false;

    public bool shieldReady = false;
    private bool shieldActive = false;


    public GameObject circleSkillPrefab;

    public bool circleSkillUnlocked = false;
    private bool isCircleSkillOnCooldown = false;
    private float circleSkillCooldown = 5f;


    public GameObject longRangeProjectilePrefab;
    public Transform firePoint;
    public bool longRangeSkillUnlokced = false;
    private bool isLongRangeSkillOnCooldown = false;
    private float longRangeSkillCooldown = 1.5f;

    public Image circleSkillIcon;
    public Image longRangeSKillIcon;

    private float originalSpeed;
    private Coroutine slowCoroutine;
    private SpriteRenderer spriteRenderer;


    //Sound Effects
    public AudioClip attackSound;  
    public AudioClip skill2Sound;
    public AudioClip skill1Sound;
    public AudioClip swingSound;
    public AudioClip shieldActivateSound;
    private AudioSource audioSource;

    public EnemySpawner enemySpawner;





    //Switch Character
    public GameObject character1Model;
    public GameObject character2Model;
    private bool usingCharacter1 = true;


    void Start()
    {
        originalScale = transform.localScale;
        originalSpeed = speed;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        if (attackArea != null)
            attackArea.SetActive(false);

        if (attackEffect != null)
            attackEffect.SetActive(false);

        if (normalModel != null)
            normalModel.SetActive(true);

        if (attackModel != null)
            attackModel.SetActive(false);

        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        if (shieldReady && !shieldActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActivateShield());
            if (shieldActivateSound != null)
            {
                audioSource.PlayOneShot(shieldActivateSound);
            }
        }

    
        if (winPanel != null && winPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (endlessTelePoint != null)
                {
                    transform.position = endlessTelePoint.position;
                    winPanel.SetActive(false);
                    Time.timeScale = 1f;
                    shopPanel.SetActive(true);

                    if (enemySpawner != null)
                    {
                        enemySpawner.StartEndlessSpawning();
                        speed = 10f;
                        damage = 3f;
                    }
                }
            }
        }

        UpdateSkillIcons();


        //Circle SKill
        if (Input.GetKeyDown(KeyCode.K) && !isCircleSkillOnCooldown && circleSkillUnlocked)
        {
            ActivateCircleSkill();
            StartCoroutine(CircleSkillCooldownRoutine());
        }

        //Long distance attack skill
        if (longRangeSkillUnlokced && !isLongRangeSkillOnCooldown && Input.GetKeyDown(KeyCode.L))
        {
            FireLongRangeProjectile();
            StartCoroutine(LongRangeSkillCooldownRoutine());
        }

    }

    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    IEnumerator Attack()
    {

        if (swingSound != null)
        {
            audioSource.PlayOneShot(swingSound);
        }

        isAttacking = true;

        if (normalModel != null) normalModel.SetActive(false);
        if (attackModel != null) attackModel.SetActive(true);
        if (attackArea != null) attackArea.SetActive(true);
        if (attackEffect != null) attackEffect.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        if (attackArea != null) attackArea.SetActive(false);
        if (attackEffect != null) attackEffect.SetActive(false);
        if (attackModel != null) attackModel.SetActive(false);
        if (normalModel != null) normalModel.SetActive(true);

        isAttacking = false;
    }



    IEnumerator ActivateShield()
    {
        shieldActive = true;

        if (shieldObject != null)
        {
            shieldObject.SetActive(true);

            SpriteRenderer sr = shieldObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = 0.9f; // semi-transparent
                sr.color = c;
            }
        }

        yield return new WaitForSeconds(3f);

        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }

        shieldActive = false;
    }


    public void EnableShieldAbility()
    {
        shieldReady = true;
    }

    public void EnableCircleSkill()
    {
        circleSkillUnlocked = true;
        UpdateSkillIcons();
    }

    public void EnableLongRangeSkill()
    {
        longRangeSkillUnlokced = true;
        UpdateSkillIcons();
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }


    void ActivateCircleSkill()
    {
        if (skill1Sound != null && audioSource != null)
        {
            audioSource.PlayOneShot(skill1Sound);
        }


        if (circleSkillPrefab != null)
        {
            GameObject skill = Instantiate(circleSkillPrefab, transform.position, Quaternion.identity);
            skill.transform.SetParent(transform); // So it follows the player (optional)
        }
    }


    void FireLongRangeProjectile()
    {
        if (skill2Sound != null && audioSource != null)
        {
            audioSource.PlayOneShot(skill2Sound);
        }

        if (longRangeProjectilePrefab != null && firePoint != null)
        {
            int direction = transform.localScale.x > 0 ? 1 : -1;

            GameObject proj = Instantiate(longRangeProjectilePrefab, firePoint.position, Quaternion.identity);

            LongRangeProjectile projectile = proj.GetComponent<LongRangeProjectile>();
            if (projectile != null)
            {
                projectile.SetDirection(direction);
            }
        }
    }


    IEnumerator CircleSkillCooldownRoutine()
    {
        isCircleSkillOnCooldown = true;
        yield return new WaitForSeconds(circleSkillCooldown);
        isCircleSkillOnCooldown = false;
    }


    IEnumerator LongRangeSkillCooldownRoutine()
    {
        isLongRangeSkillOnCooldown = true;
        yield return new WaitForSeconds(longRangeSkillCooldown);
        isLongRangeSkillOnCooldown = false;
    }



    public void UpdateSkillIcons()
    {
        // Circle Skill
        if (!circleSkillUnlocked)
            circleSkillIcon.color = new Color(0.3f, 0.3f, 0.3f); // Locked (dark gray)
        else if (isCircleSkillOnCooldown)
            circleSkillIcon.color = new Color(0.7f, 0.7f, 0.7f); // On cooldown (light gray)
        else
            circleSkillIcon.color = Color.white; // Ready

        // Long Range Skill
        if (!longRangeSkillUnlokced)
            longRangeSKillIcon.color = new Color(0.3f, 0.3f, 0.3f); // Locked
        else if (isLongRangeSkillOnCooldown)
            longRangeSKillIcon.color = new Color(0.7f, 0.7f, 0.7f); // Cooldown
        else
            longRangeSKillIcon.color = Color.white; // Ready
    }



    public void ApplySlow(float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(SlowEffect(duration));
    }


    private IEnumerator SlowEffect(float duration)
    {
        speed = originalSpeed * 0.5f;  // Slow down
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.blue;  // Visual cue
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = originalColor;
        }
        speed = originalSpeed;
    }














    public void SwitchCharacter()
    {
        usingCharacter1 = !usingCharacter1;

        if (character1Model != null && character2Model != null)
        {
            character1Model.SetActive(usingCharacter1);
            character2Model.SetActive(!usingCharacter1);

            Debug.Log("Switched to " + (usingCharacter1 ? "Character 1" : "Character 2"));
        }
    }

}
