using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject portalrightPrefab1;
    public GameObject portalrightPrefab2;
    public GameObject portalleftPrefab1;
    public GameObject portalleftPrefab2;

    Rigidbody2D rigidbody2d;
    Animator animator;
    private PlayerController player;

    float flightTime = 5.0f;
    float timeLeft;

    public Vector2 currentlocation;
    public Vector2 previouslocation;
    public Vector2 direction;
    public Quaternion rotation;
    public Quaternion rotation2;
    bool leftright;   // false: left direction; true: right direction

    int bulletNumber;

    public AudioSource portalAudio;

    void Awake()
    {
        currentlocation = transform.position;
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
            player.DiminishmentBulletNumber();


        }
    }

    void FixedUpdate()
    {
        // bullet direction
        previouslocation = currentlocation;
        currentlocation = transform.position;
        direction = currentlocation - previouslocation;
        rotation = Quaternion.LookRotation(direction, Vector3.forward);
        rotation2.x = 0;
        rotation2.y = 0;
        rotation2.z = rotation.z;
        rotation2.w = rotation.w;
        transform.rotation = rotation2;
        if (direction.x < 0)
        {
            this.leftright = false;
        }
        else if (direction.x > 0)
        {
            this.leftright = true;
        }
    }

    public void Launch(Vector3 direction, float force)
    {
        player.IncrementBulletNumber();
        bulletNumber = player.GetBulletNumber();
        Debug.Log("Bullet = " + bulletNumber);
        float x = direction.x/(Mathf.Abs(direction.x)+Mathf.Abs(direction.y));
        float y = direction.y/(Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        Vector3 direction1 = new Vector3(x, y,0);
        rigidbody2d.AddForce(direction1 * force / Mathf.Pow((Mathf.Pow(x,2))+(Mathf.Pow(y,2)),0.5f));

        if (direction.x < 0)
        {
            this.leftright = false;
        }
        else if (direction.x > 0)
        {
            this.leftright = true;
        }

        //if (direction.x > 0)
        //{
        //    dirX = 0.5f;
        //}
        //else if (direction.x < 0)
        //{
        //    dirX = -0.5f;
        //}
        //animator.SetFloat("MoveX", dirX);

        //if (direction.y > 0)
        //{
        //    dirY = 0.5f;
        //}
        //else if (direction.y < 0)
        //{
        //    dirY = -0.5f;
        //}
        //animator.SetFloat("MoveY", dirY);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall") )
        {


            Destroy(gameObject);

            //if (rotation2.z < 0)
            //{
            //    GameObject Object = Instantiate(portalPrefab1, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            //}
            //else if (rotation2.z > 0)
            //{
            //    GameObject Object = Instantiate(portalPrefab2, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            //}
            if (leftright == false && bulletNumber==1)
            {
                Instantiate(portalleftPrefab1, collision.transform.position + new Vector3(1,0,0), Quaternion.identity);
            }
            else if (leftright == false && bulletNumber == 2)
            {
                Instantiate(portalleftPrefab2, collision.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            }
            else if (leftright == true && bulletNumber == 1)
            {
                Instantiate(portalrightPrefab1, collision.transform.position - new Vector3(1,0,0), Quaternion.identity);
            }
            else if (leftright == true && bulletNumber == 2)
            {
                Instantiate(portalrightPrefab2, collision.transform.position - new Vector3(1, 0, 0), Quaternion.identity);
            }



            portalAudio.enabled = true;
            portalAudio.Play();
        }
    }
}