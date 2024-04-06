using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public float targetTime;
    public TMP_Text timerText;

    void Start()
    {
        targetTime++;
    }

    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0f)
        {
            TimerEnded();
            return;
        }
 
        int minutes = Mathf.FloorToInt(targetTime / 60f);
        int seconds = Mathf.FloorToInt(targetTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
