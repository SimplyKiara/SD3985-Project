using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    public GameObject player;
    public GameObject spot;

    Vector2 direction;
    Quaternion rotation;
    Quaternion rotation2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Time.timeScale != 0)
        {
            this.gameObject.transform.position = player.transform.position + new Vector3(-0.3f,0,0);

            direction = spot.transform.position - player.transform.position;
            rotation = Quaternion.LookRotation(direction, Vector3.forward);
            rotation2.x = 0;
            rotation2.y = 0;
            rotation2.z = rotation.z;
            rotation2.w = rotation.w;
            transform.rotation = rotation2;
        }


    }
}
