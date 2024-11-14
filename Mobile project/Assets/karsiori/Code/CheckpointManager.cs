using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;  // Stores the last activated checkpoint
    public GameObject player;            // Reference to the player object

    public GameObject Assets_Srceenshot;
    
    
    
    // This method is called when the player dies
    public void RespawnPlayer()
    {
       
            if (currentCheckpoint != null)
            {
                player.transform.position = currentCheckpoint.position;  // ย้ายผู้เล่นไปที่ checkpoint
                
            }
            
    }

    
    public void SetCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
    
    
}
