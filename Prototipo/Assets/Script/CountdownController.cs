using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    public Text startText;
    public static bool isGameStarted;

    public GameObject startDisplay;

    private bool isCoroutineRunning;

    // Start is called before the first frame update
    void Start()
    {
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
            if (isCoroutineRunning)
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
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
        startDisplay.gameObject.SetActive(false);
        isGameStarted = true;
        isCoroutineRunning = false;
    }
}
