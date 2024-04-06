using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public float targetTime;
    public TMP_Text timerText = null;
    public GameObject finishFlag = null;
    public GameObject golfCart;

    void Start()
    {
        targetTime++;
    }

    void Update()
    {
        if (GameIsFinished())
        {
            if (timerText != null)
            {
                timerText.text = "[M]enu\n[P]lay Again";
            }
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (Input.GetKey(KeyCode.P))
            {
                ReloadLevel();
            }
        }
        else if (timerText != null)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0f)
            {
                TimeOver();
                return;
            }
            int minutes = Mathf.FloorToInt(targetTime / 60f);
            int seconds = Mathf.FloorToInt(targetTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TimeOver()
    {

    }

    private bool GameIsFinished()
    {
        if (finishFlag == null)
        {
            return false;
        }

        float positionX = finishFlag.transform.position.x;
        float positionZ = finishFlag.transform.position.z;

        float cartPositionX = golfCart.transform.position.x;
        float cartPositionZ = golfCart.transform.position.z;
        return cartPositionZ + 10 > positionZ && cartPositionZ - 10 < positionZ
            && cartPositionX + 10 > positionX && cartPositionX - 10 < positionX;
    }
}
