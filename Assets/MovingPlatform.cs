using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointAOffset;
    public Vector3 pointBOffset;
    public float speed = 2.0f;
    private Vector3 target;

    private Vector3 pointA;
    private Vector3 pointB;

    void Start()
    {
        // Calculate world positions based on offsets
        pointA = transform.position + pointAOffset;
        pointB = transform.position + pointBOffset;
        target = pointB;
    }

    void Update()
    {
        // Move the platform towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Switch the target when the platform reaches one of the points
        if (transform.position == pointB)
        {
            target = pointA;
        }
        else if (transform.position == pointA)
        {
            target = pointB;
        }
    }

    // Draw Gizmos to visualize Points A and B and the path between them
    private void OnDrawGizmos()
    {

        // Set the color of the Gizmos to be drawn
        Gizmos.color = Color.red;

        // Draw spheres at Point A and Point B
        Gizmos.DrawSphere(pointA, 0.1f);
        Gizmos.DrawSphere(pointB, 0.1f);

        // Draw a line between Point A and Point B
        Gizmos.DrawLine(pointA, pointB);
    }
}
