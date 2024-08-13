using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    public Door door; // Reference to the door that will open

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.name);
        if (other.CompareTag("Player"))
        {
            // Player has collected the key
            door.SetHasKey(true);
            Destroy(gameObject); // Remove the key from the scene
        }
    }
}
