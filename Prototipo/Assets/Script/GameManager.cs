using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    //menu pausa
    public GameObject menu;
    public GameObject menuAudio;
    public GameObject menuGraphics;
    public GameObject snow;
    private int estaNevar;
    public Toggle snowParticles;
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

    // Audio
    public AudioMixer mixer;
    

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundSlider;

    //player
    public GameObject Player { get => player; set => player = value; }

    // Start is called before the first frame update
    void Start()
    {
        perdeuJogo = false;

        Player = GameObject.Find("Player");
        bestScoreUI.text = "Your best: " + PlayerPrefs.GetInt("HighScore",0).ToString();
        mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("masterVol", 0));
        mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("musicVol", 0));
        mixer.SetFloat("PlayerSoundVol", PlayerPrefs.GetFloat("soundFX", 0));
        estaNevar = PlayerPrefs.GetInt("nevar", 0);
        if(estaNevar == 1)
        {
            snow.SetActive(true);
            snowParticles.isOn = false;

        }
        else
        {
            snow.SetActive(false);
            snowParticles.isOn = true;

        }
        masterSlider.value = PlayerPrefs.GetFloat("masterVol", 0);
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", 0);
        soundSlider.value = PlayerPrefs.GetFloat("soundFX", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(perdeuJogo== true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
        estaNevar = PlayerPrefs.GetInt("nevar", 0);
        if (estaNevar == 1)
        {
            snow.SetActive(true);

        }
        else
        {
            snow.SetActive(false);

        }
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
        menuGraphics.SetActive(false);

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
    public void OnGraphicsClick()
    {
        GameIsPaused = true;
        menuGraphics.SetActive(true);
        menu.SetActive(false);
    }

    public void OnLeaveClick()
    {
        Debug.Log("Leaving...");
        SceneManager.LoadScene("Menu");
    }

    public void onBackclick()
    {
        GameIsPaused = true;
        menuAudio.SetActive(false);
        menuGraphics.SetActive(false);

        menu.SetActive(true);
    }

    public void onSnowParticlesClick()
    {
        if(PlayerPrefs.GetInt("nevar", 0) == 1)
        {
            estaNevar = 0;
            PlayerPrefs.SetInt("nevar", 0);
        }
        else
        {
            estaNevar = 1;
            PlayerPrefs.SetInt("nevar", 1);

        }

    }



    //menu lose


    public void RestartGame()
    {
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

    // Audio menu

    public void setMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat("masterVol", volume);
        PlayerPrefs.Save();
    }

    public void setSoundVolume(float volume)
    {
        mixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("musicVol", volume);
        PlayerPrefs.Save();
    }

    public void setSoundFXVolume(float volume)
    {
        mixer.SetFloat("PlayerSoundVol", volume);
        PlayerPrefs.SetFloat("soundFX", volume);
        PlayerPrefs.Save();
    }

}
