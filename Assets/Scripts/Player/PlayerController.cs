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

    public float timeInvincible = 2.0f;
    bool isInvincible = false;
    public float invincibleTimer;

    GameObject portal1;
    GameObject portal2;

    public GameObject projectilePrefab;
    private int bulletNumber = 0;
    public SpotController spotController;
    public Vector3 mouseDirection = new Vector2();

    public AudioSource teleportAudio;
    public AudioSource openPortalAudio;
    public AudioSource collectAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetFloat("LookX", 1f);
        invincibleTimer = timeInvincible;
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

        if (Input.GetMouseButtonDown(0) && bulletNumber < 2)
        {
            Launch();
        }
        // portal cancellation
        if (Input.GetMouseButtonDown(1) && portal1 != null && portal2 == null)
        {
            Destroy(portal1);
            DiminishmentBulletNumber();
        }
        if (Input.GetMouseButtonDown(1) && portal2 != null)
        {
            Destroy(portal1);
            Destroy(portal2);
            ClearBulletNumber();
        }
    }

    private void FixedUpdate()
    {
        portal1 = GameObject.FindGameObjectWithTag("portal1");
        portal2 = GameObject.FindGameObjectWithTag("portal2");
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        //trajectory
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
    }

    // portal
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
                ClearBulletNumber();
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
                ClearBulletNumber();
            }
        }
    }

    public void PortalSound()
    {
        openPortalAudio.Play();
    }

    // diamonds
    public void CollectibleAmount()
    {
        collected += 1;
        collectAudio.Play();
        Debug.Log("Diamond collected: " + collected);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position, Quaternion.identity);
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
    public void DiminishmentBulletNumber()
    {
        bulletNumber--;
    }

    public void ClearBulletNumber()
    {
        bulletNumber = 0;
    }
}