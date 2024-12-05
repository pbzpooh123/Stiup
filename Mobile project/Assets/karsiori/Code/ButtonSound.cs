using UnityEngine;
using UnityEngine.UI; // ใช้สำหรับ UI Button

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonClickSound; // เสียงที่เล่นเมื่อกดปุ่ม
    private AudioSource audioSource;   // คอมโพเนนต์ AudioSource

    void Start()
    {
        // หา AudioSource ที่มีใน GameObject นี้
        audioSource = GetComponent<AudioSource>();

        // ตรวจสอบว่า GameObject นี้มีคอมโพเนนต์ AudioSource หรือไม่
        if (audioSource == null)
        {
            // ถ้าไม่มีคอมโพเนนต์ AudioSource ให้เพิ่ม
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ตรวจสอบว่า Button ที่มีอยู่ถูกผูกกับการทำงานใน UI
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            // เมื่อกดปุ่ม, เรียกใช้ฟังก์ชัน PlayButtonSound()
            btn.onClick.AddListener(PlayButtonSound);
        }
    }

    // ฟังก์ชันที่เล่นเสียงเมื่อกดปุ่ม
    void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            // เล่นเสียงที่กำหนดเมื่อกดปุ่ม
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}