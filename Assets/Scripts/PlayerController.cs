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
    private int HP = 50;
    public int MaxHP;
    public int currentHP { get => HP; }

    public float timeInvincible = 2.0f;
    bool isInvincible = false;
    public float invincibleTimer;

    GameObject portal1;
    GameObject portal2;

    public GameObject projectilePrefab;
    Vector2 rightDirection = new Vector2(1, 0);
    Vector2 leftDirection = new Vector2(-1, 0);

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

        if (Input.GetMouseButtonDown(0)) {
            Launch();
        }
        //Debug.Log(horizontal);
    }

    private void FixedUpdate()
    {
        portal1 = GameObject.FindWithTag("portal1");
        portal2 = GameObject.FindWithTag("portal2");
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // portal
        if (collision.tag == "portal1")
        {
            transform.position = portal2.transform.position;
            Destroy(portal1);
            Destroy(portal2);
        }
        else if (collision.tag == "portal2")
        {
            transform.position = portal1.transform.position;
            Destroy(portal1);
            Destroy(portal2);
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
        if (value < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        HP += value;
        HP = Mathf.Clamp(HP, 0, MaxHP);

        Debug.Log("Current HP: " + HP + "/" + MaxHP);
    }
    void Launch() {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

        Bullet projectile = projectileObject.GetComponent<Bullet>();
        if (horizontal == 0)
        {
            projectile.Launch(rightDirection, 500);
        }
        else if (horizontal > 0)
        {
            projectile.Launch(rightDirection, 500);
        }
        else if (horizontal < 0)
        {
            projectile.Launch(leftDirection, 500);
        }

        //animator.SetTrigger("Launch");
    }
}