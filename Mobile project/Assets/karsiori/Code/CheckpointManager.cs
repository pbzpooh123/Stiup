using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;  // Stores the last activated checkpoint
    public GameObject player;            // Reference to the player object

    //------------------From Leaderboard---------------------------------------
    public GameObject leaderboardPanel;  // ตัวอ้างอิงถึงหน้า leaderboard panel // Reference to the leaderboard panel
    private int respawnCount = 0;  // ตัวนับจำนวนครั้งที่ respawn // Counter for the number of respawns
    public GameObject Assets_Srceenshot;
    //------------------From Leaderboard---------------------------------------
    
    
    // This method is called when the player dies
    public void RespawnPlayer()
    {
        //------------------From Leaderboard---------------------------------------
        if (respawnCount < 4)  // เช็คว่าผู้เล่น respawn น้อยกว่า 4 ครั้ง // Check if the player has respawned less than 4 times
        {
            if (currentCheckpoint != null)
            {
                player.transform.position = currentCheckpoint.position;  // ย้ายผู้เล่นไปที่ checkpoint
                respawnCount++;  // เพิ่มจำนวนครั้งที่ respawn // Increment the respawn count
            }
            else
            {
                Debug.LogWarning("ยังไม่มี checkpoint กำหนด. ผู้เล่นจะ respawn ที่ตำแหน่งเริ่มต้น.");
            }
        }
        else
        {
            // หยุดเกมและแสดงหน้า leaderboard เมื่อ respawn ถึง 4 ครั้ง
            StopGame();
        }
        //------------------From Leaderboard---------------------------------------
        /*if (currentCheckpoint != null)
        {
            player.transform.position = currentCheckpoint.position;
        }
        else
        {
            Debug.LogWarning("No checkpoint set. Player will respawn at the default position.");
        }*/
    }

    // Optional: Set the current checkpoint from another script
    public void SetCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
    
    //------------------From Leaderboard---------------------------------------
    // ฟังก์ชันหยุดเกมและแสดงหน้า leaderboard
    private void StopGame()
    {
        // สามารถเพิ่มการหยุดการควบคุมเกมได้ เช่น ปิดการควบคุมของผู้เล่น, หยุดเวลา ฯลฯ
        Time.timeScale = 0;  // หยุดเวลาในเกม
        // leaderboardPanel.SetActive(true);  // แสดงหน้า leaderboard
        Assets_Srceenshot.SetActive(false);
        // ซ่อน nameInputPanel
        leaderboardManager.nameInputPanel.SetActive(false);
    
        // แสดง LeaderboardPanel
        leaderboardManager.leaderboardPanel.SetActive(true);
        
        Debug.Log("เกมจบแล้ว! แสดง Leaderboard.");
    }
    public LeaderboardManager leaderboardManager;

    void OnCheckpointReached()
    {
        leaderboardManager.ShowLeaderboardPanel();  // ซ่อน nameInputPanel และแสดง leaderboardPanel
    }
    //------------------From Leaderboard---------------------------------------
}
