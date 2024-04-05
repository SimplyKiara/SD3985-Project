using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject portalPrefab1;
    public GameObject portalPrefab2;

    private int bulletNumber = 0;

    Rigidbody2D rigidbody2d;
    Animator animator;

    // for destroying bullet
    float flightTime = 5.0f;
    float timeLeft;
    int direction = 1;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timeLeft = flightTime;
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
    }

    public void SetBulletNumber(int number)
    {
        bulletNumber = number;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            //bulletNumber++;
            Destroy(gameObject);

            if (bulletNumber % 2 == 1)
            {
                Instantiate(portalPrefab1, transform.position, Quaternion.identity);
                portalPrefab1.tag = "portal1";
            }
            else
            {
                Instantiate(portalPrefab2, transform.position, Quaternion.identity);
                portalPrefab2.tag = "portal2";
            }
        }
    }
}