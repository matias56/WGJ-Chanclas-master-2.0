using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingWisp : MonoBehaviour
{
   
    public float orbitRadius = 2.0f; // Radius of the orbit
    public float orbitSpeed = 45.0f; // Speed of orbiting in degrees per second
    public Vector3 WispOffset = new Vector3(0,2,0); // Offset from the player

    private float angle;

    public Ihne p;

    void Start()
    {
        p = FindObjectOfType<Ihne>();
        // Calculate the initial angle based on the starting position
        Vector3 offset = transform.position - p.transform.position;
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        // Update the angle to rotate the wisp around the player
        angle += orbitSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;
        float x = p.transform.position.x + Mathf.Cos(radians) * orbitRadius;
        float y = p.transform.position.y + Mathf.Sin(radians) * orbitRadius;

        // Update the position of the wisp
        transform.position = new Vector3(x, y, transform.position.z) + WispOffset;
    }
}
