using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    public float minSize;
    public Transform player;
    public Transform portalA;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        Vector3 location = new Vector3(player.position.x, player.position.y, -10);
        cam.transform.position = location;
        /*
        float predictedSize = Vector3.Distance(player.transform.position, portalA.transform.position);
        predictedSize = Mathf.Clamp(predictedSize, minSize, int.MaxValue);
        predictedSize /= 2;
        cam.orthographicSize = predictedSize;
        */
    }
    
}
