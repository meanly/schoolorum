using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public Text timerText; // Assign in Inspector
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        timerText.text = "0:00";
        StartCoroutine(StartTimerAfterDelay(5f));
    }

    IEnumerator StartTimerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
