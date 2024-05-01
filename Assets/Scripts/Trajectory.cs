using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    //public int maxBounces = 5;
    public int pointNumber;   //500
    public float timeGap;   //0.01
    public LayerMask collisionLayerMask;
    public Rigidbody2D rb;

    public GameObject player;
    public GameObject spot;

    void Update()
    {
        List<Vector3> pointList = new List<Vector3>();
        Vector2 direction = spot.transform.position - player.transform.position;
        float x = direction.x / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        float y = direction.y / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        Vector2 predictedVelocity = new Vector2(x,y);
        Vector2 currentPosition = player.transform.position + new Vector3(2*x, 2*y, 0);
                                  //this.gameObject.transform.parent.gameObject.transform.position;
                                  //transform.position;

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

