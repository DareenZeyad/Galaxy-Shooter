using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour{
    public Sprite[] lives;
    public Image liveImageDisplay;
    public Text scoreText;
    public GameObject titleScreen;
    public int score; 

    public void UpdateLives(int currentLive){
        liveImageDisplay.sprite = lives[currentLive];
    }

    public void UpdateScorse(){
        score += 10;
        scoreText.text = "SCORE: " + score;
    }

    public void ShowTitleScreen(){
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen(){
        titleScreen.SetActive(false);
        scoreText.text = "SCORE: ";
    }
}
