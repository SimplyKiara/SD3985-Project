using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public float moveSpeed = 0f;
    float horizontal;
    float previousLook = 1f;

    private int collected = 0;
    public int HP = 1;
    public int MaxHP;
    public int currentHP { get => HP; }

    public float timeInvincible = 2.0f;
    bool isInvincible = false;
    public float invincibleTimer;

    GameObject portal1;
    GameObject portal2;

    public GameObject projectilePrefab;
    private int bulletNumber = 0;
    public SpotController spotController;
    public Vector3 mouseDirection = new Vector2();

    public AudioSource deathAudio;
    public AudioSource teleportAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetFloat("LookX", 1f);
        invincibleTimer = timeInvincible;
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDirection = spotController.transform.position;

        if (horizontal != 0f)
        {
            if (horizontal > 0)
            {
                animator.SetFloat("LookX", 1f);
                previousLook = 1f;
            }
            else if (horizontal < 0)
            {
                animator.SetFloat("LookX", -1f);
                previousLook = -1f;
            }
        }
        else
        {
            animator.SetFloat("LookX", previousLook);
        }

        // damage cd
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // shooting
        if (Input.GetMouseButtonDown(0))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        portal1 = GameObject.FindGameObjectWithTag("portal1");
        portal2 = GameObject.FindGameObjectWithTag("portal2");
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("portal1"))
        {
            if (portal2 != null && collision.gameObject != portal2)
            {
                transform.position = portal2.transform.position;
                Destroy(portal1); // Destroy the collided portal
                Destroy(portal2);
                teleportAudio.enabled = true;
                teleportAudio.Play();
            }
        }
        else if (collision.CompareTag("portal2"))
        {
            if (portal1 != null && collision.gameObject != portal1)
            {
                transform.position = portal1.transform.position;
                Destroy(portal1); // Destroy the collided portal
                Destroy(portal2);
                teleportAudio.enabled = true;
                teleportAudio.Play();
            }
        }
    }

    // diamonds
    public void CollectibleAmount()
    {
        collected += 1;
        Debug.Log("Diamond collected: " + collected);
    }

    public void ChangeHP(int value)
    {
        if (isInvincible)
            return;

        isInvincible = true;
        invincibleTimer = timeInvincible;

        HP += value;
        HP = Mathf.Clamp(HP, 0, MaxHP);

        Debug.Log("Current HP: " + HP + "/" + MaxHP);
        if (HP <= 0)
        {
            deathAudio.Play();
            Debug.Log("HP 0");
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Bullet projectile = projectileObject.GetComponent<Bullet>();
        projectile.Initialize(this);

        projectile.Launch(mouseDirection - this.gameObject.transform.position, 500);
    }

    public int GetBulletNumber()
    {
        return bulletNumber;
    }

    public void IncrementBulletNumber()
    {
        bulletNumber++;
    }
}