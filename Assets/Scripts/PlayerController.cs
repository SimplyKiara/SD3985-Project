using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0f;
    Rigidbody2D rb;
    float horizontal;

    GameObject portal1;
    GameObject portal2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        portal1 = GameObject.FindWithTag("portal1");
        portal2 = GameObject.FindWithTag("portal2");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "portal1")
        {
            transform.position = portal2.transform.position;
        }
        else if (collision.tag == "portal2")
        {
            transform.position = portal1.transform.position;
        }
    }
}
