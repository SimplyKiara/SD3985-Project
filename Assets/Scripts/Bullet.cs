using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject portalPrefab1;
    public GameObject portalPrefab2;

    private int bulletNumber;

    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject);

            if (bulletNumber % 2 == 1)
            {
                Instantiate(portalPrefab1, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(portalPrefab2, transform.position, Quaternion.identity);
            }
        }
    }
}