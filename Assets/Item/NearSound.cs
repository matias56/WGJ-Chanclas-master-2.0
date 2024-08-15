using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearSound : MonoBehaviour
{
    public AudioClip nSound; // The sound to play when the player is near
    public float volume = 1f; // Volume of the sound
    public Transform player; // Reference to the player's transform
    public float activationDistance = 5f; // Distance at which the sound will play
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = nSound;
        audioSource.volume = volume;
        audioSource.loop = true; // Make the sound loop while the player is near
        audioSource.playOnAwake = false; // Don't play the sound until the player is near
    }

    void Update()
    {
        // Calculate the distance between the player and the torch
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance && !audioSource.isPlaying)
        {
            audioSource.Play(); // Play the sound when the player is within the activation distance
        }
        else if (distance > activationDistance && audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the sound when the player moves out of the activation distance
        }
    }
}
