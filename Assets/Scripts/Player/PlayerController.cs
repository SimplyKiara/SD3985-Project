using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float moveSpeed = 0f;
    public float horizontal;
    public float previousLook = 1f;

    private int collected = 0;
    private bool levelCompleted = false;

    public GameObject portal1;
    public GameObject portal2;

    public GameObject projectilePrefab;
    public int bulletNumber = 0;
    public SpotController spotController;
    public Vector3 mouseDirection = new Vector2();

    public AudioSource teleportAudio;
    public AudioSource openPortalAudio;
    public AudioSource collectAudio;
    public AudioSource walkingAudio;
    public AudioSource winAudio;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetFloat("LookX", 1f);
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

            walkingAudio.enabled = true;
        }
        else
        {
            animator.SetFloat("LookX", previousLook);
            walkingAudio.enabled = false;
        }

        // shooting
        /*
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
        */
    }

    private void FixedUpdate()
    {
        portal1 = GameObject.FindGameObjectWithTag("portal1");
        portal2 = GameObject.FindGameObjectWithTag("portal2");
        //horizontal = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

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
                teleportAudio.Play();
                ClearBulletNumber();
                TextManager.instance.ReducePortals();
            }
        }
        else if (collision.CompareTag("portal2"))
        {
            if (portal1 != null && collision.gameObject != portal1)
            {
                transform.position = portal1.transform.position;
                Destroy(portal1); // Destroy the collided portal
                Destroy(portal2);
                teleportAudio.Play();
                ClearBulletNumber();
                TextManager.instance.ReducePortals();
            }
        }

        if ((collision.CompareTag("door") && !levelCompleted ))
        {
            levelCompleted = true;
            winAudio.Play();
            rb.bodyType = RigidbodyType2D.Static;
            Invoke("LeaveLevel", 5.0f);
        }
    }

    private void LeaveLevel()
    {
        SceneManager.LoadScene(0);
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

    public void Launch()
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