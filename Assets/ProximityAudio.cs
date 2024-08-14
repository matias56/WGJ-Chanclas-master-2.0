using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    public Transform player;         // Reference to the player's Transform
    public float maxVolume = 1.0f;   // Maximum volume when the player is closest
    public float maxDistance = 5.0f; // Distance at which the volume is maxVolume

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calculate the distance between the player and the item
        float distance = Vector3.Distance(player.position, transform.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Clamp(1 - (distance / maxDistance), 0, 1) * maxVolume;

        // Apply the calculated volume to the AudioSource
        audioSource.volume = volume;
    }
}
