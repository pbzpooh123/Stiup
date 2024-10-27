using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;  // Stores the last activated checkpoint
    public GameObject player;            // Reference to the player object

    // This method is called when the player dies
    public void RespawnPlayer()
    {
        if (currentCheckpoint != null)
        {
            player.transform.position = currentCheckpoint.position;
        }
        else
        {
            Debug.LogWarning("No checkpoint set. Player will respawn at the default position.");
        }
    }

    // Optional: Set the current checkpoint from another script
    public void SetCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
}
