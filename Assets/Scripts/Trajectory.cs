using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rb;
    //public int maxBounces = 5;
    public int pointNumber;   //500
    public float timeGap;   //0.01
    public LayerMask collisionLayerMask;


    void Update()
    {
        List<Vector3> pointList = new List<Vector3>();
        Vector2 predictedVelocity = rb.velocity;
        Vector2 currentPosition = transform.position;

        for (int i = 0; i < pointNumber; i++)
        {
            //float time = i * timeGap;

            Vector2 nextPoint = currentPosition + predictedVelocity * timeGap;
            
            RaycastHit2D hit = Physics2D.Linecast(currentPosition, nextPoint, collisionLayerMask);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "floor")
                {
                    //currentPosition = hit.point;
                    predictedVelocity = Vector2.Reflect(predictedVelocity, hit.normal);
                    nextPoint = currentPosition + predictedVelocity * timeGap;
                }
                else if(hit.collider.tag == "wall") 
                {
                    predictedVelocity = new Vector2(0, 0);
                }
            }
           
            pointList.Add(nextPoint);
            currentPosition = nextPoint;
            
        }
        lineRenderer.positionCount = pointList.Count;
        //lineRenderer.positionCount = 10;
        lineRenderer.SetPositions(pointList.ToArray());
    }
}

