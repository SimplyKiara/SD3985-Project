using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    Vector2 mousePosition;
    Vector2 worldPosition;

    // Update is called once per frame
    void Update()
    {
        // Get position of mouse in world
        mousePosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Pointer follows mouse
        transform.position = worldPosition;
    }
}
