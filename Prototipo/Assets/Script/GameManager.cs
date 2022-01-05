using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //menu pausa
    public GameObject menu;
    public GameObject menuAudio;
    public GameObject menuLose;
    public SceneAsset game;
    public static bool GameIsPaused;

    //counter score
    private GameObject player;
    public Text uiDistance;

    //menu start
    public GameObject startMenu;
    public int countdownTime;
    public Text countdownDisplay;
    public Text scoreCoins;
    private Boolean perdeuJogo = true;
    private int counterCoins = 0;


    //menu lose
    public Text loseScore;
    public Text loseCoins;
    public Text bestScoreUI;
    int bestScore = 0;
    int distance = 0;
    //player
    public GameObject Player { get => player; set => player = value; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        bestScoreUI.text = "Your best: " + PlayerPrefs.GetInt("HighScore",0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (perdeuJogo)
            return;
        //se pressionar esc pausa
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (!CountdownController.isGameStarted)
            return;
        //counter score
        distance = Mathf.RoundToInt(Player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";
        scoreCoins.text = counterCoins.ToString();


    }

    public void AtualizarContador()
    {
        counterCoins++;

    }

    public void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        menu.SetActive(false);
        menuAudio.SetActive(false);
    }

    void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0.0f;
        menu.SetActive(true);
    }

    public void OnResumeClick()
    {
        Resume();
    }

    public void OnAudioClick()
    {
        GameIsPaused = true;
        menuAudio.SetActive(true);
        menu.SetActive(false);
    }

    public void OnLeaveClick()
    {
        Debug.Log("Leaving...");
    }

    public void onBackclick()
    {
        GameIsPaused = true;
        menuAudio.SetActive(false);
        menu.SetActive(true);
    }



    //menu lose


    public void RestartGame()
    {
        Debug.Log("teste");
        SceneManager.LoadScene(game.name);
    }

    //SE PERDEU O JOGO
    public void GameOver()
    {
        perdeuJogo = true;
        loseScore.text = distance.ToString() + " m";
        if (distance > bestScore)
        {
            bestScore = distance;
            if (bestScore > PlayerPrefs.GetInt("HighScore", 0))
            {

                bestScoreUI.text = "Your best: " + bestScore;
                PlayerPrefs.SetInt("HighScore", bestScore);
                PlayerPrefs.Save();
            }

        }
        loseCoins.text = counterCoins.ToString();

    }

    public void GameStarted()
    {
        perdeuJogo = false;
    }
}
