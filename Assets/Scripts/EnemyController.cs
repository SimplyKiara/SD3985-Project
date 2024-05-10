using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    private int currentWaypoint = 0;
    public float speed;

    public float stoppingTime = 3.0f;
    bool stopping = false;
    float stopTimer;

    public GameObject[] waypoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stopTimer = stoppingTime;

        animator.SetFloat("MoveX", -1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }

            if (Vector2.Distance(waypoints[currentWaypoint].transform.position, transform.position) < 0.5f)
            {
                stopping = true;
                currentWaypoint++;
            }

            if (stopping)
            {
                stopTimer -= Time.deltaTime;
                if (stopTimer < 0)
                    stopping = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * speed);
                stopTimer = stoppingTime;
            }

            float movementDirection = waypoints[currentWaypoint].transform.position.x - transform.position.x;

            if (movementDirection < 0f)
            {
                animator.SetFloat("MoveX", 1f);
            }
            else if (movementDirection > 0f)
            {
                animator.SetFloat("MoveX", -1f);
            }
        }
    }
}