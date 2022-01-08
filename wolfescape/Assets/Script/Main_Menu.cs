using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Main_Menu : MonoBehaviour
{
    public AudioSource audio;
    private bool isMuted = false;
    public Slider menuSlider;
    public AudioMixer menuMixer;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>();
        menuMixer.SetFloat("MenuVol", PlayerPrefs.GetFloat("menuFloat", 0));
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

    public void setSoundMenu(float volume)
    {
        menuMixer.SetFloat("MenuVol", volume);
        PlayerPrefs.SetFloat("menuFloat", volume);
        PlayerPrefs.Save();
    }
}
