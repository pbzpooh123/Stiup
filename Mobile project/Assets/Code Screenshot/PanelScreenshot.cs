using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScreenshot : MonoBehaviour
{
    public GameObject screenshotPanel;  
    public GameObject backButton;
    public GameObject CameraModeButton;
    public GameObject ShowImagePanel;  // เพิ่มตัวแปรสำหรับ ShowImagePanel
    public GameObject CanvasContor;

    private bool isPaused = false;

    void Start()
    {
        // screenshotPanel.SetActive(true);  // ปิด Panel Screenshot ตอนเริ่มเกม
        backButton.SetActive(false);       // ปิดปุ่มย้อนกลับตอนเริ่มเกม
    }

    public void OnScreenshotIconClick()
    {
        isPaused = true;
        screenshotPanel.SetActive(true);   // เปิด Panel Screenshot
        backButton.SetActive(true);        // เปิดปุ่มย้อนกลับ
        CameraModeButton.SetActive(false);
        CanvasContor.SetActive(false);
        Time.timeScale = 0f;               // หยุดเกมชั่วคราว
    }

    public void OnBackButtonClick()
    {
        isPaused = false;
        screenshotPanel.SetActive(false);  // ปิด Panel Screenshot
        backButton.SetActive(true);  
        CameraModeButton.SetActive(true);  // แสดงปุ่ม CameraModeButton
        ShowImagePanel.SetActive(false);   // ปิด ShowImagePanel
        CanvasContor.SetActive(true);
        Time.timeScale = 1f;               // คืนค่าความเร็วเวลาเกม
    }

    void Update()
    {
        // ตรวจสอบว่าเกมหยุดอยู่หรือไม่
        if (isPaused)
        {
            // Optional: ป้องกันการเคลื่อนไหวหรือการควบคุมของผู้เล่นในขณะที่เกมหยุด
        }
    }
}