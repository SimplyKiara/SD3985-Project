using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 currentlocation;
    public Vector2 previouslocation;
    public Vector2 direction;
    public Quaternion rotation;
    public Quaternion rotation2;

    public GameObject portalPrefab1;
    public GameObject portalPrefab2;

    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        currentlocation = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame

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
        
    }

    public void Launch (Vector3 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // portal
        if(collision.tag == "wall")
        {
            Destroy(gameObject);
            if (rotation2.z < 0) { 
                GameObject Object = Instantiate(portalPrefab1, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            }
            else if (rotation2.z > 0)
            {
                GameObject Object = Instantiate(portalPrefab2, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            }
            
        }
        
    }

}
