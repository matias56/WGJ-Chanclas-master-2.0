using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float detectionRadius = 1.5f; // Radius of the circle for detecting the player
    public LayerMask playerLayer;        // Layer mask to identify the player
    public Vector3 openPositionOffset;   // Offset for the door to move when opened
    public float openSpeed = 2.0f;       // Speed at which the door opens
    public bool hasKey = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    private AudioSource audioSource;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openPositionOffset;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
        }
        else
        {
            // Perform a circle raycast to detect if the player is within range
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

            if (playerCollider != null && hasKey)
            {
                Debug.Log("Collision detected with: " + playerCollider.gameObject.name);

                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        // Optionally, disable the collider or other components here
        //GetComponent<Collider2D>().enabled = false;
        audioSource.Play();
    }

    public void SetHasKey(bool value)
    {
        hasKey = value;
    }

    // Draw the detection radius in the editor for easier setup
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
