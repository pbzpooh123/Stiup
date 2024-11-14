using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the CheckpointManager and set the current checkpoint
            PlayerController  player = FindObjectOfType< PlayerController>();

            player.dashSpeed = 500f;
            
            gameObject.SetActive(false);
            
        }
    }
}
