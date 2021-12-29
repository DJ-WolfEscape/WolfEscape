using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //menu pausa
    public GameObject menu;
    public GameObject menuAudio;
    public static bool GameIsPaused;

    //counter score
    private GameObject player;
    public Text uiDistance;

    //menu start
    public GameObject startMenu;
    public int countdownTime;
    public Text countdownDisplay;

    //player
    public GameObject Player { get => player; set => player = value; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
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
        int distance = Mathf.RoundToInt(Player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";


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
}
