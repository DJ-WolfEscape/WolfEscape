using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public AudioSource audio;
    private bool isMuted = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onLeave()
    {
        Application.Quit();
    }

    public void onMuteClick()
    {
        if (isMuted == true)
        {
            isMuted = false;
            audio.mute = false;
        }
        else
        {
            isMuted = true;
            audio.mute = true;
        }
        
    }
}
