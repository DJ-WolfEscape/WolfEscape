using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    public Text startText;
    public static bool isGameStarted;

    public GameObject startDisplay;

    private bool isCoroutineRunning;

    private AudioSource source;
    public AudioClip[] clip;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        isGameStarted = false;
        startDisplay.SetActive(true);
        countdownDisplay.gameObject.SetActive(false);
        isCoroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (isCoroutineRunning || isGameStarted)
                return;

            StartCoroutine(CountdownToStart());
            isCoroutineRunning = true;
            countdownDisplay.gameObject.SetActive(true);
            startText.gameObject.SetActive(false);
        }
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            if (countdownTime == 3)
            {
                source.PlayOneShot(clip[0]);
            }
            else if (countdownTime == 2)
                {
                    source.PlayOneShot(clip[1]);
                }
            else if (countdownTime == 1)
            {
                source.PlayOneShot(clip[2]);
            }
            else if (countdownTime == 0)
            {
                source.PlayOneShot(clip[3]);
            }
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        source.PlayOneShot(clip[3]);

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
        startDisplay.gameObject.SetActive(false);
        isGameStarted = true;
        isCoroutineRunning = false;
    }
}
