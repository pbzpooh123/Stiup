using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public GameObject Assets_Screenshot;
    public GameObject Canvas;
    public GameObject play;
    public GameObject CanvasLeaderboard;
    public GameObject Assets_Leaderboard;
    public GameObject Canvas_Start_Game;
    
    public GameObject Sound_Setting;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardPanel.SetActive(false); // ซ่อน Leaderboard Panel เริ่มต้น
        Assets_Screenshot.SetActive(false);
        Canvas.SetActive(false);
        Sound_Setting.SetActive(false);
    }

    public void OpenleaderboardPanel()
    {
        leaderboardPanel.SetActive(true);
        play.SetActive(false);
    }

    public void OpenCanvasLeaderboard()
    {
        CanvasLeaderboard.SetActive(true);
        Assets_Leaderboard.SetActive(true);
        Canvas_Start_Game.SetActive(false);
        play.SetActive(false);
    }

    public void Back()
    {
        CanvasLeaderboard.SetActive(false);
        play.SetActive(true);
    }

    public void BackSound_Setting()
    {
        Sound_Setting.SetActive(false);
    }
    public void OpenSound_Setting()
    {
        Sound_Setting.SetActive(true);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
}
