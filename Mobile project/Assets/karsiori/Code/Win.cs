using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public GameObject Text;
    public GameObject Text2;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // เพิ่มเงื่อนไขที่ต้องการที่นี่ เช่น การแสดงข้อความหรือการเปลี่ยนแปลงสถานะ
            Debug.Log("Player collided with Win object!");
            leaderboardPanel.SetActive(true);
            Text.SetActive(false);
            Text2.SetActive(true);
            
            // หยุดเล่นเสียงพื้นหลัง
            AudioManager.instance.backgroundMusicSource.clip = AudioManager.instance.backgroundMusic;
            AudioManager.instance.backgroundMusicSource.Stop();
            
            // ไม่ทำลายวัตถุที่มีสคริปต์นี้อยู่
            // Object นี้จะไม่ถูกทำลาย
            // เล่นเสียง Win
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayWinSound();
            }
            
        }
    }
}
