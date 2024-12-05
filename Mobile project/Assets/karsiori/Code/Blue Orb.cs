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

            player.dashSpeed = 20f;
            
            gameObject.SetActive(false);
            
            
            //------------------From Leaderboard---------------------------------------
            // เพิ่มคะแนนเมื่อเก็บ BlueOrb
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
