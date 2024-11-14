using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the CheckpointManager and set the current checkpoint
            CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
            if (checkpointManager != null)
            {
                checkpointManager.SetCheckpoint(transform);
                Debug.Log("Checkpoint updated!");

                // Make the checkpoint disappear by disabling or destroying it
                gameObject.SetActive(false); // Disables the checkpoint
                // or
                // Destroy(gameObject); // Use this line instead if you want to permanently destroy it
            }
        }
    }
}