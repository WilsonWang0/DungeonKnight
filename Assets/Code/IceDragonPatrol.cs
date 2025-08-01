using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonPatrol : MonoBehaviour
{
    public float detectionRange = 25f;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float timeBetweenFireballs = 0.5f;
    public float attackDuration = 3f;
    public LayerMask playerLayer;

    public Sprite idleSprite;
    public Sprite attackSprite;

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    private enum BossState { Idle, Attacking }
    private BossState currentState = BossState.Idle;

    private bool isAttacking = false;


    //Sound Effects
    public AudioClip attackSound;
    private AudioSource audioSource;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found;
        }

        if (player == null || isAttacking) return;

        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= detectionRange && currentState == BossState.Idle)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        currentState = BossState.Attacking;
        spriteRenderer.sprite = attackSprite;
        Debug.Log("Boss starts attack!");

        for (int i = 0; i < 3; i++)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenFireballs);
        }

        float remainingTime = attackDuration - (3 * timeBetweenFireballs);
        if (remainingTime > 0)
            yield return new WaitForSeconds(remainingTime);

        currentState = BossState.Idle;
        spriteRenderer.sprite = idleSprite;
        Debug.Log("Boss returns to idle.");
        isAttacking = false;
    }

    void Shoot()
    {

        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }


        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            Iceball iceballScript = fb.GetComponent<Iceball>();
            if (iceballScript != null)
            {
                iceballScript.direction = (player.transform.position - firePoint.position).normalized;
                iceballScript.shooter = this.gameObject;  // ✅ Assign the boss as the shooter at runtime
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
