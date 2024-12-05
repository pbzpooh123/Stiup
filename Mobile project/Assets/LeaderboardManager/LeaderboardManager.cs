using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText; // สำหรับแสดงรายชื่อบน Leaderboard
    public TMP_InputField playerNameInput;  // InputField สำหรับกรอกชื่อผู้เล่น
    public GameObject nameInputPanel;       // Panel ที่ให้ผู้เล่นกรอกชื่อ
    public GameObject leaderboardPanel;     // Panel สำหรับแสดง Leaderboard

    public GameObject Canvas_control;
    public GameObject Assets_Srceenshot;

    private List<PlayerScore> scores = new List<PlayerScore>();
    private const int MaxEntries = 4; // จำกัดจำนวนชื่อใน Leaderboard
    
    public int gameScore = 0; // ตัวแปรเก็บคะแนนจริงในเกม 
    
    private bool shouldShowNameInputPanel = true; // ตัวแปรเช็คว่าให้แสดง nameInputPanel หรือไม่
    
    public TextMeshProUGUI scoreText; // ช่องแสดงคะแนนปัจจุบันในเกม
    
    private void Start()
    {
        nameInputPanel.SetActive(true); // แสดงหน้ากรอกชื่อผู้เล่น
        leaderboardPanel.SetActive(false); // ซ่อน Leaderboard Panel เริ่มต้น
        LoadScores(); // โหลดข้อมูลคะแนนจาก PlayerPrefs
        
        Canvas_control.SetActive(false);
        Assets_Srceenshot.SetActive(false);
        
        
        /*// ตรวจสอบก่อนว่า nameInputPanel และ leaderboardPanel ถูกตั้งค่าแล้วหรือยัง
        if (nameInputPanel == null)
        {
            Debug.LogError("nameInputPanel not assigned in the Inspector!");
        }

        if (leaderboardPanel == null)
        {
            Debug.LogError("leaderboardPanel not assigned in the Inspector!");
        }*/
        
        /*// เริ่มต้นโดยแสดง nameInputPanel ถ้าตัวแปร shouldShowNameInputPanel เป็น true
        if (shouldShowNameInputPanel)
        {
            nameInputPanel.SetActive(true); // แสดงหน้ากรอกชื่อผู้เล่น
            leaderboardPanel.SetActive(false); // ซ่อน Leaderboard Panel เริ่มต้น
        }
        else
        {
            nameInputPanel.SetActive(false); // ซ่อนหน้ากรอกชื่อ
            leaderboardPanel.SetActive(true); // แสดง Leaderboard Panel
        }*/
        
    }

    // ฟังก์ชันเมื่อผู้เล่นกรอกชื่อเสร็จ
    public void OnNameSubmit()
    {
        nameInputPanel.SetActive(false); // ปิดหน้ากรอกชื่อ
        //leaderboardPanel.SetActive(true); // เปิดหน้า Leaderboard

        // เริ่มเกมหลังจากกรอกชื่อ
        StartGame(); // เรียกฟังก์ชันที่เริ่มเกม
        Canvas_control.SetActive(true);
        Assets_Srceenshot.SetActive(true);
        
        // จำลองคะแนนทดสอบและเพิ่มลงใน Leaderboard
        //int testScore = Random.Range(50, 100); // สมมุติคะแนนแบบสุ่มระหว่าง 50 - 100
        //AddNewScore(testScore);
        
        // เล่นเสียงพื้นหลังจากคลิปที่กำหนด
        AudioManager.instance.backgroundMusicSource.clip = AudioManager.instance.backgroundMusic;
        AudioManager.instance.backgroundMusicSource.Play();
        // ใช้คะแนนจริงจากเกมแทนการสุ่มคะแนน
        AddNewScore(gameScore); // เพิ่มคะแนนจริงลงใน Leaderboard
    }
    
    // ฟังก์ชันเริ่มเกม
    private void StartGame()
    {
        // ที่นี่คุณสามารถเริ่มเกม เช่น เปิดฉากแรก หรือทำให้ตัวละครเริ่มเดิน
        Debug.Log("Game Started!"); // พิมพ์ข้อความว่าเกมเริ่มแล้ว
    }

    // ฟังก์ชันเพิ่มคะแนนใหม่
    public void AddNewScore(int score)
    {
        string playerName = playerNameInput.text; // รับชื่อจาก InputField
        scores.Add(new PlayerScore(playerName, score));  // เพิ่มชื่อและคะแนนลงในรายการ

        // เรียงลำดับคะแนนจากสูงไปต่ำ
        scores = scores.OrderByDescending(s => s.Score).ToList(); 

        // ถ้าจำนวนรายชื่อเกิน 4 จะลบรายชื่อที่คะแนนต่ำสุดออก
        if (scores.Count > MaxEntries)
        {
            scores.RemoveAt(scores.Count - 1); // ลบรายชื่อที่มีคะแนนต่ำสุด
        }

        SaveScores();  // บันทึกข้อมูลคะแนน
        DisplayLeaderboard();  // แสดงคะแนนใน Leaderboard
    }

    // ฟังก์ชันเพิ่มคะแนนจากการเก็บ Orb
    public void IncreaseScore()
    {
        gameScore += 1; // เพิ่มคะแนน 1 คะแนน
        scoreText.text = "Score: " + gameScore.ToString(); // อัปเดตคะแนนบนหน้าจอ
        AddNewScore(gameScore); // เพิ่มคะแนนลงใน Leaderboard
    }
    

    // แสดงชื่อและคะแนนบน Leaderboard
    private void DisplayLeaderboard()
    {
        leaderboardText.text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {scores[i].PlayerName} - {scores[i].Score}\n";
        }
    }

    // บันทึกคะแนนใน PlayerPrefs
    private void SaveScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString($"PlayerName_{i}", scores[i].PlayerName);
            PlayerPrefs.SetInt($"PlayerScore_{i}", scores[i].Score);
        }
    }

    // โหลดคะแนนจาก PlayerPrefs
    private void LoadScores()
    {
        scores.Clear();
        for (int i = 0; i < MaxEntries; i++)
        {
            if (PlayerPrefs.HasKey($"PlayerName_{i}"))
            {
                string name = PlayerPrefs.GetString($"PlayerName_{i}");
                int score = PlayerPrefs.GetInt($"PlayerScore_{i}");
                scores.Add(new PlayerScore(name, score));
            }
        }
        DisplayLeaderboard();
    }
    
    // ฟังก์ชันสำหรับลบข้อมูลทั้งหมดใน Leaderboard
    public void ClearLeaderboard()
    {
        // ลบข้อมูลทั้งหมดใน PlayerPrefs
        PlayerPrefs.DeleteAll();

        // รีเซ็ตคะแนนและชื่อ
        scores.Clear();

        // แสดง Leaderboard ใหม่ (จะเป็นค่าว่างตอนนี้)
        DisplayLeaderboard();
    }
    
    public void RestartGame()
    {
        // ลบข้อมูลเกี่ยวกับคะแนนและชื่อผู้เล่นใน Leaderboard
        /*for (int i = 0; i < MaxEntries; i++)
        {
            PlayerPrefs.DeleteKey($"PlayerName_{i}");
            PlayerPrefs.DeleteKey($"PlayerScore_{i}");
        }*/

        // รีเซ็ตคะแนน
        scores.Clear();

        // แสดงหน้ากรอกชื่อใหม่
        /*nameInputPanel.SetActive(true);  // ให้หน้ากรอกชื่อปรากฏ
        leaderboardPanel.SetActive(false); // ซ่อน Leaderboard Panel*/

        // รีเซ็ตคะแนนของเกม
        gameScore = 0;
        
        //PlayerPrefs.DeleteAll();

        // เปลี่ยนสถานะให้แสดง nameInputPanel
        shouldShowNameInputPanel = true;
        nameInputPanel.SetActive(true);
        leaderboardPanel.SetActive(false);

        // แสดงข้อความว่าเกมถูกรีสตาร์ทแล้ว
        Debug.Log("Game Restarted!");

        // โหลด Scene ปัจจุบันใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; 
        
        
        
    }
    // ฟังก์ชันเพื่อให้ CheckpointManager เรียกเพื่อซ่อน nameInputPanel และแสดง leaderboardPanel
    public void ShowLeaderboardPanel()
    {
        shouldShowNameInputPanel = false;
        nameInputPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }



}

[System.Serializable]
public class PlayerScore
{
    public string PlayerName;
    public int Score;

    public PlayerScore(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}
