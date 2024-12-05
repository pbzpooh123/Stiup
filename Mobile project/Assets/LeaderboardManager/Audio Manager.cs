using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton สำหรับเรียกใช้จากทุกที่

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource; // แหล่งเสียงเพลงพื้นหลัง
    public AudioSource sfxSource;             // แหล่งเสียงเอฟเฟกต์

    [Header("Background Music")]
    public AudioClip backgroundMusic;         // เพลงพื้นหลัง

    [Header("Volume Sliders")]
    public Slider musicSlider;  // Slider สำหรับปรับเสียงเพลงพื้นหลัง
    public Slider sfxSlider;    // Slider สำหรับปรับเสียงเอฟเฟกต์

    [Header("Sound Effects")]
    public AudioClip buttonClickSound;    // เสียงกดปุ่ม
    public AudioClip winSound;            // เสียงชนะ
    public AudioClip loseSound;           // เสียงแพ้
    public AudioClip collectItemSound;    // เสียงเก็บไอเท็ม
    public AudioClip jumpSound;           // เสียงกระโดด

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();

        // โหลดค่า Volume จาก PlayerPrefs (ถ้ามี)
        backgroundMusicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // ตั้งค่า Slider ให้ตรงกับค่า Volume ปัจจุบัน
        if (musicSlider != null)
        {
            musicSlider.value = backgroundMusicSource.volume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // ฟังก์ชันเล่นเสียงกดปุ่ม
    public void PlayButtonClickSound()
    {
        PlaySFX(buttonClickSound);
    }

    // ฟังก์ชันเล่นเสียงชนะ
    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }

    // ฟังก์ชันเล่นเสียงแพ้
    public void PlayLoseSound()
    {
        PlaySFX(loseSound);
    }

    // ฟังก์ชันเล่นเสียงเก็บไอเท็ม
    public void PlayCollectItemSound()
    {
        PlaySFX(collectItemSound);
    }

    // ฟังก์ชันเล่นเสียงกระโดด
    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }

}

/*if (SoundManager.instance != null)
{
    SoundManager.instance.PlayPlaceTowerSound();
}*/