using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject portalPrefab1;
    public GameObject portalPrefab2;

    Rigidbody2D rigidbody2d;
    Animator animator;
    private PlayerController player;

    float flightTime = 5.0f;
    float timeLeft;
    float dirX = 0;
    float dirY = 0;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timeLeft = flightTime;
    }

    public void Initialize(PlayerController controller)
    {
        player = controller;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector3 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);

        if (direction.x > 0)
        {
            dirX = 0.5f;
        }
        else if (direction.x < 0)
        {
            dirX = -0.5f;
        }
        animator.SetFloat("MoveX", dirX);

        if (direction.y > 0)
        {
            dirY = 0.5f;
        }
        else if (direction.y < 0)
        {
            dirY = -0.5f;
        }
        animator.SetFloat("MoveY", dirY);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            player.IncrementBulletNumber();
            int bulletNumber = player.GetBulletNumber();

            Destroy(gameObject);

            if (bulletNumber % 2 == 1)
            {
                Instantiate(portalPrefab1, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(portalPrefab2, transform.position, Quaternion.identity);
            }

            Debug.Log("Bullet = " + bulletNumber);
        }
    }
}