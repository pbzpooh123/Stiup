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
                
                //------------------From Leaderboard---------------------------------------
                // เพิ่มคะแนนเมื่อเก็บ Orb
                LeaderboardManager leaderboardManager = FindObjectOfType<LeaderboardManager>();
                if (leaderboardManager != null)
                {
                    leaderboardManager.IncreaseScore(); // เพิ่มคะแนน 1
                }
                
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlayCollectItemSound();
                }
                //------------------From Leaderboard---------------------------------------
            }
        }
    }
}