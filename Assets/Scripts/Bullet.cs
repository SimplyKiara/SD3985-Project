using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 currentlocation;
    public Vector3 previouslocation;
    public Vector3 direction;
    public Quaternion rotation;
    public Vector3 eulerAngles;
    public Quaternion rotation2;
    // Start is called before the first frame update
    void Start()
    {
        currentlocation = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
}
