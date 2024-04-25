using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    Rigidbody2D rb;
    private int currentWaypoint = 0;
    public float speed;

    public float stoppingTime = 3.0f;
    bool stopping = false;
    float stopTimer;

    public GameObject[] waypoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stopTimer = stoppingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypoint].transform.position, transform.position) < 0.05f)
        {
            stopping = true;
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
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
    }
}