using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public float targetTime;
    public TMP_Text timerText;
    public GameObject finishFlag;
    public GameObject golfCart;

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

        if (GameIsFinished())
        {
            Debug.Log("Finish");
        }
 
        int minutes = Mathf.FloorToInt(targetTime / 60f);
        int seconds = Mathf.FloorToInt(targetTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool GameIsFinished()
    {
        float positionX = finishFlag.transform.position.x;
        float positionZ = finishFlag.transform.position.z;

        float cartPositionX = golfCart.transform.position.x;
        float cartPositionZ = golfCart.transform.position.z;
        return cartPositionZ + 10 > positionZ && cartPositionZ - 10 < positionZ
            && cartPositionX + 10 > positionX && cartPositionX - 10 < positionX;
    }
}
